using Microsoft.AspNet.Identity;
using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mikro.ViewModels;
using System.Web.Security;

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

            return RedirectToAction("Index", "Home");
        }

        [Route("Mikro/Post/{id:int}")]
        public ActionResult Post(int id)
        {
            var viewModel = new PostFormViewModel
            {
                Posts = _context.Posts.Where(x => x.Id == id).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Mikro/Post/{id:int}")]
        public ActionResult Post(int id)
        {
            
        }
    }    
}