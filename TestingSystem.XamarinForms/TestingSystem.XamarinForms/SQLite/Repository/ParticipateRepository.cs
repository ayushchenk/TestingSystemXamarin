using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.XamarinForms.SQLite.Model;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.SQLite.Repository
{
    public class ParticipateRepository
    {
        private SQLiteAsyncConnection database;

        public ParticipateRepository(string dbName)
        {
            string databasePath = DependencyService.Get<ISQLiteProvider>().GetDatabasePath(dbName);
            database = new SQLiteAsyncConnection(databasePath);
            CreateTableAsync();
        }

        public async Task CreateTableAsync()
        {
            await database.CreateTableAsync<ParticipateSQLiteModel>();
        }

        public async Task<IList<ParticipateSQLiteModel>> GetAllAsync()
        {
            return await database.Table<ParticipateSQLiteModel>().ToListAsync();
        }

        public async Task<IList<ParticipateSQLiteModel>> FindByAsync(Expression<Func<ParticipateSQLiteModel, bool>> pred)
        {
            return await database.Table<ParticipateSQLiteModel>().Where(pred).ToListAsync();
        }

        public async Task<ParticipateSQLiteModel> GetAsync(int id)
        {
            return await database.GetAsync<ParticipateSQLiteModel>(id);
        }

        public async Task<ParticipateSQLiteModel> SaveAsync(ParticipateSQLiteModel item)
        {
            if(item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item;
            }
            else
            {
                item.Id = await database.InsertAsync(item);
                return item;
            }
        }

        public async Task<ParticipateSQLiteModel> DeleteAsync(ParticipateSQLiteModel item)
        {
            await database.DeleteAsync(item);
            return item;
        }


    }
}
