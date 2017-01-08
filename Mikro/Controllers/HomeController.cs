using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Mikro.Core;
using Mikro.Core.ViewModels;

namespace Mikro.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork uow;

        public HomeController(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        
        public ActionResult Index()
        {            
            ViewBag.actualUserId = User.Identity.GetUserId();
            var user = User.Identity.GetUserId();

            var viewModel = new HomeViewModel
            {
                Posts = uow.Posts.GetAllPosts(),
                Comments = uow.Comments.GetComments(),
                Plus = uow.PostPluses.GetPostPlusByUser(user)
            };

            return View(viewModel);
        }
    }
}