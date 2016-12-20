using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System.Web.Mvc;
using Mikro.Extension;

namespace Mikro.Controllers
{
    public class CommentController : Controller
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

        [Route("Post/Edit/{id:int}")]
        public ActionResult EditComment(int id)
        {
            var comment = uow.Repository<Comment>().Select(x => x.Id == id);

            if (comment == null | comment.UserId != User.Identity.GetUserId())
                return HttpNotFound();
            return View(comment);
        }

        [HttpPost]
        public ActionResult EditComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var tagFunction = new TagFunction();

            IEnumerable<string> tags = Regex.Split(comment.Content, @"\s+").Where(i => i.StartsWith("#"));
            var output = tagFunction.TagToUrl(comment.Content, tags);

            var dbComment = uow.Repository<Comment>().Select(x => x.Id == comment.Id);
            dbComment.Content = comment.Content;
            dbComment.PostedContent = output;
            uow.SaveChanges();

            return RedirectToAction("Post", "Post", new {id = dbComment.PostId });
        }
      
        public ActionResult Delete(int id)
        {
            var comment = uow.Repository<Comment>().Select(x => x.Id == id);
            uow.Repository<Comment>().Delete(comment);
            uow.SaveChanges();

            return RedirectToAction("Post", "Post", new { id = comment.PostId });
        }

        [Authorize]
        public ActionResult PlusComment(int id)
        {
            var userId = User.Identity.GetUserId();
            var comment = uow.Repository<Comment>().Select(x => x.Id == id);

            var postPlus = uow.Repository<CommentPlus>().GetPlus(x => x.CommentId == id, x => x.UserId == userId);

            if (postPlus != null)
            {
                comment.PlusCounter -= 1;
                uow.Repository<CommentPlus>().Delete(postPlus);
                uow.SaveChanges();
                return RedirectToAction("Post", "Post", new { id = comment.PostId });
            }

            else
            {
                var plus = new CommentPlus
                {
                    CommentId = id,
                    UserId = userId
                };

                comment.PlusCounter += 1;
                uow.Repository<CommentPlus>().Add(plus);
                uow.SaveChanges();
            }

            return RedirectToAction("Post", "Post", new { id = comment.PostId });
        }
    }
}