using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppChiaSeCongThucNauAnBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Services
{
    public interface IBookmarkService
    {
        Task<Bookmark> CreateBookmarkAsync(BookmarkCreateDto dto);
        Task RemoveBookmarkAsync(Guid id);
        Task<Bookmark> GetBookmarkByIdAsync(Guid id);
        Task<object> CreateBookmarkAsync(Controllers.BookmarkCreateDto dto);
    }
}
