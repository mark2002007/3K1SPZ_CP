using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3K1SPZ_CP
{
    public static class Helper
    {
        public static string CnnVal(string name) => ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }
}
