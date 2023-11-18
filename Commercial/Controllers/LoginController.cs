using Commercial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commercial.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost("/autorisation")]
        public IActionResult verification(String matricule, String paswd)
        {
            Personnel personnel = new Personnel();
            bool perso = personnel.VerificationPersonnelByMatriculeAndPassword(matricule, paswd);
            Console.WriteLine("aona =" + perso);
            Console.WriteLine("matricule = " + matricule + " paswd = " + paswd);
            personnel = personnel.SearchPersonnelByMatriculeAndPassword(matricule, paswd);
            HttpContext.Session.SetInt32("idpersonne", personnel.idPersonnel);
            int idpersonne = int.Parse(HttpContext.Session.GetInt32("idpersonne").ToString());
            Console.WriteLine("Ito ilay mamorona azyyy "+idpersonne);
            Console.WriteLine("tsy azokooo "+personnel.idPersonnel);
            bool persoResp = personnel.VerificationPersonnelResponsableByMatriculeAndPassword(matricule, paswd); // rhf true ito d tkn ampotra le Layout Special
            if(perso == true)
            {
                HttpContext.Session.SetString("persoResp", persoResp.ToString());
                HttpContext.Session.SetString("personnel", personnel.ToString());
                return RedirectToAction("Index", "Besoin");
            }
            return RedirectToAction("Index");
        }

        [HttpPost("/deconnection")]
        public IActionResult deconnection()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
