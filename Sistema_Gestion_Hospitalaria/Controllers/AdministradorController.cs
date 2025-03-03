using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: AdministradorController
        public ActionResult Index()
        {
            return View();
        }

    }
}
