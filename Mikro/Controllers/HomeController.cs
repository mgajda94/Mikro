﻿using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Mikro.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow = null;
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

            var viewModel = new ViewModels.IndexViewModel
            {
                Posts = uow.Repository<Post>().GetOverview().OrderByDescending(x => x.PostedOn).ToList(),
                Comments = uow.Repository<Comment>().GetOverview().ToList(),
                Plus = uow.Repository<PostPlus>().GetOverview(x => x.UserId == user).ToList()
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

            var output = viewModel.Content;
            string href = "";
            string name = "";

            IEnumerable<string> tags = Regex.Split(viewModel.Content, @"\s+").Where(i => i.StartsWith("#"));
            
            foreach (var item in tags)
            {
                name = item.Replace("#", "");
                href = Url.Action("DisplayTagContent", "Tag", new { tagId = name });
                output = viewModel.Content
                    .Replace(item, "<a href='" + href + "'>" + item + "</a>");             
            }

            var post = new Post
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                Content = output
            };
            
            foreach (var item in tags)
            {
                name = item.Replace("#", "");

                var existingTag = uow.Repository<Tag>().Select(x => x.Name == item);

                if (existingTag == null)
                {
                    var tag = new Tag
                    {
                        Name = name,
                    };
                    tag.Posts.Add(post);
                    uow.Repository<Tag>().Add(tag);

                    var postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tag.Id
                    };
                    uow.Repository<Tag>().Add(tag);
                    uow.Repository<PostTag>().Add(postTag);
                }
                else
                {
                    existingTag.Posts.Add(post);
                    var postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = existingTag.Id
                    };
                    uow.Repository<PostTag>().Add(postTag);
                }
                
            }

            uow.Repository<Post>().Add(post);
            uow.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}