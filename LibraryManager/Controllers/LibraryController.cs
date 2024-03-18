using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LibraryDataSource;

namespace LibraryManager.Controllers
{
    public class LibraryController : Controller
    {
        // GET: Library
        String connectionString = @"Data Source = aes377\sqlexpress;Initial Catalog=LibraryManagement;Integrated security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Category", con);
                sqlData.Fill(dataTable);

            }
            return View(dataTable);
        }

        // GET: Library/Create
        public ActionResult Create()
        {
            return View(new CategoryDb());
        }

        // POST: Library/Create
        [HttpPost]
        public ActionResult Create(CategoryDb categoryDb)
        {
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                con.Open();
                string query = "INSERT INTO Category (Id, Genre) VALUES (@Id, @Name)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", categoryDb.Id);
                cmd.Parameters.AddWithValue("@Name", categoryDb.Name);
                cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            
        }

        // GET: Library/Edit/5
        public ActionResult Edit(int id)
        {
            CategoryDb categoryDb = new CategoryDb();
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select * From Category Where Id = @ID";
                SqlDataAdapter dataAdapter = new  SqlDataAdapter(query, con);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@ID", id);
                dataAdapter.Fill(dataTable);

            }
            if (dataTable.Rows.Count == 1)
            {
                categoryDb.Id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                categoryDb.Name = dataTable.Rows[0][1].ToString();
                return View(categoryDb);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Library/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryDb categoryDb)
        {

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Update Category Set Genre = @Name Where Id = @Id" ;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", categoryDb.Id);
                cmd.Parameters.AddWithValue("@Name", categoryDb.Name);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
            
        }

        // GET: Library/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Delete From Category Where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Library/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
