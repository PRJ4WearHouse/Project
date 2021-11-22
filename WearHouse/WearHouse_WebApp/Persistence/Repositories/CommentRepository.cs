using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class CommentRepository : RepositoryEfCore<dbComments>, Interfaces.IComment
    {
        private readonly DbSet<dbComments> Comments_;
        public CommentRepository(DbContext context) : base(context)
        {
            Comments_ = context.Set<dbComments>();
        }

        public Task<List<dbComments>> GetdbCommentsOnWearable(int wearableId)
        {
            return Comments_.Where(c => c.WearableId == wearableId).ToListAsync();
        }

        //public Task<dbComments> GetCommentsOnWearable(int wearableId)
        //{
        //    return Comments_.Where(c => c.WearableId == wearableId).ToListAsync();
        //}
    }
}
