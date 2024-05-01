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
                SqlCommand command = new SqlCommand("SELECT name, type, url FROM uni WHERE location LIKE @Location", connection);
                command.Parameters.AddWithValue("@Location", "%" + location + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDiv.InnerHtml = "<table class='table table-striped'><thead class='thead-dark' style='height: 5rem;'><tr><th class='col-3 text-center font-weight-bold'>Name</th><th class='col-3 text-center font-weight-bold'>Type</th></tr></thead><tbody>";

                    while (reader.Read())
                    {
                        string name = reader["name"].ToString();
                        string type = reader["type"].ToString();

                        // Üniversite ismi için bir bağlantı oluştur
                        string universityLink = "<a href='" + reader["url"].ToString() + "' target='_blank'>" + name + "</a>";

                        resultDiv.InnerHtml += "<tr><td class='text-center'>" + universityLink + "</td><td class='text-center'>" + type + "</td></tr>";
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
