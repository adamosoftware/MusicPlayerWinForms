using Dapper;
using Postulate.LocalFileDb;
using Postulate.LocalFileDb.Models;
using Postulate.Orm.SqlCe;
using System;
using System.Data;
using System.Data.SqlServerCe;

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
    }
}