using Microsoft.AspNetCore.Http;

namespace CloudPlayerAPI.ViewModel
{
    public class AddSongVM
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public int Duration { get; set; }
    }
}