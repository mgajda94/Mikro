using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Mikro.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow = null;
        public HomeController()
        {
            uow = new UnitOfWork();
        }
        public HomeController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult Index()
        {            
            ViewBag.actualUserId = User.Identity.GetUserId();
            var user = User.Identity.GetUserId();

            var viewModel = new ViewModels.IndexViewModel
            {
                Posts = uow.Repository<Post>().GetOverview().OrderByDescending(x => x.PostedOn).ToList(),
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == user).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var post = new Post
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                Content = viewModel.Content
            };

            uow.Repository<Post>().Add(post);
            uow.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}