using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class UserRepository : RepositoryEfCore<ApplicationUser>, IUserRepository
    {
        private readonly DbSet<ApplicationUser> _entities;
        public UserRepository(DbContext context) : base(context)
        {
            _entities = context.Set<ApplicationUser>();
        }

        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        //OBS Should be a way to override the Get(int id) function, since userID is a string, and it will therefore not work.
        public Task<ApplicationUser> GetUserWithWearables(string id)
        {
            return DbContext.Users.Include(u => u.Wearables).SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
