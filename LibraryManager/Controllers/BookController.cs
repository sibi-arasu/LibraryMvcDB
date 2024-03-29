﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LibraryDataSource;
using LibraryDataSource.DAL;

namespace LibraryManager.Controllers
{
    public class BookController : Controller
    {
        BookInterData _libraryIData = new BookInterData();
        //String connectionString = @"Data Source = aes377\sqlexpress;Initial Catalog=LibraryManagement;Integrated security = True";
        //DataTable dataTable1 = new DataTable();
        [HttpGet]
        public ActionResult Index1(int id)
        {

            /* using (SqlConnection con = new SqlConnection(connectionString))
             {
                 con.Open();
                 string query = "SELECT * FROM Book WHERE GenreName = @Id";
                 SqlCommand cmd = new SqlCommand(query, con);
                 cmd.Parameters.AddWithValue("@Id", id);

                 SqlDataAdapter sqlData1 = new SqlDataAdapter(cmd);
                 sqlData1.Fill(dataTable1);
             }*/
            
            DataTable dtTable = _libraryIData.GetBookData(id);
            return View(dtTable);
        }
        public ActionResult AllBook()
        {

            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Exec BookSp";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sqlData1 = new SqlDataAdapter(cmd);
                sqlData1.Fill(dataTable);
            }*/

            DataTable dataTable = _libraryIData.GetAllBook();
            return View(dataTable);
        }

        // GET: Book/Create
        public ActionResult Create1()
        {
            return View(new BookDb());
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create1(BookDb bookDb)
        {
            /* using (SqlConnection con = new SqlConnection(connectionString))
             {
                 con.Open();
                 string query = "INSERT INTO Book (Book_Id, Name, GenreName) VALUES (@Id, @Name , @GenreName)";
                 SqlCommand cmd = new SqlCommand(query, con);
                 cmd.Parameters.AddWithValue("@Id", bookDb.Id);
                 cmd.Parameters.AddWithValue("@Name", bookDb.Name);
                 cmd.Parameters.AddWithValue("@GenreName", bookDb.GenreName);
                 cmd.ExecuteNonQuery();
             }*/
            int num = _libraryIData.CreateBookData(bookDb);
            if (num == 1)
                return RedirectToAction("AllBook");
            else 
                return RedirectToAction("Create1");
        }

        // GET: Book/Edit/5
        public ActionResult Edit1(int id)
        {
            BookDb bookDb = new BookDb();
            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "Select * From Book Where Book_Id = @ID";
                con.Open();
                SqlDataAdapter sqlData1 = new SqlDataAdapter(query,con);
                sqlData1.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlData1.Fill(dataTable1);
            }*/
            DataTable dataTable = _libraryIData.EditBookData(id);
            if(dataTable.Rows.Count == 1)
            {
                bookDb.Id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                bookDb.Name = dataTable.Rows[0][1].ToString();
                bookDb.GenreName = Convert.ToInt32(dataTable.Rows[0][2].ToString());
                return View(bookDb);
            }
            return RedirectToAction("Index1");
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit1(int id, BookDb bookDb)
        {
            /*string query = "Update Book set Name = @Name , GenreName = @GenreName Where Book_Id = @Id";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", bookDb.Id);
                cmd.Parameters.AddWithValue("@Name", bookDb.Name);
                cmd.Parameters.AddWithValue("@GenreName", bookDb.GenreName);
                cmd.ExecuteNonQuery();

            }*/
            int num = _libraryIData.PostEditBook(id, bookDb);
            if (num == 1)
                return RedirectToAction("AllBook");
            else
                return RedirectToAction("Edit1", id);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Delete From Book Where Book_Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }*/
            _libraryIData.DeleteBook(id);
            return RedirectToAction("AllBook");
        }
        public ActionResult DeleteAll(int id)
        {
            /*using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Delete From Book Where GenreName = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }*/
            _libraryIData.DeleteAllBook(id);
            return RedirectToAction("Delete","Library",new { @Id = id});
        }

        // POST: Book/Delete/5
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
