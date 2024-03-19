using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace LibraryDataSource.DAL
{
    public class BookInterData
    {
        private String connectionString = @"Data Source = aes377\sqlexpress;Initial Catalog=LibraryManagement;Integrated security = True";

        public DataTable GetBookData(int id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //string query = "SELECT * FROM Book WHERE GenreName = @Id";

                SqlCommand cmd = new SqlCommand("BookSp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Get1");
                cmd.Parameters.AddWithValue("@GenreName", id);

                SqlDataAdapter sqlData1 = new SqlDataAdapter(cmd);
                sqlData1.Fill(dataTable);
            }
            return dataTable;
        }
        public DataTable GetAllBook()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //string query = "Exec BookSp";

                SqlCommand cmd = new SqlCommand("BookSp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Get");
                SqlDataAdapter sqlData1 = new SqlDataAdapter(cmd);
                sqlData1.Fill(dataTable);
            }
            return dataTable;
        }
        public int CreateBookData(BookDb bookDb)
        {
            int num = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
               // string query = "INSERT INTO Book (Book_Id, Name, GenreName) VALUES (@Book_Id, @Name , @GenreName)";
                SqlCommand cmd = new SqlCommand("bookInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_Id", bookDb.Id);
                cmd.Parameters.AddWithValue("@Name", bookDb.Name);
                cmd.Parameters.AddWithValue("@GenreName", bookDb.GenreName);
                cmd.ExecuteNonQuery();
                num = 1;
            }
            return num;
        }
        public DataTable EditBookData(int id)
        {
            //CategoryDb categoryDb = new CategoryDb();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //string query = "Select * From Book Where Book_Id = @ID";
                con.Open();
                SqlCommand cmd = new SqlCommand("BookSp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "B4Put");
                cmd.Parameters.AddWithValue("@Book_Id", id);
                SqlDataAdapter sqlData1 = new SqlDataAdapter(cmd);
                //sqlData1.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlData1.Fill(dataTable);
            }
            return dataTable;
        }
        public int PostEditBook(int id, BookDb bookDb)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //string query = "Update Book set Name = @Name , GenreName = @GenreName Where Book_Id = @Id";
                SqlCommand cmd = new SqlCommand("BookEdit", con);
                cmd.CommandType = CommandType.StoredProcedure;
  
                cmd.Parameters.AddWithValue("@Book_Id", bookDb.Id);
                cmd.Parameters.AddWithValue("@Name", bookDb.Name);
                cmd.Parameters.AddWithValue("@GenreName", bookDb.GenreName);
                
                cmd.ExecuteNonQuery();

            }
            return 1;
        }
        public void DeleteBook(int id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //string query = "Delete From Book Where Book_Id = @Id";

                SqlCommand cmd = new SqlCommand("BookDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@Book_Id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteAllBook(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
               // string query = "Delete From Book Where GenreName = @Id";

                SqlCommand cmd = new SqlCommand("BookDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteAll");
                cmd.Parameters.AddWithValue("@Book_Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
 }

