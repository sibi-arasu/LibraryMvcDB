using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LibraryDataSource.DAL
{
    public partial class LibraryIData
    {
        private String connectionString = @"Data Source = aes377\sqlexpress;Initial Catalog=LibraryManagement;Integrated security = True";

        public DataTable GetCategoryData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CategorySp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Get");
                //cmd.Parameters.AddWithValue("@Id", DBNull.Value);
                //cmd.Parameters.AddWithValue("@Genre", DBNull.Value);
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                sqlData.Fill(dataTable);
            }
            return dataTable;
        }
        public int CreateCategoryData(CategoryDb categoryDb)
        {
            int num = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
               // string query = "INSERT INTO Category (Id, Genre) VALUES (@Id, @Name)";
                SqlCommand cmd = new SqlCommand("CategorySp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Post");
                cmd.Parameters.AddWithValue("@Id", categoryDb.Id);
                cmd.Parameters.AddWithValue("@Genre", categoryDb.Name);
                cmd.ExecuteNonQuery();
                num = 1;
            }
            return num;
        }
        public DataTable EditCategoryData(int id)
        {
            //CategoryDb categoryDb = new CategoryDb();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string query = "Select * From Category Where Id = @ID";
                SqlCommand cmd = new SqlCommand("CategorySp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Get1");
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dataTable);

            }
            return dataTable;
        }
        public int PostEditCategory(int id, CategoryDb categoryDb)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //string query = "Update Category Set Genre = @Name Where Id = @Id";
                SqlCommand cmd = new SqlCommand("CategorySp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Put");
                cmd.Parameters.AddWithValue("@Id", categoryDb.Id);
                cmd.Parameters.AddWithValue("@Genre", categoryDb.Name);
                cmd.ExecuteNonQuery();
            }
            return 1;
        }
        public void DeleteCategory(int id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
               // string query = "Delete From Category Where Id = @Id";
                SqlCommand cmd = new SqlCommand("CategorySp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
