using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;
using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IWearableRepository Wearables { get; }
        AzureImageStorage ImageStorage { get; }
        IUserRepository UserRepository { get; }
        UserManager<ApplicationUser> UserManager { get; }
        IComment CommentRepository { get; }
        Task<int> Complete();
    }
}
