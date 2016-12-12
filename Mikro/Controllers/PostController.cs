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
    public class PostController : Controller
    {
        private UnitOfWork uow = null;

        public PostController()
        {
            uow = new UnitOfWork();
        }

        public PostController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult Index()
        {
            ViewBag.actualUserId = User.Identity.GetUserId();

            var viewModel = new PostFormViewModel
            {
                Posts = uow.Repository<Post>().GetOverview().OrderByDescending(x=>x.PostedOn).ToList()
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

            return RedirectToAction("Index", "Post");
        }

        [Route("Post/Edit/{id:int}")]
        public ActionResult EditPost(int id, PostFormViewModel viewModel)
        {
            var post = uow.Repository<Post>().Select(x=>x.Id == id);

            if (post == null | post.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            return View(post);
        }

        [HttpPost]
        public ActionResult EditPost(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var post = uow.Repository<Post>().Select(x=>x.Id == viewModel.Id);
            post.Content = viewModel.Content;
            uow.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("Post/{id:int}")]
        public ActionResult Post(int id)
        {
            ViewBag.actualUserId = User.Identity.GetUserId();

            var viewModel = new CommentFormViewModel
            {
                Post = uow.Repository<Post>().Select(x => x.Id == id),
                Comments = uow.Repository<Comment>()
                .GetOverview(x=>x.PostId == id)
                .OrderBy(x=>x.PostedOn)
                .ToList()
            };

            if (viewModel.Post == null)
                return HttpNotFound();

            return View(viewModel);
        }
  
        [HttpPost]
        public ActionResult Post(int id, CommentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var comment = new Comment
            {
                UserId = User.Identity.GetUserId(),
                PostId = id,
                UserName = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                Content = viewModel.Content
            };

            uow.Repository<Comment>().Add(comment);
            uow.SaveChanges();

            return RedirectToAction("Post", "Post");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = uow.Repository<Post>().Select(x => x.Id == id);
            uow.Repository<Post>().Delete(post);
            uow.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}