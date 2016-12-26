﻿using System;
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
    public class CommentController : ApiController
    {
        private UnitOfWork uow = null;
        public CommentController()
        {
            uow = new UnitOfWork();
        }
        public CommentController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        [HttpPost]
        public IHttpActionResult AddComent(CommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = uow.Repository<Post>().Select(x => x.Id == dto.PostId);
            var tagFunction = new TagFunction();

            IEnumerable<string> tags = Regex.Split(dto.Content, @"\s+").Where(i => i.StartsWith("#"));

            var output = tagFunction.TagToUrl(dto.Content, tags);
            var comment = new Comment
            {
                UserId = User.Identity.GetUserId(),
                PostId = dto.PostId,
                UserName = User.Identity.GetUserName(),
                PostedOn = DateTime.Now,
                PostedContent = output,
                Content = dto.Content,
                Post = post
            };

            uow.Repository<Comment>().Add(comment);
            uow.SaveChanges();
            return Ok();
        }
    }
}
