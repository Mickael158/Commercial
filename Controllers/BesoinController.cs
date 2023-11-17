using Commercial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Commercial.Controllers
{
    public class BesoinController : Controller
    {
        private readonly ILogger<BesoinController> _logger;

        public BesoinController(ILogger<BesoinController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            Produit produit = new Produit();
            List<Produit> produits = produit.AllProduit();

            Service service = new Service();
            List<Service> services = service.AllService();

            ViewBag.Produits = produits;
            ViewBag.Services = services;

            return View("Index");
        }
        [HttpGet("/besoinService")]
        public IActionResult InsertBesoinByService(string numero, String idservice, String idproduit, String qte)
        {
            BesoinService besoinService = new BesoinService();
            besoinService.getInsererBesoinService(numero , idservice , idproduit , qte);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
