using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Project_WPF.MVVM.Core
{
    public static class Settings
    {
        #region Private data to find a dir
        private static string WorkingDirectory = ".";
        private static string UsersDbFileName = "_users.xml";
        private static string AlcoholsDbFileName = "_alcochols.xml";
        private static string DrinksDbFileName = "_drinks.xml";
        private static string OthersDbFileName = "_others.xml";
        #endregion

        #region Global used variables
        #endregion

        #region Public setters for files path
        public static string UsersFile { get { return $"{WorkingDirectory}\\{UsersDbFileName}"; } }
        public static string AlcoholFile { get { return $"{WorkingDirectory}\\{AlcoholsDbFileName}"; } }
        public static string DrinksFile { get { return $"{WorkingDirectory}\\{DrinksDbFileName}"; } }
        public static string OthersFile { get { return $"{WorkingDirectory}\\{OthersDbFileName}"; } }
        #endregion
    }
}
