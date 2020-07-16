using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPlayerAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string FilePath { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}