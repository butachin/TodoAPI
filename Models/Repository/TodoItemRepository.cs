using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using TodoApi.Models;


namespace TodoApi.Models.Repository 
{
    public class TodoItemRepository: IBaseRepository<TodoItem>, ITodoItemRepository
    {
        private string connectionString = null;
        private string tableName = "[dbo].[TodoItems]";

        internal IDbConnection Connection 
        {
            get 
            {
                return new SqlConnection (connectionString);
            }
        }

        public TodoItemRepository (IConfiguration configuration) 
        {
            connectionString = @"Data Source=127.0.0.1;Initial Catalog=TodoApi;Connect Timeout=60;Persist Security Info=True;User ID=sa;Password=Test@1234";
        }

        public int Count ()
        {
            using (IDbConnection db = Connection) 
            {
                var result = db.ExecuteScalar<int> ($"SELECT COUNT(*) FROM {tableName}");
                return result;
            }
        }

        public async Task Remove (TodoItem entity) 
        {
            using (IDbConnection db = Connection) 
            {
                db.Open ();
                using (var tran = db.BeginTransaction ()) 
                {
                    try 
                    {
                        String sql = $@"DELETE FROM {tableName} WHERE Id LIKE @Id";
                        await db.ExecuteAsync (sql, entity, tran);
                        tran.Commit ();
                    } catch 
                    {
                        tran.Rollback ();
                    }
                }
            }
        }

        public async Task<TodoItem> FindAsync (String id) 
        {
            using (IDbConnection db = Connection) 
            {
                db.Open ();
                var query = $"SELECT * FROM {tableName} WHERE Id LIKE '{id}'";
                return (await db.QueryAsync<TodoItem> (query)).FirstOrDefault<TodoItem> ();
            }
        }

        public async Task<IEnumerable<TodoItem>> FindAsync () 
        {
            IEnumerable<TodoItem> result;
            using (IDbConnection db = Connection) 
            {
                db.Open ();
                var query = $"SELECT * FROM {tableName}";
                result = await db.QueryAsync<TodoItem> (query);
                Console.Out.WriteLine (result);
            }
            return result;
        }

        public async Task Add (TodoItem entity) 
        {
            using (IDbConnection db = Connection) 
            {
                db.Open ();
                using (var tran = db.BeginTransaction ())
                 {
                    try 
                    {
                        entity.Id = Guid.NewGuid ().ToString ("N");
                        String sql = $@"INSERT INTO {tableName} (Id ,Name, IsComplete) VALUES (@Id, @Name, @IsComplete)";
                        await db.ExecuteAsync (sql, entity, tran);
                        tran.Commit ();
                    } catch 
                    {
                        tran.Rollback ();
                    }
                }
            }
        }

        public async Task Update (TodoItem entity) 
        {
            using (IDbConnection db = Connection) 
            {
                db.Open ();
                using (var tran = db.BeginTransaction ()) 
                {
                    try 
                    {
                        var sql = $@"UPDATE {tableName} SET Name = @Name, IsComplete = @IsComplete WHERE Id LIKE @Id";
                        await db.ExecuteAsync (sql, entity, tran);
                        tran.Commit ();
                    } catch 
                    {
                        tran.Rollback ();
                    }
                }
            }
        }
    }
}