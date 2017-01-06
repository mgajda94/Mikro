using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using Mikro.ViewModels;
using System.Web.Mvc;

namespace Mikro.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow;
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            uow = new UnitOfWork(_context);          
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