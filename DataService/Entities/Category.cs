using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public string? Deskripsi { get; set; }
        
        public List<Book> Books { get; set; }
    }
}
