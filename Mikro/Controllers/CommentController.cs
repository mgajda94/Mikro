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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment = uow.Repository<Comment>().Select(x => x.Id == id);
            uow.Repository<Comment>().Delete(comment);
            uow.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult PlusPost(int id)
        {
            var userId = User.Identity.GetUserId();
            var comment = uow.Repository<Post>().Select(x => x.Id == id);

            var postPlus = uow.Repository<PostPlus>().GetPlus(x => x.PostId == id, x => x.UserId == userId);

            if (postPlus != null)
            {
                comment.PlusCounter -= 1;
                uow.Repository<PostPlus>().Delete(postPlus);
                uow.SaveChanges();
                return RedirectToAction("Index", "Post");
            }

            else
            {
                var plus = new PostPlus
                {
                    PostId = id,
                    UserId = userId
                };

                comment.PlusCounter += 1;
                uow.Repository<PostPlus>().Add(plus);
                uow.SaveChanges();
            }

            return RedirectToAction("Index", "Post");
        }
    }
}