using Commercial.Models;
using Npgsql;

namespace Prestataire
{
    public class Proforma
    {
        public string produit {  get; set; }   
        public double qterestant { get; set; }
        public double pu { get; set; }


        public static List<Proforma> getProforma(string idprestataire, Besoin[] besoins)
        {
            List<Proforma> proformaList = new List<Proforma>();
            foreach(Besoin b in besoins) 
            {
                proformaList.Add(GetProformaProduit(idprestataire, b.produit));
                
            }

            return proformaList;
        }

        public static Proforma GetProformaProduit(string idprestataire, string nom)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            using (NpgsqlConnection connection = connexion.Connect())
            {
                string query = "SELECT * FROM v_etat_stock WHERE codeproduit=@code AND idfournisseur=@idprestataire";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@code", getCode(nom));
                    command.Parameters.AddWithValue("@idprestataire", idprestataire);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Proforma proforma = new Proforma
                            {
                                produit = nom,
                                qterestant = Convert.ToDouble(reader["qterestant"]),
                                pu = Convert.ToDouble(reader["pump"])
                            };

                            return proforma;
                        }
                    }

                }
            }
            return new Proforma
            {
                produit = nom,
                qterestant = 0,
                pu = 0
            };
        }


        public static string getCode(string Nom)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select code from produit WHERE nom=@nom";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nom", Nom);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToString(reader["code"]);
                        }
                    }
                }
            }

            return "";
        } 
    }
}
