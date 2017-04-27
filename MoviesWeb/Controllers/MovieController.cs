using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PL.Models;

namespace MoviesWeb.Controllers
{
    public class MovieController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Movie
        //public ActionResult Index()
        //{
        //    var movies = db.Movies.Include(m => m.Genre);
        //    return View(movies.ToList());
        //}
        public ViewResult Index(string searchString, string sortOrder, int? SelectedGenre)
        {
            var genres = db.Genres.OrderBy(g => g.Name).ToList();
            ViewBag.SelectedGenre = new SelectList(genres, "GenreID", "Name", SelectedGenre);
            int genreID = SelectedGenre.GetValueOrDefault();

            var movies = db.Movies
                .Where(c => !SelectedGenre.HasValue || c.GenreID == genreID);

            //var movies = from movie in movieDb.Movies
            //               select movie;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "rating_asc" : "";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_asc" : "rating_desc";

            switch (sortOrder)
            {
                case "rating_desc":
                    movies = movies.OrderByDescending(s => s.Rating);
                    break;
                case "rating_asc":
                    movies = movies.OrderBy(s => s.Rating);
                    break;
            }

            return View(movies);
        }


        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movie/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogAttribute]
        public ActionResult Create([Bind(Include = "ID,Title,Director,ReleaseDate,Gross,Rating,GenreID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", movie.GenreID);
            return View(movie);
        }

        // GET: Movie/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", movie.GenreID);
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[LogAttribute]
        //public ActionResult Edit([Bind(Include = "ID,Title,Director,ReleaseDate,Gross,Rating,GenreID")] Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(movie).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", movie.GenreID);
        //    return View(movie);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogAttribute]
        public ActionResult Edit(int id, string title, string director,
                                 DateTime releaseDate, decimal gross,
                                 double rating, string imageUrl, int genreID,
                                 HttpPostedFileBase image)

        {
            var movie = db.Movies.Find(id);
            if (ModelState.IsValid && movie != null)
            {
                movie.Title = title;
                movie.Director = director;
                movie.ReleaseDate = releaseDate;
                movie.Gross = gross;
                movie.Rating = rating;
                movie.ImageUrl = imageUrl;
                movie.GenreID = genreID;
                //if image object is not empty update the photo attribute, using image info.
                if (image != null)
                {
                    movie.ImageMimeType = image.ContentType;
                    movie.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(movie.ImageFile, 0, image.ContentLength);
                }
                
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", movie.GenreID);
            return View(movie);
        }

        // GET: Movie/Delete/5
        [Authorize(Users = "admin@psa.br")]
        [LogAttribute]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@psa.br")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
       // [Authorize(Users = "admin@psa.br")]
        public ActionResult RemoveAjax(int id)
        {
            var movie = db.Movies.Find(id);
            if (movie == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            string movieName = movie.Title;

            db.Movies.Remove(movie);
            db.SaveChanges();

            var results = new
            {
                Message = movieName + "has been removed.",
                DeleteId = id
            };
            return Json(results);

        }



        public ActionResult Browse(string genre = "Action")
        {
            var genreModel = db.Genres.Include("Movies").Single(g => g.Name == genre);

            return View(genreModel);
        }

        public ActionResult GetImage(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie != null && movie.ImageFile != null)
            {
                return File(movie.ImageFile, movie.ImageMimeType);
            }
            else
            {
                return new FilePathResult("~/Images/nao-disponivel.jpg", "image/jpeg");
            }
        }


        public ActionResult Catalogo(string titulo)
        {
            string filePath = Server.MapPath("~/Content/Catalogo/") + titulo.ToLower() + ".pdf";

            if (System.IO.File.Exists(filePath))
                return new FilePathResult(filePath, "application/pdf");
            else
                return HttpNotFound();
        }


        public JsonResult Filmes()
        {
            // atenção: este código é apenas um exemplo;
            // ver possíveis vulnerabilidades em:
            // http://msdn.microsoft.com/query/dev11.query?appId=Dev11IDEF1&l=EN-US&k=k(System.Web.Mvc.JsonRequestBehavior);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5);k(DevLang-csharp)&rd=true
            // http://haacked.com/archive/2008/11/20/anatomy-of-a-subtle-json-vulnerability.aspx
            // http://msdn.microsoft.com/en-us/library/hh404095.aspx

            var model = from movie in db.Movies
                        select new
                        {
                            Titulo = movie.Title,
                            Ano = movie.ReleaseDate.Year,
                            Genero = movie.Genre.Name,
                            Avaliacao = movie.Rating
        };
           return Json(model.OrderBy(m => m.Ano), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Users = "admin@psa.br")]
        public ActionResult ViewLog()
        {
            var logs = db.Logs;
            return View(logs.ToList());
        }

        [ChildActionOnly]
        public ActionResult GenreMenu(int num = 5)
        {
            var genres = db.Genres
                               .OrderByDescending(g => g.Movies.Count)
                               .Take(num)
                               .ToList();

            return this.PartialView(genres);
        }

        public ActionResult MovieFilter(string term)
        {
            term = term.ToLower();
            var movies = from movie in db.Movies
                         where (movie.Title.ToLower().Contains(term))
                         select movie.Title;
            return Json(movies, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
