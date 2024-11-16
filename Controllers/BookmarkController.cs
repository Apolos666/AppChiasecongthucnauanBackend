using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppChiaSeCongThucNauAnBackend.Models;
using AppChiaSeCongThucNauAnBackend.Services;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly ILogger<BookmarkController> _logger;

        public BookmarkController(IBookmarkService bookmarkService, ILogger<BookmarkController> logger)
        {
            _bookmarkService = bookmarkService;
            _logger = logger;
        }

        public IBookmarkService Get_bookmarkService()
        {
            return _bookmarkService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookmark(BookmarkCreateDto dto, IBookmarkService _bookmarkService)
        {
            try
            {
                return Ok(await _bookmarkService.CreateBookmarkAsync(dto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bookmark");
                return BadRequest("Failed to create bookmark");
            }
        }

        private IActionResult Ok(object v)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookmark(Guid id)
        {
            try
            {
                await _bookmarkService.RemoveBookmarkAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing bookmark");
                return BadRequest("Failed to remove bookmark");
            }
        }
    }
} 