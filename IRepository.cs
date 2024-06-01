using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(string userEmail, TEntity entity);
        public TEntity GetById(int id);
        public IEnumerable<TEntity> GetAll(string userEmail);
        void Update(TEntity entity, string userEmail);
        bool Delete(int id,string userEmail);

    }
}