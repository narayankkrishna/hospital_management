using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Pages.Clients.Login;

using System.Data.SqlClient;

namespace MyStore.Pages
{
	public class LoginModel : PageModel
	{
		public LoginInfo loginInfo = new LoginInfo();
		public string errorMessage = "";
		public string successMessage = "";
		public string loginemail = "pbadmin@bpbloodbank.com";
		public string loginpassword = "BpBloodbank@1";
		public string loginname = "Admin-B.P";

		public void OnGet()
		{
		}

		public void OnPost()
		{
			loginInfo.name = Request.Form["name"];
			loginInfo.email = Request.Form["email"];
			loginInfo.password = Request.Form["password"];

			if (loginInfo.name.Length == 0 || loginInfo.email.Length == 0 || loginInfo.password.Length == 0 || loginInfo.name != "Narayan" || loginInfo.email != "narayan@gmail.com" || loginInfo.password != "123456")
			{
				errorMessage = "Only ADMIN can be Logged In ! ";
				return;
			}

			//save new donor into database


			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sql = "INSERT INTO users" +
						"(name, email, password) VALUES " +
						"(@name, @email, @password);";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", loginInfo.name);
						command.Parameters.AddWithValue("@email", loginInfo.email);
						command.Parameters.AddWithValue("@password", loginInfo.password);
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}
			loginInfo.name = "";
			loginInfo.email = "";
			loginInfo.password = "";
			successMessage = "Login Succesfull";

			Response.Redirect("/Clients/Index");

		}
	}
}