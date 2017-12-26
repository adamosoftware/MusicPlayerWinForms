using Postulate.Orm.Abstract;
using Postulate.Orm.Attributes;

namespace MusicPlayer.Models
{
    public class PlaylistFile : Record<int>
    {
        [ForeignKey(typeof(Playlist))]
        public int PlaylistId { get; set; }

        [ForeignKey(typeof(Mp3File))]
        public int FileId { get; set; }

        public int? Order { get; set; }
    }
}