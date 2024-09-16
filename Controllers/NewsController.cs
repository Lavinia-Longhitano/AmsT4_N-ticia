using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmsT4_BLOG_2.Infrastructure.Data.Context;
using AmsT4_BLOG_2.Models;

namespace AmsT4_BLOG_2.Controllers
{
    public class NewsController : Controller
    {
        private readonly DataContext _context;

        public NewsController(DataContext context)
        {
            _context = context;
        }

        // GET: News/IndexNews
        public async Task<IActionResult> IndexNews()
        {
            return View(await _context.News.ToListAsync());
        }

        // GET: News/DetailsNews/5
        public async Task<IActionResult> DetailsNews(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/CreateNews
        public IActionResult CreateNews()
        {
            return View();
        }

        // POST: News/CreateNews
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNews([Bind("Title,Description,Author,Category,ImageUrl,PublishedDate")] News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexNews));
            }
            return View(news);
        }

        // GET: News/EditNews/5
        public async Task<IActionResult> EditNews(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/EditNews/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNews(int id, [Bind("Id,Title,Description,Author,Category,ImageUrl,PublishedDate")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexNews));
            }
            return View(news);
        }

        // GET: News/DeleteNews/5
        public async Task<IActionResult> DeleteNews(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/DeleteNews/5
        [HttpPost, ActionName("DeleteNews")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexNews));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
