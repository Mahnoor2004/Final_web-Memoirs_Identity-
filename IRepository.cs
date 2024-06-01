using System.Collections.Generic;

namespace WebApplication1.Models
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(string userEmail, TEntity entity);
        //TEntity Get(int id);
        //IEnumerable<TEntity> GetAll();
        public IEnumerable<TEntity> GetAll(string userEmail);
        void Update(TEntity entity);
        void Delete(int id);
    }
}