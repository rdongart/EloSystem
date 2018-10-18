using System.IO;
using System.Reflection;

namespace EloSystem.IO
{
    public static class StaticMembers
    {
        public const string FILE_EXTENSION_NAME = ".elo";
        public static string SaveDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\EloSystems\";
            }
        }


    }
}