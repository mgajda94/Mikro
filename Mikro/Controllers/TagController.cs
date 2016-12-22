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
    [RoutePrefix("Tag")]
    public class TagController : Controller
    {
        private UnitOfWork uow ;
        public TagController()
        {
            uow = new UnitOfWork();
        }
        public TagController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        [Route("/{tagId:string}")]
        public ActionResult DisplayTagContent(string id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.actualUserId = userId;
            ViewBag.TagName = id;
            var expectedTag = uow.Repository<Tag>().Select(x => x.Name == id);

            var viewModel = new ViewModels.HomeViewModel()
            {
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == userId).ToList(),
                Posts = new List<Post>(),
                Tag = uow.Repository<Tag>().Select(x => x.Name == id),
                Following = uow.Repository<Following>().Select(x => x.UserId == userId && x.TagId == expectedTag.Id)
            };

            var postTag = uow.Repository<PostTag>().GetOverview(x => x.Tag == viewModel.Tag).ToList();
            ViewBag.Kant = postTag.Count();


            foreach (var post in postTag)
            {
                var eachPost = uow.Repository<Post>().Select(x => x.Id == post.PostId);
                viewModel.Posts.Add(eachPost);
            }

            viewModel.Posts.GroupBy(x => x.PostedOn);

            return View(viewModel);
        }
    }
}