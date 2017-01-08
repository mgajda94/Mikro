using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mikro.Core;
using Mikro.Core.ViewModels;
using Mikro.Persistance;

namespace Mikro.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUnitOfWork uow;
        public UserProfileController(IUnitOfWork _uow)
        {
            uow = _uow;
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
                Posts = uow.Posts.GetAllPostsByUsername(id),
                Comments = uow.Comments.GetComments(),
                Plus = uow.PostPluses.GetPostPlusByUser(userId)
            };
            return View(viewModel);
        }

    }
}