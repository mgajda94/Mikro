using Microsoft.AspNet.Identity;
using Mikro.Models;
using Mikro.Repository;
using System.Linq;
using System.Web.Mvc;

namespace Mikro.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork uow = null;
        public HomeController()
        {
            uow = new UnitOfWork();
        }
        public HomeController(UnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult Index()
        {
            ViewBag.actualUserId = User.Identity.GetUserId();

            var viewModel = new ViewModels.IndexViewModel
            {
                Posts = uow.Repository<Post>().GetOverview().OrderByDescending(x => x.PostedOn).ToList(),
                Comments = uow.Repository<Comment>().GetOverview().ToList()
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}