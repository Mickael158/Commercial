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


        [HttpPost("/besoinService")]
        public IActionResult InsertBesoinByService(string numero, String idservice, String[] produit, String[] qte)
        {
            BesoinService besoinService = new BesoinService();
            for (int i = 0; i < produit.Length; i++)
            {
                string idProduit = produit[i];
                string quantite = qte[i];
                besoinService.getInsererBesoinService(numero , idservice , idProduit, quantite);
            }
            return RedirectToAction("Index");
        }


        public IActionResult ValidationBesoin()
        {
            ValidationBesoin validation = new ValidationBesoin();
            int idpersonne = int.Parse(HttpContext.Session.GetInt32("idpersonne").ToString());
            Console.WriteLine("idpersonne = " + idpersonne);
            List<ValidationBesoin> validations = validation.getListBesoinParResp(idpersonne);
            ViewBag.ValidationBesoin = validations;
            return View("ValidationBesoin");
        }


        [HttpPost("/getProduitToCheck")]
        public IActionResult getProduitToCheck(List<int> selectedProducts, List<string> numeros)
        {
            BesoinService besoinService = new BesoinService();
            int idpersonne = int.Parse(HttpContext.Session.GetInt32("idpersonne").ToString());
            ValidationBesoin validation = new ValidationBesoin();
            DateTime dateAujourdhui = DateTime.Now;
            // Vérifier que les deux listes ont la même taille
            Console.WriteLine("taille 1 = " + selectedProducts.Count);
            Console.WriteLine("taille 2 = " + numeros.Count);

            for (int i = 0; i < selectedProducts.Count; i++)
            {
                int productId = selectedProducts[i];
                Console.WriteLine("Produit sélectionné : " + productId);
                besoinService.UpdateEtatProduit(productId, idpersonne);
            }
            for(int j = 0; j < numeros.Count; j++)
            {
                string numero = numeros[j];
                Console.WriteLine("Numero : " + numero);
                validation.InsererValidateBesoin(dateAujourdhui, numero, idpersonne);
            }
            return RedirectToAction("ValidationBesoin");
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
