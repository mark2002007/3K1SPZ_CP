using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3K1SPZ_CP
{
    public class Order
    {
        public int id { get; set; }
        public string user_login { get; set; }
        public string product { get; set; }
        public DateTime order_time { get; internal set; }
    }
}
