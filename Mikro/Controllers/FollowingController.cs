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
    public class FollowingController : Controller
    {
        private UnitOfWork uow;

        public FollowingController()
        {
            uow = new UnitOfWork();
        }

        public FollowingController(UnitOfWork _uow)
        {
            uow = _uow;
        }

        public ActionResult Index()
        {
            List<PostTag> postTags = new List<PostTag>();
            List<Post> Posts = new List<Post>();
            var userId = User.Identity.GetUserId();
            var following = uow.Repository<Following>().GetOverview(x => x.UserId == userId).ToList();
            var viewModel = new HomeViewModel
            {
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == userId).ToList()
            };
            
            foreach (var tag in following)
                postTags.AddRange(uow.Repository<PostTag>().GetOverview(x => x.TagId == tag.TagId));               

            foreach (var post in postTags)
                 Posts.AddRange(uow.Repository<Post>().GetOverview(x => x.Id == post.PostId));

            foreach(var post in Posts)
                viewModel.Posts.Add(post);

            return View(viewModel);
        }       
    }
}