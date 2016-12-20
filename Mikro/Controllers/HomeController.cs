using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Mikro.Extension;

namespace Mikro.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow;
        public HomeController()
        {
            uow = new UnitOfWork();
        }
        public HomeController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult Index()
        {            
            ViewBag.actualUserId = User.Identity.GetUserId();
            var user = User.Identity.GetUserId();

            var viewModel = new HomeViewModel
            {
                Posts = uow.Repository<Post>().GetOverview().OrderByDescending(x => x.PostedOn).ToList(),
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == user).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            var tagFunction = new TagFunction();          
            IEnumerable<string> tags = Regex.Split(viewModel.Content, @"\s+")
                .Where(i => i.StartsWith("#"));            
            var output = tagFunction.TagToUrl(viewModel.Content, tags);
            var post = new Post
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                Content = viewModel.Content,
                PlusCounter = 0,
                PostedContent = output
            };
            uow.Repository<Post>().Add(post);         
            tagFunction.IsExist(tags, uow, post);
            uow.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}