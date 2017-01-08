using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Mikro.Core;
using Mikro.Core.Models;
using Mikro.Persistance.Extension;

namespace Mikro.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork uow;
        public CommentController(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        
        [Route("Post/Edit/{id:int}")]
        public ActionResult EditComment(int id)
        {
            var comment = uow.Comments.GetCommentById(id);
            
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

            var dbComment = uow.Comments.GetCommentById(comment.Id);

            dbComment.Content = comment.Content;
            dbComment.PostedContent = output;
            uow.SaveChanges();
            return RedirectToAction("Post", "Post", new {id = dbComment.PostId });
        }
      
        public ActionResult Delete(int id)
        {
            var comment = uow.Comments.GetCommentById(id);
            uow.Comments.Delete(comment);
            uow.SaveChanges();
            return RedirectToAction("Post", "Post", new { id = comment.PostId });
        }

        [Authorize]
        public ActionResult PlusComment(int id)
        {
            var userId = User.Identity.GetUserId();
            var comment = uow.Comments.GetCommentById(id);

            var postPlus = uow.CommentPluses.GetCommentPlus(userId, id);
           
            if (postPlus != null)
            {
                comment.PlusCounter -= 1;
                uow.CommentPluses.Delete(postPlus);
                uow.SaveChanges();
                return RedirectToAction("Post", "Post", new { id = comment.PostId });
            }
            var plus = new CommentPlus
            {
                CommentId = id,
                UserId = userId
            };

            comment.PlusCounter += 1;
            uow.CommentPluses.Add(plus);
            uow.SaveChanges();           
            return RedirectToAction("Post", "Post", new { id = comment.PostId });
        }
    }
}