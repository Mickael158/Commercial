using Npgsql;

namespace Commercial.Models
{
    public class Personnel
    {
        public int idPersonnel { get; set; }
        public string matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string password { get; set; }
        public Role rolep { get; set; }

        public Personnel SearchPersonnelByMatriculeAndPassword(string matricule , string password)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            Personnel personnel = new Personnel();


            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "select * from personnel WHERE matricule='"+matricule+ "' AND pass='"+password+"'";
                Console.WriteLine(sql);
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            personnel.idPersonnel = Convert.ToInt32(reader["id"]);
                            personnel.matricule = Convert.ToString(reader["matricule"]);
                            personnel.nom = Convert.ToString(reader["nom"]);
                            personnel.prenom = Convert.ToString(reader["prenom"]);
                            personnel.password = Convert.ToString(reader["pass"]);
                        }
                    }
                }
            }
            return personnel;
        }

        public bool VerificationPersonnelByMatriculeAndPassword(string matricule, string password)
        {
            bool result = false;
            Personnel personnel = SearchPersonnelByMatriculeAndPassword(matricule , password);
            if(personnel.idPersonnel != 0)
            {
                result = true;
            }
            return result;
        }

        public bool VerificationPersonnelResponsableByMatriculeAndPassword(string matricule, string password)
        {
            bool result = false;
            Personnel personnel = SearchPersonnelByMatriculeAndPassword(matricule, password);
            Service service1 = new Service();
            if (personnel.idPersonnel != 0)
            {
                Service service = service1.SearchResponsableServiceByResponsable( personnel.idPersonnel);
                if (service.idResposable != 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public Personnel getRoleParPersonnel(String matricule,String password)
        {
            ConnexionBDD connexion = new ConnexionBDD();
            Personnel personnel1 = new Personnel();

            using (NpgsqlConnection connection = connexion.Connect())
            {
                string sql = "SELECT personnel.id, personnel.matricule,personnel.nom,personnel.prenom,personnel.pass, role.etat FROM personnel JOIN role ON personnel.matricule = role.matricule where personnel.matricule = '"+matricule+"' and personnel.pass = '"+password+"';";
                Console.WriteLine(sql);
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role role2 = new Role
                            {
                                etat = Convert.ToInt32(reader["etat"]),
                            };
                            Personnel personnel = new Personnel
                            {
                                idPersonnel = Convert.ToInt32(reader["id"]),
                                matricule = Convert.ToString(reader["matricule"]),
                                nom = Convert.ToString(reader["nom"]),
                                prenom = Convert.ToString(reader["prenom"]),
                                password = Convert.ToString(reader["pass"]),
                                rolep = role2,
                            };
                            personnel1 = personnel;
                        }
                    }
                }
            }
            return personnel1;
        }
    }
}
