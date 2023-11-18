using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Prestataire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProformaController : ControllerBase
    {
        [HttpPost("{idprestataire}")]
        public IActionResult DemandeProforma(string idprestataire, [FromBody] Besoin[] besoins)
        {
            return Ok(Proforma.getProforma(idprestataire, besoins));
        }
    }
}
