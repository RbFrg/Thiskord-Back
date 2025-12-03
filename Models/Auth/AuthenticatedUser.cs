using System.Text.Json;
namespace Thiskord_Back.Models.Auth
{

    public class User
    {
        private string user_name { get; set; }
        private string user_mail { get; set; }
        private string user_picture { get; set; }

        public User(string _user_name, string _user_mail, string _user_picture)
        {
            this.user_name = _user_name;
            this.user_mail = _user_mail;
            this.user_picture = _user_picture;
        }
    }
    public class AuthenticatedUser
    {
        private User user { get; set; }
        private int[] servers;


        // constructeur avec objet User | On part du principe que l'on a fait appel au constructeur User en amont
        public AuthenticatedUser(User _user, int[] servers)
        {
            this.user = _user;
            this.servers = servers;
        }

    }

    public class AuthRequest
    {
        public string user_auth { get; set; }
        public string password { get; set; }
    }
}
