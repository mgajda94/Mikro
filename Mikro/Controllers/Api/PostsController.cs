using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Mikro.Dtos;
using Mikro.Extension;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;

namespace Mikro.Controllers.Api
{
    public class PostsController : ApiController
    {
        private UnitOfWork uow;
        private readonly ApplicationDbContext _context;
        public PostsController(ApplicationDbContext context)
        {
            _context = context;
            uow = new UnitOfWork(_context);
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddPost(AddingPostDto dto)
        {

            if (!ModelState.IsValid || dto.Content == null)
            {
                return BadRequest(ModelState);
            }

            var tagFunction = new TagFunction();
            IEnumerable<string> tags = Regex.Split(dto.Content, @"\s+")
                .Where(i => i.StartsWith("#"));
            var output = tagFunction.TagToUrl(dto.Content, tags);

            var post = new Post
            {
                UserId = User.Identity.GetUserId(),
                Username = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                Content = dto.Content,
                PlusCounter = 0,
                PostedContent = output
            };
            uow.Posts.Add(post);
            tagFunction.IsExist(tags, uow, post);
            uow.SaveChanges();
            return Ok();
        }
    }
}
