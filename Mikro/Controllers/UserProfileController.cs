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
        private readonly ApplicationDbContext _context;
        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
            uow = new UnitOfWork(_context);
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