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
        public IActionResult getProduitToCheck(string[] selectedProducts, Dictionary<int, string> numeros)
        {
            BesoinService besoinService = new BesoinService();
            int idpersonne = HttpContext.Session.GetInt32("idpersonne") ?? 0; // Utilisation de ?? pour obtenir une valeur par défaut de 0 si la clé n'existe pas
            ValidationBesoin validation = new ValidationBesoin();
            DateTime dateAujourdhui = DateTime.Now;
            if (selectedProducts.Length != numeros.Count)
            {
                Console.WriteLine("tsy mitovy kyyy ny habehany ann");
            }

            foreach (var productId in selectedProducts)
            {
                if (int.TryParse(productId, out int productIdInt))
                {
                    if (numeros.ContainsKey(productIdInt))
                    {
                        string numero = numeros[productIdInt];
                        Console.WriteLine($"Produit sélectionné : {productIdInt}, Numéro : {numero}");
                        try
                        {
                            validation.InsererValidateBesoin(dateAujourdhui, numero, idpersonne);
                            int idproduit = int.Parse(productId);
                            besoinService.UpdateEtatProduit(productIdInt, idpersonne, numero);
                        }
                        catch (Exception ex)
                        {
                            // Gérez l'exception selon vos besoins
                            Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                        }
                    }
                }
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
