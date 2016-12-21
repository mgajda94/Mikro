using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;

namespace Mikro.Controllers
{
    public class UserProfileController : Controller
    {
        private UnitOfWork uow;

        public UserProfileController()
        {
                uow = new UnitOfWork();
        }

        public UserProfileController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult Index(string id)
        {
            ViewBag.Username = id;
            var userId = User.Identity.GetUserId();
            var viewModel = new HomeViewModel
            {
                Posts = uow.Repository<Post>().GetOverview(x=>x.Username == id).OrderByDescending(x => x.PostedOn).ToList(),
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == userId).ToList()
            };
            return View(viewModel);
        }

    }
}