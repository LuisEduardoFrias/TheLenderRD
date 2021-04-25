using System.Configuration;

namespace TheLenderRD.Domain.Services
{
    public static class SettingStrings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    }
}
