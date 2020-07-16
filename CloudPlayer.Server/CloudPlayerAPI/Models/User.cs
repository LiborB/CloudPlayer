using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CloudPlayerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(64)]
        public string PasswordHash {get;set;}
        [StringLength(16)]
        public byte[] PasswordSalt { get; set; }
        [MaxLength(36)]
        public string Token { get; set; }
        [Required]
        public DateTime Created { get; set; }
        
        public List<Song> Songs { get; set; }
    }
}