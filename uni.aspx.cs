using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CampusAtlas
{
    public partial class uni : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string location = Request.QueryString["location"];
            string servername = "localhost";  // Sunucu adı
            string username = "root";  // Veritabanı kullanıcı adı
            string password = "root";  // Veritabanı şifresi
            string dbname = "universities";  // Veritabanı adı

            using (var conn = new SqlConnection("Data Source=" + servername + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password))
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    using (var cmd = new SqlCommand("SELECT * FROM uni WHERE location LIKE @Location", conn))
                    {
                        cmd.Parameters.AddWithValue("@Location", "%" + location + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                litUniversityData.Text = "<table class='table table-striped'><thead class='thead-dark' style='height: 5rem;'><tr><th class='col-8 text-center font-weight-bold'>Üniversite Adı</th><th class='col-4 font-weight-bold'>Bölüm</th></tr></thead><tbody>";

                                while (reader.Read())
                                {
                                    string name = reader["name"].ToString();
                                    string pageNo = reader["pageNo"].ToString();

                                    litUniversityData.Text += "<tr style='height: 5rem;'><td class='font-weight-bold col-8 align-middle' style='padding-left: 18%;'>" + name + "</td><td class='align-middle'><button class='btn btn-info col-4' onclick='goToDepartmentPage(\"" + pageNo + "\")'>Bölümler</button></td></tr>";
                                }

                                litUniversityData.Text += "</tbody></table>";
                            }
                            else
                            {
                                litUniversityData.Text = "There is no available data";
                            }
                        }
                    }
                }
                else
                {
                    litUniversityData.Text = "Veritabanına bağlanılamadı.";
                }
            }
        }
    }
}