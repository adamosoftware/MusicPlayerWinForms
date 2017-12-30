using Postulate.LocalFileDb.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Postulate.Orm.Abstract;
using Postulate.Orm.Enums;
using System.Data;

namespace MusicPlayer.Models
{
    public class Mp3File : Postulate.LocalFileDb.Models.File
    {
        [MaxLength(100)]
        public string Artist { get; set; }

        [MaxLength(100)]
        public string Album { get; set; }        

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        public int? TrackNumber { get; set; }

        public int? Year { get; set; }

        /// <summary>
        /// Indicates that user made manual changes to the record, so <see cref="SetMetadata(FileInfo)"/> should not modify it
        /// </summary>
        public bool IsManuallyEdited { get; set; }

        public DateTime? LastPlayed { get; set; }

        [MaxLength(300)]
        public string SearchText { get; set; }

        public override void BeforeSave(IDbConnection connection, SqlDb<int> db, SaveAction action)
        {
            base.BeforeSave(connection, db, action);
            SearchText = Artist + "|" + Album + "|" + Title;
        }

        public override void SetMetadata(FileInfo fileInfo)
        {
            if (!IsManuallyEdited)
            {
                Title = Name;
                using (var stream = new FileStream(fileInfo.FullName, FileMode.Open))
                {
                    var mp3file = new Id3.Mp3Stream(stream, Id3.Mp3Permissions.Read);
                    if (mp3file.HasTags)
                    {
                        var tags = mp3file.GetAllTags();
                        if (tags.Any())
                        {
                            if (!string.IsNullOrEmpty(tags[0].Title)) Title = tags[0].Title;
                            Artist = tags[0].Artists;
                            Album = tags[0].Album;
                            TrackNumber = tags[0].Track.AsInt;
                            Year = tags[0].Year.AsDateTime?.Year;                            
                        }
                    }
                }
            }
        }
    }
}