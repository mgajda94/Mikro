using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

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
            var viewModel = new PostFormViewModel
            {
                Posts = _context.Posts.ToList()
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
                PlusCounter = 0
            };

            

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Mikro");
        }

        [Route("Mikro/Post/{id:int}")]
        public ActionResult Post(int id)
        {
            var viewModel = new CommentFormViewModel { 
                Posts = _context.Posts.Where(x => x.Id == id).ToList(),
                Comments = _context.Comments.Where(x => x.PostId == id).OrderBy(x=> x.PostedOn).ToList()
            };

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

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Post", "Mikro");
        }
    }    
}