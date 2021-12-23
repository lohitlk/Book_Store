using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models
{
    public class Books
    {
        public int Book_Id { get; set; }
        public string Book_Type { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Author { get; set; }
        public string Discription { get; set; }
    }
}
