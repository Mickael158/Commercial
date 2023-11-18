using Npgsql;
using System.Data;

namespace Commercial.Models
{
    public class Service
    {
        public int idService { get; set; }
        public int idSociete { get; set; }
        public string nom { get; set; }
        public int idResposable { get; set; }

        public List<Service> AllService()
        {
            ConnexionBDD connexion = new ConnexionBDD();
            List<Service> services = new List<Service>();


            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select * from service ";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Service service = new Service
                            {
                                idService = Convert.ToInt32(reader["idservice"]),
                                idSociete = Convert.ToInt32(reader["idsociete"]),
                                nom = Convert.ToString(reader["nom"]),
                                idResposable = Convert.ToInt32(reader["idresponsable"]),
                            };

                            services.Add(service);
                        }
                    }
                }
            }
            return services;
        }
        public Service SearchResponsableServiceByResponsable(int idPersonnne)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            Service service = new Service();


            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select * from service WHERE idresponsable="+idPersonnne;
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            service.idService = Convert.ToInt32(reader["idservice"]);
                            service.idSociete = Convert.ToInt32(reader["idsociete"]);
                            service.nom = Convert.ToString(reader["nom"]);
                            service.idResposable = Convert.ToInt32(reader["idresponsable"]);
                        }
                    }
                }
            }
            return service;
        }
    }
}
