using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoviesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void Agente()
        {
            Response.Write("<h3>Hello MVC!</h3>");
            Response.Write("<ul> <li>Welcome, you are using "+ Request.Browser.Browser);
            Response.Write(" and your address is "+ Request.UserHostAddress + "! </li>");
            Response.Write("<li>User agent: " + Request.UserAgent + "</li>");
            Response.Write("<li>httpMethod: " + Request.HttpMethod + "</li>");
            foreach(String s in Request.AcceptTypes)
            {
                Response.Write("<li>AcceptTypes: " + s + "</li>");
            }
            foreach (String s in Request.UserLanguages)
            {
                Response.Write("<li>User languages: " + s + "</li>");
            }

            Response.Write("</ul>");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserData()
        {
            if (!Request.IsAuthenticated)
            {
                return Content("Not Authenticated");
            }

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;

            return Content("Id: " + User.Identity.Name + "  Name: " + user.FullName);

        }

    }
}