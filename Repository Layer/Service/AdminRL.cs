using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Service
{
    public class AdminRL : IAdminRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public AdminRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public AdminLogin Adminlogin(AdminResponse adminResponse)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
                SqlCommand cmd = new SqlCommand("LoginAdmin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmailId", adminResponse.EmailId);
                cmd.Parameters.AddWithValue("@Password", adminResponse.Password);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AdminLogin admin = new AdminLogin();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        admin.AdminId = Convert.ToInt32(reader["AdminId"] == DBNull.Value ? default : reader["AdminId"]);
                        admin.FullName = Convert.ToString(reader["FullName"] == DBNull.Value ? default : reader["FullName"]);
                        admin.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                        admin.MobileNumber = Convert.ToString(reader["MobileNumber"] == DBNull.Value ? default : reader["MobileNumber"]);
                        admin.Address = Convert.ToString(reader["Address"] == DBNull.Value ? default : reader["Address"]);
                    }

                    this.sqlConnection.Close();
                    admin.Token = this.GenerateJWTToken(admin);
                    return admin;
                }
                else
                {
                    throw new Exception("Email Or Password Is Wrong");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateJWTToken(AdminLogin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("EmailId", admin.EmailId),
                new Claim("AdminId", admin.AdminId.ToString())
            };

            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
