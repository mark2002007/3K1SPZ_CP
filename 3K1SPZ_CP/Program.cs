using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using _3K1SPZ_CP;
using _3K1SPZ_CP.DAL;


namespace _3K1SPZ_CP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            consoleUI ui = new();
            ui.CallLogPage();
        }
    }
}
    