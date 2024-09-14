using SQLite;

namespace BudgetHelper.Core
{
    // All the code in this file is included in all platforms.
    public class TodoItemDatabase
    {
        SQLiteAsyncConnection Database;

        public TodoItemDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<ToDoItem>();
        }
        public async Task<List<ToDoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<ToDoItem>().ToListAsync();
        }

        //public async Task<List<ToDoItem>> GetItemsNotDoneAsync()
        //{
        //    //await Init();
        //    //return await Database.Table<ToDoItem>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}
        public async Task<int> GetCountAsync()
        {
            await Init();
            return await Database.Table<ToDoItem>().CountAsync();
        }
        public async Task<ToDoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<ToDoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(ToDoItem item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(ToDoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
