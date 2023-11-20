using Npgsql;

namespace Commercial.Models
{
    public class Fournisseur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Code { get; set; }


        public static List<Fournisseur> getDemande(string numero)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            List<Fournisseur> list = new List<Fournisseur>();
            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "SELECT * FROM fournisseur";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@numero", numero);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Fournisseur bd = new Fournisseur
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Code = reader.GetString(2)
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
