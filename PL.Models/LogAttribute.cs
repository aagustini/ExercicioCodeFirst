using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PL.Models
{
    public class LogAttribute : ActionFilterAttribute
    {
        private MovieContext movieDb = new MovieContext();

        public override void OnActionExecuting(ActionExecutingContext
        filterContext)
        {
            Log novo = new Log()
            {
                OperationDate = DateTime.Now,
                Action = filterContext.ActionDescriptor.ActionName,
                User = filterContext.HttpContext.User.Identity.Name,
                Message = "No additional information available."
            };

            if (filterContext.ActionParameters.ContainsKey("movie"))
            {
                var movie = filterContext.ActionParameters["movie"] as Movie;

                if (movie != null)
                    novo.Message = movie.Title;
            }
            else if (filterContext.ActionParameters.ContainsKey("id"))
            {
                var id = filterContext.ActionParameters["id"];

                if (id != null)
                    novo.Message = "filme id: " + id;
            }


            movieDb.Logs.Add(novo);
            movieDb.SaveChanges();
        }
    }
}
