using TodoApi.Models.Repository;

namespace TodoApi.Models.Persistance
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoITems { get;}
    }
}