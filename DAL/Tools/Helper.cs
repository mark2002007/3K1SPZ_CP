using System.Configuration;

namespace _3K1SPZ_CP;

public static class Helper
{
    public static string CnnVal() => ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
}