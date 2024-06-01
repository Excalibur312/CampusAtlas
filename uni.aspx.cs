using System;
using System.Data.SqlClient;

namespace CampusAtlas
{
    public partial class uni : System.Web.UI.Page
    {
        string location = "";
        string connectionString = "Data Source=JETAIME;Initial Catalog=Atlas;Integrated Security=True;Connect Timeout=30;Encrypt=False;MultiSubnetFailover=False";

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "SELECT uni_id, uni_name, type, url " +
                    "FROM uni u " +
                    "JOIN city c ON u.city_id = c.id " +
                    "WHERE c.name LIKE @Location", connection);
                command.Parameters.AddWithValue("@Location", "%" + location + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    resultDiv.InnerHtml = "<table class='table table-striped'><thead class='thead-dark'><tr><th>Adı</th><th>Tür</th><th>URL</th></tr></thead><tbody>";

                    while (reader.Read())
                    {
                        int uniId = (int)reader["uni_id"];
                        string name = reader["uni_name"].ToString();
                        string type = reader["type"].ToString();
                        string url = reader["url"].ToString();

                        string universityLink = "<a href='faculty.aspx?uniId=" + uniId + "'>" + name + "</a>";

                        resultDiv.InnerHtml += "<tr><td class='text-center'>" + universityLink + "</td><td class='text-center'>" + type + "</td><td class='text-center'><a href='" + url + "' target='_blank'>" + url + "</a></td></tr>";
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
