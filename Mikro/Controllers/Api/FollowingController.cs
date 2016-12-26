using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Mikro.Dtos;
using Mikro.Models;
using Mikro.Repository;

namespace Mikro.Controllers.Api
{
    public class FollowingController : ApiController
    {
        private UnitOfWork uow;

        public FollowingController()
        {
            uow = new UnitOfWork();
        }

        public FollowingController(UnitOfWork _uow)
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
            var searchedTag = uow.Repository<Tag>().Select(x => x.Name == dto.TagName);

            //if (searchedTag == null)
                //return BadRequest(ModelState);

            var following = uow.Repository<Following>().Select(x => x.UserId == userId && x.TagId == searchedTag.Id);
            if (following == null)
            {
                var follow = new Following()
                {
                    UserId = userId,
                    TagId = searchedTag.Id
                };

                uow.Repository<Following>().Add(follow);
                uow.SaveChanges();
                //return Ok();
            }
            else
            {
                uow.Repository<Following>().Delete(following);
                uow.SaveChanges();
            }           
            //return Ok();
        }
    }
}
