using System;
using System.Data.SqlClient;

namespace CampusAtlas
{
    public partial class uni : System.Web.UI.Page
    {
        string location = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["location"] != null)
                {
                    location = Request.QueryString["location"];
                    ListUniByLocation();
                }
            }
        }

        private void ListUniByLocation()
        {
            string connectionString = "Data Source=JETAIME;Initial Catalog=CampusAtlas;Integrated Security=True;Connect Timeout=30;Encrypt=False;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT id, name, location, type, url FROM dbo.uni WHERE location LIKE @Location", connection);
                command.Parameters.AddWithValue("@Location", "%" + location + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDiv.InnerHtml = "<table class='table table-striped'><thead class='thead-dark' style='height: 5rem;'><tr><th class='col-2 text-center font-weight-bold'>ID</th><th class='col-3 text-center font-weight-bold'>Name</th><th class='col-3 text-center font-weight-bold'>Location</th><th class='col-2 text-center font-weight-bold'>Type</th><th class='col-2 text-center font-weight-bold'>URL</th></tr></thead><tbody>";

                    while (reader.Read())
                    {
                        string id = reader["id"].ToString();
                        string name = reader["name"].ToString();
                        string loc = reader["location"].ToString();
                        string type = reader["type"].ToString();
                        string url = reader["url"].ToString();

                        resultDiv.InnerHtml += "<tr><td class='text-center'>" + id + "</td><td class='text-center'>" + name + "</td><td class='text-center'>" + loc + "</td><td class='text-center'>" + type + "</td><td class='text-center'>" + url + "</td></tr>";
                    }

                    resultDiv.InnerHtml += "</tbody></table>";
                }
                else
                {
                    resultDiv.InnerHtml = "There is no available data";
                }
            }
        }
    }
}
