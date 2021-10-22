using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3K1SPZ_CP.DAL.Repositories
{
    public abstract class BaseRepository
    {
        protected string connectionString { get; set; }
        protected BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
