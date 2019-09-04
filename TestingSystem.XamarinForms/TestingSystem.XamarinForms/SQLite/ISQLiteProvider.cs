using System;
using System.Collections.Generic;
using System.Text;

namespace TestingSystem.XamarinForms.SQLite
{
    public interface ISQLiteProvider
    {
        string GetDatabasePath(string fileName);
    }
}
