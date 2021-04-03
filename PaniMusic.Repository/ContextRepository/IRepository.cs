using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Repository.ContextRepository
{
    public interface IRepository<TModelRepository>
    {
        Task<TModelRepository> Get(int id);

        Task<List<TModelRepository>> GetAll();

        //Because the insert , delete & update methods need the save method, I return them as void(not Task).

        void Insert(TModelRepository insertInput);

        void Update(TModelRepository inputUpdate);

        void Delete(int id);

        IQueryable<TModelRepository> GetQuery();

        Task Save();
    }
}
