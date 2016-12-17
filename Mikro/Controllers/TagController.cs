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
            ViewBag.actualUserId = User.Identity.GetUserId();
            ViewBag.TagName = tagId;

            var viewModel = new ViewModels.IndexViewModel();
            var tag = uow.Repository<Tag>().Select(x => x.Name == tagId);
            foreach (var item in tag.PostsId)
            {
                viewModel.Posts = uow.Repository<Post>().GetOverview(x => x.Id == item).ToList(); ;
            }

            return View(viewModel);
        }
    }
}