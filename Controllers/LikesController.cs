using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ClicLikes.Models;

namespace Project_ClicLikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly DataContext _context;

        public LikesController(DataContext context)
        {
            _context = context;
        }

        ///[Route("api/[controller]/PostLike")]
        [RequestRateLimit(Name = "Limit Request", Seconds = 5)]
        [HttpPost]
        public List<ArticleView> PostLike(LikeView minilike)
        {
            int articleid = minilike.ArticleId;

            Like olike = new Like { ArticleId = articleid, Creation = DateTime.UtcNow.AddHours(-5), UserCode = "XYZ" };
            _context.Likes.Add(olike);
            _context.SaveChanges();

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
    }
}
