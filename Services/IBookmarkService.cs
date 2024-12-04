using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppChiaSeCongThucNauAnBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Services
{
    public interface IBookmarkService
    {
        Task<bool> AddBookmarkAsync(Guid userId, Guid recipeId);
        Task<bool> RemoveBookmarkAsync(Guid userId, Guid recipeId);
        Task<IEnumerable<object>> GetBookmarksAsync(Guid userId);
        Task<bool> CheckBookmarkStatusAsync(Guid userId, Guid recipeId);
    }
}
