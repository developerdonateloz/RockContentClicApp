using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ClicLikes.Models;

namespace Project_ClicLikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DataContext _context;

        public ArticlesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public List<ArticleView> GetArticle()
        {
            List<ArticleView> listado = new List<ArticleView>();
            listado = (from a in _context.Articles
                       join tl in
                       (
                            from l in _context.Likes
                            group l by l.ArticleId into grouped
                            select new
                            {
                                Id = grouped.Key,
                                Conteo = grouped.Count()
                            }
                       ) on a.Id equals tl.Id into sr1
                       from z in sr1.DefaultIfEmpty()
                       select new ArticleView
                       {
                           Id = a.Id,
                           Content = a.Content,
                           Creation = a.Creation,
                           NumberLikes = z.Conteo
                       }
                      ).ToList();
            return listado;
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return article;
        }
    }
}
