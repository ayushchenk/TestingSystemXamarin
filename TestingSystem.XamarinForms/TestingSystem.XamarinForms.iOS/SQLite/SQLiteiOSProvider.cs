using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Foundation;
using TestingSystem.XamarinForms.iOS.SQLite;
using TestingSystem.XamarinForms.SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteiOSProvider))]
namespace TestingSystem.XamarinForms.iOS.SQLite
{
    public class SQLiteiOSProvider : ISQLiteProvider
    {
        public string GetDatabasePath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, fileName);
            return path;
        }
    }
}