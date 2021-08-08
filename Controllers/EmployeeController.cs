using DepartamentEmployee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace EmployeeEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select 
                                EmployeeId,
                                EmployeeName,
                                Departament,
                                DATE_FORMAT(DateOfJoining, '%Y-%m-%d') as DateOfJoining,
                                PhotoFileName
                            from `MgZGKoFugz`.`dbo.Employee`";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeDB");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"insert 
                            into `MgZGKoFugz`.`dbo.Employee`
                            (EmployeeName, Departament, DateOfJoining, PhotoFileName)
                            values (@EmployeeName, @Departament, @DateOfJoining, @PhotoFileName);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeDB");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Departament", emp.Departament);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"update 
                            `MgZGKoFugz`.`dbo.Employee`
                            set EmployeeName = @EmployeeName,
                                Departament = @Departament,
                                DateOfJoining = @DateOfJoining,
                                PhotoFileName = @PhotoFileName
                            where EmployeeId = @EmployeeId;
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeDB");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Departament", emp.Departament);
                    string date = emp.DateOfJoining.ToString("yyyy-MM-dd");
                    myCommand.Parameters.AddWithValue("@DateOfJoining", date);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    Console.WriteLine(myCommand.CommandText.ToString());
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Succesfully");
        }
        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"Delete from
                            `MgZGKoFugz`.`dbo.Employee`
                            where EmployeeId = @EmployeeId;
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeDB");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Succesfully");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                using (var stream = new FileStream(physicalPath,FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.jpg");
            }
        }
    }


}
