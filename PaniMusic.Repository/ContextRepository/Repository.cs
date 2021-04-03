using Microsoft.EntityFrameworkCore;
using PaniMusic.Core.DatabaseContext;
using PaniMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaniMusic.Repository.ContextRepository
{
    public class Repository<TModelRepository> : IRepository<TModelRepository> where TModelRepository : class, IEntity
    {
        private readonly PaniMusicDbContext paniMusicDbContext;

        public Repository(PaniMusicDbContext paniMusicDbContext)
        {
            this.paniMusicDbContext = paniMusicDbContext;
        }

        public async Task<TModelRepository> Get(int id)
        {
            var getItem = await this.paniMusicDbContext
                .Set<TModelRepository>()
                .FirstOrDefaultAsync(get => get.Id == id);

            if (getItem == null)
                return null;

            return getItem;
        }

        public async Task<List<TModelRepository>> GetAll()
        {
            var getAllItems = await this.paniMusicDbContext
                .Set<TModelRepository>()
                .ToListAsync();

            if (getAllItems == null)
                return null;

            return getAllItems;
        }

        public void Insert(TModelRepository insertInput)
        {
            this.paniMusicDbContext.Add<TModelRepository>(insertInput);
        }

        public void Update(TModelRepository inputUpdate)
        {
            this.paniMusicDbContext.Update<TModelRepository>(inputUpdate);
        }

        public void Delete(int id)
        {
            var itemForDelete = this.paniMusicDbContext
                .Set<TModelRepository>()
                .FirstOrDefault(getItem => getItem.Id == id);

            paniMusicDbContext.Remove(itemForDelete);
        }

        public IQueryable<TModelRepository> GetQuery()
        {
            return paniMusicDbContext.Set<TModelRepository>().AsQueryable();
        }

        public Task Save()
        {
            return paniMusicDbContext.SaveChangesAsync();
        }
    }
}
