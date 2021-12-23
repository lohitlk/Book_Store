using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models
{
    public class Cart
    {
        public int cart_id { get; set; }
        public double cart_totalprice { get; set; }
        public int Book_id { get; set; }
        public int user_id { get; set; }
    }
}
