using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.Models.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity :IBaseEntity
    {
        public Task Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public int Count()
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> FindAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}