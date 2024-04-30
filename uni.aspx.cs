<<<<<<< HEAD
﻿using System;
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
=======
﻿using System;
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
>>>>>>> 16655f4c31da23fa686c127c1fcea6ba863946c5
