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

        private readonly ApplicationDbContext _context;
        private UnitOfWork uow;

        public TagController()
        {
            uow = new UnitOfWork(_context);
            _context = new ApplicationDbContext();
        }       

        [Route("/{tagId:string}")]
        public ActionResult DisplayTagContent(string id)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.actualUserId = userId;
            ViewBag.TagName = id;
          
            var viewModel = new HomeViewModel()
            {
                Comments = uow.Comments.GetComments(),
                Plus = uow.PostPluses.GetPostPlusByUser(userId),
                Posts = new List<Post>(),
                Tag = uow.Tags.GetTagByName(id)
            };

            if (viewModel.Tag == null)
            {
                return RedirectToAction("Index", "Home");
            }
            viewModel.Following = uow.Followings.GetFollowing(userId, viewModel.Tag.Id);
            
            var postTag = uow.PostTags.GetPostTagsByTag(viewModel.Tag);
           
            foreach (var post in postTag)
            {
                var eachPost = uow.Posts.GetPost(post.PostId);
                viewModel.Posts.Add(eachPost);
            }

            viewModel.Posts.GroupBy(x => x.PostedOn);

            return View(viewModel);
        }
    }
}