using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Persistence.Core;
using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Persistence
{
    public class UnitOfWorkCreatingWearable : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWorkCreatingWearable(ApplicationDbContext context, string connString)
        {
            _context = context;
            Wearables = new WearableRepository(_context);
            ImageStorage = new AzureImageStorage(connString);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public WearableRepository Wearables { get; }
        public AzureImageStorage ImageStorage { get; }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
    }
}
