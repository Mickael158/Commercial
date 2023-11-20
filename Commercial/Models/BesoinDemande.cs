using Newtonsoft.Json;
using Npgsql;
using System.Text.Json.Nodes;

namespace Commercial.Models
{
    public class BesoinDemande
    {
        public string produit { get; set; }
        public double qte { get; set; }

        public static async Task sendDemandeAsync(List<BesoinDemande> list, string codefournisseur)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:1784/api/Proforma/{codefournisseur}";
                HttpResponseMessage response = await client.PostAsJsonAsync(url, list);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JsonArray result = JsonConvert.DeserializeObject<JsonArray>(jsonResponse);
                }
            }
        }

        public static List<BesoinDemande> getDemande(string numero)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            List<BesoinDemande> list = new List<BesoinDemande>();
            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "SELECT p.nom, bs.qte FROM besoin_service bs JOIN produit p ON bs.idproduit = p.idproduit WHERE bs.numero = @numero and bs.etat=11";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", numero);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BesoinDemande bd = new BesoinDemande
                            {
                                produit = reader.GetString(0),
                                qte = reader.GetDouble(1)
                            };
                            list.Add(bd);
                        }
                    }
                }
            }
            return list;
        }
    }
}
