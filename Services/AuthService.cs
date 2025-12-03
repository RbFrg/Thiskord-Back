using Microsoft.Data.SqlClient;
using BCrypt.Net;
using Thiskord_Back.Models.Auth;
using System.Diagnostics;
using System.Collections.Generic;
namespace Thiskord_Back.Services
{
    public class AuthService
    {
        private readonly IDbConnectionService _dbService;
        private readonly JsonService _jsonService;

        public AuthService(IDbConnectionService dbService, JsonService jsonService)
        {
            _dbService = dbService;
            _jsonService = jsonService;
        }
        public string AuthLogin(string user_auth, string user_password)
        {
            // requete en base pour chopper les infos utilisateurs : user_auth
            SqlConnection conn = _dbService.CreateConnection();
            conn.Open();
            string pwd = "";
            string query = "SELECT user_password FROM dbo.Account WHERE user_name = @user_auth";
            using var command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@user_auth", user_auth);
            using var res = command.ExecuteReader();
            res.Read();
            pwd = res.GetString(0);
            res.Close();
            // à partir du résultat, on compare le password en base avec : user_encrypted password
            bool ok = BCrypt.Net.BCrypt.Verify(user_password, pwd);
            // si true : 
            if (ok)
            {
                // requete pour aller chercher les infos utilisateur
                int user_id;
                query = "SELECT user_id, user_name, user_mail, user_picture FROM Account WHERE user_name = @user_auth;";
                using var command1 = new SqlCommand(query, conn);
                command1.Parameters.AddWithValue("@user_auth", user_auth);
                using var res1 = command1.ExecuteReader();
                res1.Read();
                user_id = res1.GetInt32(0);
                User user = new User(res1.GetString(1), res1.GetString(2), res1.GetString(3));
                res1.Close();
                query = "SELECT id_project_account FROM ACCESS WHERE id_account = @user_id;";
                using var command2 = new SqlCommand(query, conn);
                command2.Parameters.AddWithValue("@user_id", user_id);
                using var res2 = command2.ExecuteReader();
                List<int> projects = new List<int>();
                while (res2.Read())
                {
                    projects.Add(res2.GetInt32(0));
                }
                res2.Close();

                //return _jsonService.toJson(new AuthenticatedUser(user, projects));
                //return new AuthenticatedUser(user, projects.ToArray());
                return user_id.ToString();

                // requete pour aller chercher la liste de projets
                // return JsonContent(new AuthenticatedUser())
            }
            else
                //return "Y'a pas";
                return null;
            {

            //return _jsonService.toJson(new AuthenticatedUser());
            }
        }
    }
}
