using Microsoft.AspNet.Identity;
using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mikro.ViewModels;

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
                Content = _context.Posts.ToList(),
            };

            return View(posts);
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
                PostedOn = DateTime.Now,
                Content = viewmodel.Content,
                PlusCounter = 0
            };


            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
     }    
}