using System.Linq;

namespace MusicPlayer.Models
{
    public class FilterCriteria
    {
        public string Artist { get; set; }
        public string Album { get; set; }

        public string GetCriteria()
        {
            var props = GetType().GetProperties().Where(pi => pi.GetValue(this) != null);
            return string.Join(" AND ", props.Select(pi => $"[{pi.Name}]='{pi.GetValue(this)}'"));
        }
    }
}