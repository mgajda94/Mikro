using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

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
                PostPlus = _context.PostPluses.ToList(),
                UserId = actualUserId
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
                Content = viewmodel.Content
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Mikro");
        }

        [Route("Mikro/Post/{id:int}")]
        public ActionResult Post(int id)
        {

            ViewBag.actualUserId = User.Identity.GetUserId();

            var viewModel = new CommentFormViewModel { 
                

                Comments = _context.Comments
                .Where(x => x.PostId == id)
                .OrderBy(x=> x.PostedOn).ToList()
            };

            if (viewModel.Post == null)
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
                Content = viewModel.Content
            };

            _context.Comments.Add(comment);
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
            _context.Posts.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult PlusPost(int id)
        {
            var userId = User.Identity.GetUserId();

            var pluspost = _context.PostPluses
                .Where(x => x.PostId == id)
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var post = _context.Posts.Find(id);

            if (pluspost != null)
            {
                post.PlusCounter -= 1;
                _context.PostPluses.Remove(pluspost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var plus = new PostPlus
            {
                PostId = id,
                UserId = userId
            };
            
            post.PlusCounter += 1;
            _context.PostPluses.Add(plus);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult PlusComment(int id)
        {
            var userId = User.Identity.GetUserId();

            var pluscomment = _context.CommentPluses
                .Where(x => x.CommentId == id)
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            var comment = _context.Comments.Find(id);

            if (pluscomment != null)
            {
                comment.PlusCounter -= 1;
                _context.CommentPluses.Remove(pluscomment);
                _context.SaveChanges();
                return RedirectToAction("Post", new { id = comment.PostId});
            }

            var plus = new CommentPlus
            {
                CommentId = id,
                UserId = userId
            };

            comment.PlusCounter += 1;
            _context.CommentPluses.Add(plus);
            _context.SaveChanges();
            return RedirectToAction("Post", new { id = comment.PostId });
        }
    }    
}