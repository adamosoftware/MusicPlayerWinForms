using Dapper;
using Postulate.LocalFileDb;
using Postulate.LocalFileDb.Models;
using Postulate.Orm.SqlCe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
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

        public async Task<IEnumerable<ArtistSearch>> FindArtistsAsync(IDbConnection connection, string query)
        {
            return await connection.QueryAsync<ArtistSearch>("SELECT 'Artist' AS [Type], [Artist] AS [Label], COUNT(1) AS [SongCount], [Artist] AS [KeyValues] FROM [Mp3File] WHERE [Artist] LIKE '%'+@query+'%' GROUP BY [Artist] ORDER BY [Artist]", new { query = query });
        }

        public async Task<IEnumerable<AlbumSearch>> FindAlbumsAsync(IDbConnection connection, string query)
        {
            return await connection.QueryAsync<AlbumSearch>("SELECT 'Album' AS [Type], [Artist] + '|' + [Album] AS [Label], COUNT(1) AS [SongCount], [Album] + '|' + [Artist] AS [KeyValues] FROM [Mp3File] WHERE [Album] LIKE '%'+@query+'%' GROUP BY [Artist], [Album] ORDER BY [Artist], [Album]", new { query = query });
        }

        public async Task<IEnumerable<AlbumSearch>> FindSongsInAlbumAsync(IDbConnection connection, string query)
        {
            return await connection.QueryAsync<AlbumSearch>("SELECT 'Song' AS [Type], [Artist] + '|' + [Album] + '|' + [Title] AS [Label], -1 AS [SongCount], [Album] + '|' + [Artist] AS [KeyValues] FROM [Mp3File] WHERE [Title] LIKE '%'+@query+'%' ORDER BY [Title]", new { query = query });
        }

        public async Task<IEnumerable<PlaylistSongSearch>> FindSongsInPlaylistAsync(IDbConnection connection, string query)
        {
            return await connection.QueryAsync<PlaylistSongSearch>("SELECT 'Playlist' AS [Type], [Name] + '|' + [Artist] + '|' + [Title] AS [Label], -1 AS [SongCount], [pl].[Id] AS [KeyValues] FROM [Playlist] [pl] INNER JOIN [PlaylistFile] [pf] ON [pl].[Id]=[pf].[PlaylistId] INNER JOIN [Mp3File] [f] ON [pf].[SongId]=[f].[Id] WHERE [Name] LIKE '%'+@query+'%' ORDER BY [Name]", new { query = query });
        }

        /// <summary>
        /// Finds one or more groups of songs that match the query. For example "lenno" matches Artist: Annie Lennox. "colors" matchest Album: Colors by Beck
        /// </summary>        
        public async Task<IEnumerable<Search>> FindSongGroupsAsync(string query)
        {
            using (var cn = GetConnection())
            {
                var artists = await FindArtistsAsync(cn, query);
                var albums = await FindAlbumsAsync(cn, query);
                var songs = await FindSongsInAlbumAsync(cn, query);
                var playlists = await FindSongsInPlaylistAsync(cn, query);

                List<Search> results = new List<Search>();
                results.AddRange(artists);
                results.AddRange(albums);
                results.AddRange(songs);
                results.AddRange(playlists);
                return results;
            }            
        }

        /*
        private string WhereClausePhrase(string column, string query)
        {
            string[] words = query.Split(' ').Select(s => s.Trim()).ToArray();

        }*/

        public abstract class Search
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

        public class ArtistSearch : Search
        {
            public override string SongQuery => "SELECT * FROM [Mp3File] WHERE [Artist]=@param1 ORDER BY [Album], [TrackNumber]";            
        }

        public class AlbumSearch : Search
        {
            public override string SongQuery => "SELECT * FROM [Mp3File] WHERE [Album]=@param1 AND [Artist]=@param2 ORDER BY [TrackNumber]";
        }

        public class PlaylistSongSearch : Search
        {
            public override string SongQuery => "SELECT * FROM [Mp3File] [mp3] WHERE EXISTS(SELECT 1 FROM [PlaylistFile] WHERE [PlaylistId]=@param1 AND [FileId]=[mp3].[Id])";
        }
    }
}