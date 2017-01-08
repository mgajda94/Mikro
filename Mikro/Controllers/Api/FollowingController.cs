using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Mikro.Core;
using Mikro.Core.Dtos;
using Mikro.Core.Models;

namespace Mikro.Controllers.Api
{
    public class FollowingController : ApiController
    {
        private readonly IUnitOfWork uow;

        public FollowingController(IUnitOfWork _uow)
        {
            uow = _uow;
        }


        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public void Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var searchedTag = uow.Tags.GetTagByName(dto.TagName);

            //if (searchedTag == null)
                //return BadRequest(ModelState);

            var following = uow.Followings.GetFollowing(userId, searchedTag.Id);
            if (following == null)
            {
                var follow = new Following()
                {
                    UserId = userId,
                    TagId = searchedTag.Id
                };

                uow.Followings.Add(follow);
                uow.SaveChanges();
                //return Ok();
            }
            else
            {
                uow.Followings.Delete(following);
                uow.SaveChanges();
            }           
            //return Ok();
        }
    }
}
