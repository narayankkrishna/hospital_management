using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients.Login
{
    public class IndexModel : PageModel
    {

        public List<LoginInfo> listLogin = new List<LoginInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoginInfo loginInfo = new LoginInfo();
                                loginInfo.id = "" + reader.GetInt32(0);
                                loginInfo.name = reader.GetString(1);
                                loginInfo.email = reader.GetString(2);
                                loginInfo.password = reader.GetString(3);
                                loginInfo.created_at = reader.GetDateTime(8).ToString();

                                listLogin.Add(loginInfo);

                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }
        }
    }

    public class LoginInfo
    {
        public String id;
        public String name;
        public String email;
        public String password;
        public String created_at;
    }
}
