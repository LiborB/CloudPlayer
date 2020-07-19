using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using CloudPlayerAPI.Data;
using CloudPlayerAPI.Models;
using CloudPlayerAPI.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using NAudio.Wave;

namespace CloudPlayerAPI.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : BaseApiController
    {
        private readonly IWebHostEnvironment _env;

        public SongController(CloudPlayerContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [Route("getallsongs")]
        [HttpGet]
        public IActionResult GetAllSongsForUser()
        {
            UserTokenHandler();
            var user = GetCurrentUser();
            var songs = _context.Song.Where(x => x.UserId == user.Id);
            var songVms = songs.Select(x => new SongVM()
            {
                Id = x.Id,
                Title = x.Title,
                Duration = x.Duration
            });
            return Ok(songVms);
        }

        [Route("addsong")]
        [HttpPost]
        public IActionResult AddSong([FromForm] AddSongVM addSongVm)
        {
            var user = UserTokenHandler();

            var file = addSongVm.File;
            var userSongPath = Path.Combine(_env.ContentRootPath, $"Data/UserSongs/{user.Id}");
            var userDirectory = System.IO.Directory.CreateDirectory(userSongPath);
            var dateString = DateTime.UtcNow.Ticks.ToString();
            var userSongFullPath = Path.Combine(userSongPath, dateString + Path.GetExtension(file.FileName));
            var fileStream = System.IO.File.Create(userSongFullPath);
            file.CopyTo(fileStream);
            fileStream.Close();
            var audio = new AudioFileReader(userSongFullPath);
            audio.Close();
            var song = new Song()
            {
                Duration = (int) audio.TotalTime.TotalSeconds,
                Title = addSongVm.Title,
                UserId = user.Id,
                FilePath = userSongFullPath
            };
            _context.Song.Add(song);
            _context.SaveChanges();


            return Ok();
        }

        [Route("getsinglesongblob")]
        [HttpGet]
        public IActionResult GetSingleSongBlob(int songId)
        {
            var song = _context.Song.FirstOrDefault(x => x.Id == songId);
            if (song == null)
            {
                return NotFound();
            }

            var songFile = System.IO.File.ReadAllBytes(song.FilePath);


            return File(songFile, "application/octet-stream");
        }
    }
}