using Dapper;
using Postulate.LocalFileDb;
using Postulate.LocalFileDb.Models;
using Postulate.Orm.SqlCe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Threading.Tasks;

namespace MusicPlayer.Models
{
    public class Mp3Database : Database<Folder, Mp3File>
    {
        public Mp3Database(string path) : base(path)
        {
            Options.IncludePatterns = new string[] { "*.mp3" };
        }

        public Mp3Database() : this(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic))
        {
        }

        protected override void CreateAdditionalTables(IDbConnection connection, SqlCeSyntax syntax)
        {
            syntax.CreateTable<Playlist>(connection);
            syntax.CreateTable<PlaylistFile>(connection, true);
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            return GetConnection().QuerySingle<T>(query, parameters);
        }

        /// <summary>
        /// Finds one or more groups of songs that match the query. For example "lenno" matches Artist: Annie Lennox. "colors" matchest Album: Colors by Beck
        /// </summary>        
        public async Task<IEnumerable<SongGroup>> FindSongGroupsAsync(string query)
        {
            using (var cn = GetConnection())
            {
                var artists = await cn.QueryAsync<ArtistSongGroup>("SELECT 'Artist' AS [Type], [Artist] AS [Label], COUNT(1) AS [SongCount], [Artist] AS [KeyValues] FROM [Mp3File] WHERE [Artist] LIKE '%'+@query+'%' GROUP BY [Artist] ORDER BY [Artist]", new { query = query });
                var albums = await cn.QueryAsync<AlbumSongGroup>("SELECT 'Album' AS [Type], [Artist] + ', ' + [Album] AS [Label], COUNT(1) AS [SongCount], [Album] + '|' + [Artist] AS [KeyValues] FROM [Mp3File] WHERE [Album] LIKE '%'+@query+'%' GROUP BY [Artist], [Album] ORDER BY [Artist], [Album]", new { query = query });
                var songs = await cn.QueryAsync<AlbumSongGroup>("SELECT 'Song' AS [Type], [Artist] + ', ' + [Title] + ' (' + [Album] + ')' AS [Label], -1 AS [SongCount], [Album] + '|' + [Artist] AS [KeyValues] FROM [Mp3File] WHERE [Title] LIKE '%'+@query+'%' ORDER BY [Title]", new { query = query });
                var playlists = await cn.QueryAsync<PlaylistSongGroup>("SELECT 'Playlist' AS [Type], [Name] AS [Label], -1 AS [SongCount], [Id] AS [KeyValues] FROM [Playlist] [pl] WHERE [Name] LIKE '%'+@query+'%' ORDER BY [Name]", new { query = query });

                List<SongGroup> results = new List<SongGroup>();
                results.AddRange(artists);
                results.AddRange(albums);
                results.AddRange(songs);
                results.AddRange(playlists);
                return results;
            }            
        }

        public abstract class SongGroup
        {
            /// <summary>
            /// Indicates Artist, Album, or Playlist
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// Artist, album, song title, or playlist name
            /// </summary>
            public string Label { get; set; }

            public int SongCount { get; set; }

            /// <summary>
            /// Pipe-separated list of columns that provide values to use as criteria for the <see cref="SongQuery"/>
            /// </summary>
            public string KeyValues { get; set; }

            /// <summary>
            /// Query to get the songs for this group
            /// </summary>
            public abstract string SongQuery { get; }

            public async Task<IEnumerable<Mp3File>> GetSongsAsync(IDbConnection connection)
            {
                var paramValues = KeyValues.Split('|');
                DynamicParameters dp = new DynamicParameters();

                int index = 0;
                foreach (var value in paramValues)
                {
                    index++;
                    dp.Add($"param{index}", value);
                }

                return await connection.QueryAsync<Mp3File>(SongQuery, dp);
            }
        }

        public class ArtistSongGroup : SongGroup
        {
            public override string SongQuery => "SELECT * FROM [Mp3File] WHERE [Artist]=@param1 ORDER BY [Album], [TrackNumber]";            
        }

        public class AlbumSongGroup : SongGroup
        {
            public override string SongQuery => "SELECT * FROM [Mp3File] WHERE [Album]=@param1 AND [Artist]=@param2 ORDER BY [TrackNumber]";
        }

        public class PlaylistSongGroup : SongGroup
        {
            public override string SongQuery => "SELECT * FROM [Mp3File] [mp3] WHERE EXISTS(SELECT 1 FROM [PlaylistFile] WHERE [PlaylistId]=@param1 AND [FileId]=[mp3].[Id])";
        }
    }
}