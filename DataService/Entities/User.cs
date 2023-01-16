using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Nama { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public string? ProfilePicture { get; set; }

    }
}
