using Npgsql;

namespace Commercial.Models
{
    public class Produit
    {
        public int idProduit { get; set; }
        public string code { get; set; }
        public string nom { get; set; }

        public List<Produit> AllProduit()
        {
            ConnexionBDD connexion = new ConnexionBDD();
            List<Produit> produits = new List<Produit>();


            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select * from produit ";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produit produit = new Produit
                            {
                                idProduit = Convert.ToInt32(reader["idproduit"]),
                                code = Convert.ToString(reader["code"]),
                                nom = Convert.ToString(reader["nom"]),
                            };
                            produits.Add(produit);
                        }
                    }
                }
            }
            return produits;
        }
    }
}
