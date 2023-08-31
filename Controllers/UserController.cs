using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRUD_Example.Models;

namespace CRUD_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM Users ORDER BY UserId DESC";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("YourConnectionString")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                }
            }
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            string query = @"INSERT INTO Users (UserName, Email, Password, RoleId) VALUES (@UserName, @Email, @Password, @RoleId)";
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("YourConnectionString")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@UserName", user.UserName);
                    sc.Parameters.AddWithValue("@Email", user.Email);
                    sc.Parameters.AddWithValue("@Password", user.Password);
                    sc.Parameters.AddWithValue("@RoleId", user.RoleId);
                    sc.ExecuteNonQuery();
                }
            }
            return new JsonResult("User added successfully");
        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"UPDATE Users SET UserName = @UserName, Email = @Email, Password = @Password, RoleId = @RoleId WHERE UserId = @UserId";
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("YourConnectionString")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@UserId", user.UserId);
                    sc.Parameters.AddWithValue("@UserName", user.UserName);
                    sc.Parameters.AddWithValue("@Email", user.Email);
                    sc.Parameters.AddWithValue("@Password", user.Password);
                    sc.Parameters.AddWithValue("@RoleId", user.RoleId);
                    sc.ExecuteNonQuery();
                }
            }
            return new JsonResult("User updated successfully");
        }

        [HttpDelete("{userId}")]
        public JsonResult Delete(int userId)
        {
            string query = @"DELETE FROM Users WHERE UserId = @UserId";
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("YourConnectionString")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@UserId", userId);
                    sc.ExecuteNonQuery();
                }
            }
            return new JsonResult("User deleted successfully");
        }
    }
}
