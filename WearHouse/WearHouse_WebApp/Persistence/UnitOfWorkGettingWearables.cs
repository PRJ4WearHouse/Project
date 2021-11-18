using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;
using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Persistence
{
    public class UnitOfWorkGettingWearables : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWorkGettingWearables(ApplicationDbContext context, UserManager<ApplicationUser> userManager, string azureConnString)
        {
            _context = context;
            Wearables = new WearableRepository(_context);
            ImageStorage = new AzureImageStorage(azureConnString);
            UserRepository = new UserRepository(_context);
            UserManager = userManager;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IWearableRepository Wearables { get; }
        public AzureImageStorage ImageStorage { get; }
        public IUserRepository UserRepository { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
    }
}
