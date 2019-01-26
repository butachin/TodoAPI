using System;
using Microsoft.Extensions.Configuration;
using TodoApi.Models.Repository;

namespace TodoApi.Models.Persistance
{
    public class UnitOfWork: IUnitOfWork
    {
        private ITodoItemRepository todoItems;
        private IConfiguration _configuration;
        
        public UnitOfWork(){

        }
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ITodoItemRepository TodoItems
        {
            get
            {
                if (todoItems == null)
                {
                    todoItems = new TodoItemRepository(_configuration);
                }
                return todoItems;
            }
        }
    }
}