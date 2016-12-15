using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System.Web.Mvc;

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
        public ActionResult EditComment(PostFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var comment = uow.Repository<Comment>().Select(x => x.Id == viewModel.Id);
            comment.Content = viewModel.Content;
            uow.SaveChanges();

            return RedirectToAction("Post", "Post", new {id = comment.PostId });
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