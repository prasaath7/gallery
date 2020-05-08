using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class ImageBusinessLayer
    {
        public IEnumerable<Image> Images
        {
            get
            {
                string connectionString =
                    ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

                List<Image> images = new List<Image>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetImages", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Image image = new Image();
                        image.UserId = Convert.ToInt32(rdr["UserId"]);
                        image.username = rdr["username"].ToString();
                        image.imagename = rdr["imagename"].ToString();
                        image.imagepath = rdr["imagepath"].ToString();

                        images.Add(image);
                    }
                }

                return images;
            }
        }

        public void AddImage(Image image)
        {
            string connectionString =
                    ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddImage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@username";
                paramName.Value = image.username;
                cmd.Parameters.Add(paramName);

                SqlParameter paramImg = new SqlParameter();
                paramImg.ParameterName = "@imagename";
                paramImg.Value = image.imagename;
                cmd.Parameters.Add(paramImg);

                SqlParameter paramPath= new SqlParameter();
                paramPath.ParameterName = "@imagepath";
                paramPath.Value = image.imagepath;
                cmd.Parameters.Add(paramPath);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveImage(Image image)
        {
            string connectionString =
                    ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SaveImage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@UserId";
                paramId.Value = image.UserId;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@username";
                paramName.Value = image.username;
                cmd.Parameters.Add(paramName);

                SqlParameter paramImg = new SqlParameter();
                paramImg.ParameterName = "@imagename";
                paramImg.Value = image.imagename;
                cmd.Parameters.Add(paramImg);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteImage(int id)
        {
            string connectionString =
                    ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteImage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@UserId";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
