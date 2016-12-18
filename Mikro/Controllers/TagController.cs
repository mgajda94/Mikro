using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mikro.Controllers
{
    public class TagController : Controller
    {
        private UnitOfWork uow = null;
        public TagController()
        {
            uow = new UnitOfWork();
        }
        public TagController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult DisplayTagContent(string tagId)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.actualUserId = userId;
            ViewBag.TagName = tagId;

            var viewModel = new ViewModels.IndexViewModel()
            {
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == userId).ToList(),
                Posts = new List<Post>(),
                Tag = uow.Repository<Tag>().Select(x => x.Name == tagId)
            };

            var postTag = uow.Repository<PostTag>().GetOverview(x => x.TagId == viewModel.Tag.Id).ToList();

            foreach (var post in postTag)
            {
                viewModel.Posts = uow.Repository<Post>().GetOverview(x => x.Id == post.PostId).ToList();
            }

            if (postTag == null)
                return View();

            return View(viewModel);
        }
    }
}