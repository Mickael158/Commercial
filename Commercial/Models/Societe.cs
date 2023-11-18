using Npgsql;

namespace Commercial.Models
{
    public class Societe
    {
        public int idSociete { get; set; }
        public String nom { get; set; }

        public String localisation { get; set; }

        public static List<Societe> AllSociete()
        {
            ConnexionBDD connexion = new ConnexionBDD();
            List<Societe> societes = new List<Societe>();


            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select * from societe ";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Societe societe = new Societe
                            {
                                idSociete = Convert.ToInt32(reader["idsociete"]),
                                nom = Convert.ToString(reader["nom"]),
                                localisation = Convert.ToString(reader["localisation"]),
                            };

                            societes.Add(societe);
                        }
                    }
                }
            }

            return societes;
        }

    }
}
