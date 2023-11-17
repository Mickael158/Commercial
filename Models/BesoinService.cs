using Npgsql;

namespace Commercial.Models
{
    public class BesoinService
    {
        public int idbesoin_service { get; set; }
        public String numero { get; set; }

        public int idservice { get; set; }
        public int idproduit { get; set; }
        public double quantite { get; set; }

        public void getInsererBesoinService(String numero, String idservice , String idproduit , String qte)
        {
            BesoinService besoinService = new BesoinService();
            besoinService.numero = numero;
            besoinService.idservice = int.Parse(idservice);
            besoinService.idproduit = int.Parse(idproduit);
            besoinService.quantite = double.Parse(qte);
            besoinService.InsererBesoinService(besoinService);
        }

        public void InsererBesoinService(BesoinService besoinService )
        {
            ConnexionBDD connexion = new ConnexionBDD();

            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "INSERT INTO besoin_service (numero, idservice , idproduit , qte) VALUES (@numero, @idservice , @idproduit , @qte)";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", besoinService.numero);
                    command.Parameters.AddWithValue("@idservice", besoinService.idservice);
                    command.Parameters.AddWithValue("@idproduit", besoinService.idproduit);
                    command.Parameters.AddWithValue("@qte", besoinService.quantite);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
