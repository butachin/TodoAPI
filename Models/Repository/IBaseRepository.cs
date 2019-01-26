using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TodoApi.Models.Repository 
{
    public interface IBaseRepository<TEntity> 
    {
        Task<TEntity> FindAsync (String id);
        Task<IEnumerable<TEntity>> FindAsync ();
        Task Add (TEntity entity);
        Task Remove (TEntity entity);
        Task Update (TEntity entity);
        int Count ();
    }
}