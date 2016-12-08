using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Collections.Generic;

namespace Mikro.Controllers
{
    public class MikroController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MikroController()
        {
            _context = new ApplicationDbContext();
        }
        

        public ActionResult Index()
        {
            var actualUserId = User.Identity.GetUserId();
            ViewBag.actualUserId = actualUserId;

            var viewModel = new PostFormViewModel
            {
                Posts = _context.Posts.OrderByDescending(x => x.PostedOn).ToList(),
                PlusUsers = _context.Posts.SelectMany(x => x.PlusUsers).ToList(),
                Comment = _context.Posts.SelectMany(x => x.Comment).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PostFormViewModel viewmodel)
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
                Content = viewmodel.Content,
                PlusUsers = new List<ApplicationUser>(),
                Comment = new List<Comment>()
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Mikro");
        }

        [Route("Mikro/Post/{id:int}")]
        public ActionResult Post(int id)
        {
            var viewModel = new CommentFormViewModel { 
                Posts = _context.Posts.Where(x => x.Id == id)
                .ToList(),

                Comments = _context.Comments
                .Where(x => x.PostId == id)
                .OrderBy(x=> x.PostedOn).ToList()
            };

            if (viewModel.Posts == null)
                return HttpNotFound();

            return View(viewModel);
        }


        [Route("Mikro/Post/{id:int}")]
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
                Content = viewModel.Content,
                PlusCounter = 0,
            };

            var post = _context.Posts.Find(comment.PostId);

            if (post.Comment == null)
            {
                post.Comment = new List<Comment>();
            }

            _context.Comments.Add(comment);
            post.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Post", "Mikro");
        }

        [Route("Mikro/Post/Edit/{id:int}")]
        public ActionResult EditPost(int id, PostFormViewModel viewModel)
        {
            
            var post = _context.Posts.Find(id);

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

            var post = _context.Posts.Find(viewModel.Id);
            post.Content = viewModel.Content;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [Route("Mikro/Post/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            Post post = _context.Posts.Find(id);

            if (post == null | post.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            _context.Posts.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult PlusPost(int id)
        {
            var post = _context.Posts.Find(id);
            var user = _context.Users.Find(User.Identity.GetUserId());

            if (post == null | post.UserId != User.Identity.GetUserId())
                return HttpNotFound();

            if(post.PlusUsers == null)
            {
                post.PlusUsers = new List<ApplicationUser>();
            }

            if (post.PlusUsers.Contains(user))
            {
                post.PlusUsers.Remove(user);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            post.PlusUsers.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }    
}