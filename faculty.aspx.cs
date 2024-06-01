using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace CampusAtlas
{
    public partial class faculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["uniId"] != null)
                {
                    int uniId = Convert.ToInt32(Request.QueryString["uniId"]);
                    ListDepartments(uniId);
                }
            }
        }

        private void ListDepartments(int uniId)
        {
            string connectionString = "Data Source=JETAIME;Initial Catalog=Atlas;Integrated Security=True;Connect Timeout=30;Encrypt=False;MultiSubnetFailover=False";
            StringBuilder result = new StringBuilder();
            result.Append("<div class='table-responsive'><table class='table table-striped table-bordered'><thead class='thead-dark'><tr><th>Fakülte</th><th>Bölüm</th><th>Süre</th><th>Tür</th><th>Kontenjan</th><th>Sıralama</th><th>Taban Puan</th></tr></thead><tbody>");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(
                        "SELECT faculty, department, uni_time, dep_type, kont, siralama, base " +
                        "FROM department " +
                        "WHERE uni_id = @UniId " +
                        "ORDER BY faculty", connection);
                    command.Parameters.AddWithValue("@UniId", uniId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        string currentFaculty = "";
                        while (reader.Read())
                        {
                            string faculty = reader["faculty"].ToString();
                            if (currentFaculty != faculty)
                            {
                                result.Append("<tr class='table-info'><td colspan='7' class='text-center font-weight-bold'>" + faculty + "</td></tr>");
                                currentFaculty = faculty;
                            }

                            string department = reader["department"].ToString();
                            string uniTime = reader["uni_time"].ToString();
                            string depType = reader["dep_type"].ToString();
                            string kont = reader["kont"].ToString();
                            string siralama = reader["siralama"].ToString();
                            string baseScore = reader["base"].ToString();

                            result.Append($"<tr><td>{faculty}</td><td>{department}</td><td>{uniTime}</td><td>{depType}</td><td>{kont}</td><td>{siralama}</td><td>{baseScore}</td></tr>");
                        }

                        result.Append("</tbody></table></div>");
                    }
                    else
                    {
                        result.Append("There are no departments available for this university");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Append("An error occurred: " + ex.Message);
            }

            litDepartments.Text = result.ToString();
        }
    }
}
