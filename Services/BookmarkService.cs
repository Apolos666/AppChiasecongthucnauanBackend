using Microsoft.EntityFrameworkCore;
using AppChiaSeCongThucNauAnBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppChiaSeCongThucNauAnBackend.Services;
using AppChiaSeCongThucNauAnBackend.Data;

namespace AppChiaSeCongThucNauAnBackend.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly AppDbContext _context;

        public BookmarkService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bookmark> CreateBookmarkAsync(BookmarkCreateDto dto)
        {
            var bookmark = new Bookmark
            {
                Id = Guid.NewGuid(),
                RecipeId = dto.RecipeId,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();
            
            return bookmark;
        }

        public async Task RemoveBookmarkAsync(Guid id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Bookmark> GetBookmarkByIdAsync(Guid id)
        {
            return await _context.Bookmarks
                .Include(b => b.Recipe)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public Task<object> CreateBookmarkAsync(Controllers.BookmarkCreateDto dto)
        {
            throw new NotImplementedException();
        }
    }
} 