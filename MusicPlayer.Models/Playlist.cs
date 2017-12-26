using Postulate.Orm.Abstract;
using Postulate.Orm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models
{
    public class Playlist : Record<int>
    {
        [MaxLength(100)]
        [PrimaryKey]
        public string Name { get; set; }
    }
}