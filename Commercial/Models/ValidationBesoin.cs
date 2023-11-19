using System;
using System.Collections.Generic;
using Npgsql;

namespace Commercial.Models
{
    public class ValidationBesoin
    {
        public DateTime date { get; set; }
        public BesoinService besoinservice { get; set; }
        public Service service { get; set; }
        public Personnel personnel { get; set; }
        public Produit produit { get; set; }

        public void InsererValidateBesoin(DateTime date,String numero_besoin_service,int idpersonnel)
        {
            ConnexionBDD connexion = new ConnexionBDD();

            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "INSERT INTO validate_besoin (date,numero_besoin_service,idpersonnel) VALUES (@date, @numero_besoin_service, @idpersonnel)";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@numero_besoin_service", numero_besoin_service);
                    command.Parameters.AddWithValue("@idpersonnel", idpersonnel);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ValidationBesoin> getListBesoinParResp(int id)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            List<ValidationBesoin> listavalider = new List<ValidationBesoin>();

            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select * from v_validation_by_resp where idresponsable_service = "+id+" and etat = 0";
                Console.WriteLine(sql);
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BesoinService besoinserv = new BesoinService
                            {
                                idbesoin_service = Convert.ToInt32(reader["idbesoin_service"]),
                                numero = Convert.ToString(reader["numero"]),
                                idproduit = Convert.ToInt32(reader["idproduit"]),
                                quantite = Convert.ToDouble(reader["qte"]),
                                etat = Convert.ToInt32(reader["etat"]),
                            };

                            Service service = new Service
                            {
                                idService = Convert.ToInt32(reader["idservice"]),
                                idResposable = Convert.ToInt32(reader["idresponsable_service"]),
                            };

                            Produit produit1 = new Produit
                            {
                                idProduit = Convert.ToInt32(reader["idproduit"]),
                                nom = Convert.ToString(reader["produit_nom"]),
                            };

                            ValidationBesoin validation = new ValidationBesoin
                            {
                                besoinservice = besoinserv,
                                service = service,
                                produit = produit1,
                            };

                            listavalider.Add(validation);
                        }
                    }
                }
            }
            return listavalider;
        }
    }
}
