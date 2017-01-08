using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mikro.Core;
using Mikro.Core.Models;
using Mikro.Core.ViewModels;

namespace Mikro.Controllers
{
    public class FollowingController : Controller
    {
        private readonly IUnitOfWork uow;

        public FollowingController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public ActionResult Index()
        {
            List<PostTag> postTags = new List<PostTag>();
            List<Post> Posts = new List<Post>();

            var userId = User.Identity.GetUserId();
            var following = uow.Followings.GetFollowings(userId);

            var viewModel = new HomeViewModel
            {
                Comments = uow.Comments.GetComments(),
                Plus = uow.PostPluses.GetPostPlusByUser(userId)
            };
            
            foreach (var tag in following)
                postTags.AddRange(uow.PostTags.GetPostTagsByTagId(tag.TagId));                          
            foreach (var post in postTags)
                 Posts.AddRange(uow.Posts.GetPostsById(post.PostId));
            foreach(var post in Posts)
                viewModel.Posts.Add(post);

            return View(viewModel);
        }       
    }
}