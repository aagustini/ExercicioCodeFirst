using MoviesWeb.ViewModels;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesWeb.Controllers
{
    public class GenreController : Controller
    {
            private MovieContext db = new MovieContext();

            // GET: Genre
            public ActionResult Index()
            {
                var genres = db.Genres.OrderBy(g => g.Name);
                return View(genres.ToList());
            }

            public ActionResult About()
            {
                var data = from movie in db.Movies
                           group movie by movie.Genre into dateGroup
                           select new GenreDataInfo()
                           {
                               GenreName = dateGroup.Key.Name,
                               GenreCount = dateGroup.Count(),
                               TotalGross = dateGroup.Sum(m => m.Gross),
                               AverageRating = dateGroup.Average(m => m.Rating)
                           };
                return View(data.OrderByDescending(g => g.GenreCount).ToList());
            }

            protected override void Dispose(bool disposing)
            {
                db.Dispose();
                base.Dispose(disposing);
            }
        }

    }