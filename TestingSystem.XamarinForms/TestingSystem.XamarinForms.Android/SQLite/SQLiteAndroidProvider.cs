using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestingSystem.XamarinForms.Droid.SQLite;
using TestingSystem.XamarinForms.SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteAndroidProvider))]
namespace TestingSystem.XamarinForms.Droid.SQLite
{
    public class SQLiteAndroidProvider : ISQLiteProvider
    {
        public string GetDatabasePath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            return path;
        }
    }
}