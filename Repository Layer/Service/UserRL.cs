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
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        //private IConfiguration Configuration { get; }
        private static string Key = "36c53aa7571c33d2f98d02a4313c4ba1ea15e45c18794eb564b21c19591805g";
        private readonly IConfiguration appsettings;

        public UserRL(IConfiguration Appsettings)
        {
            //this.Configuration = configuration;
            this.appsettings = Appsettings;

        }

        public UserReg Registration(UserReg userRegModel)
        {
            sqlConnection = new SqlConnection(this.appsettings["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddUsersDetails", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var encryptPassword = EncryptPassword(userRegModel.Password);
                    cmd.Parameters.AddWithValue("@FullName", userRegModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", userRegModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", encryptPassword);
                    cmd.Parameters.AddWithValue("@MobileNumber", userRegModel.MobileNumber);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return userRegModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    return null;
                else
                {
                    password += Key;
                    var passwordBytes = Encoding.UTF8.GetBytes(password);
                    return Convert.ToBase64String(passwordBytes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string DecryptPassword(string encodedPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(encodedPassword))
                    return null;
                else
                {
                    var encodedBytes = Convert.FromBase64String(encodedPassword);
                    var res = Encoding.UTF8.GetString(encodedBytes);
                    var resPass = res.Substring(0, res.Length - Key.Length);
                    return resPass;
                    //return res;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public LoginResponse UserLogin(UserLoginModel userLog)
        {
            
            using (SqlConnection sqlConnection = new SqlConnection(this.appsettings["ConnectionString:BookStoreDB"]))
            {
                try
                {
                    LoginResponse login = new LoginResponse();
                    SqlCommand cmd = new SqlCommand("SpUsersLogin", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", userLog.EmailId);

                    sqlConnection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var userId = Convert.ToInt64(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);
                            var password = Convert.ToString(rdr["Password"] == DBNull.Value ? default : rdr["Password"]);

                            login.FullName = Convert.ToString(rdr["FullName"] == DBNull.Value ? default : rdr["FullName"]);
                            login.EmailId = Convert.ToString(rdr["EmailId"] == DBNull.Value ? default : rdr["EmailId"]);
                            login.MobileNumber = Convert.ToInt64(rdr["MobileNumber"] == DBNull.Value ? default : rdr["MobileNumber"]);

                            var decryptPassword = DecryptPassword(password);
                            if (decryptPassword == userLog.Password)
                            {
                                login.Token = GenerateSecurityToken(userLog.EmailId, userId);
                                return login;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                        return null;
                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return default;

        }
        public string GenerateSecurityToken(string emailID, long userId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this.appsettings["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, emailID),
                new Claim("UserId", userId.ToString())
            };
            var token = new JwtSecurityToken(
                this.appsettings["Jwt:Issuer"],
                this.appsettings["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgotPassword(ForgotPasswordModel forgotPass)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.appsettings["ConnectionString:BookStoreDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpForgotPassword", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", forgotPass.EmailId);

                    sqlConnection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var userId = Convert.ToInt64(rdr["UserId"] == DBNull.Value ? default : rdr["UserId"]);

                            string token = GenerateSecurityToken(forgotPass.EmailId, userId);
                            new Msmq().SendMessage(token);
                            return "Reset link send successfully";
                        }
                    }
                    else
                        return null;
                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return default;

        }
        public string ResetPassword(ResetPassword resetPassword, string emailId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.appsettings["ConnectionString:BookStoreDB"]))
                {
                    if (resetPassword.Password == resetPassword.ConfirmPassword)
                    {
                        var encryptPassword = EncryptPassword(resetPassword.Password);

                        SqlCommand cmd = new SqlCommand("SpResetPasswords", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmailId", emailId);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassword(resetPassword.Password));
                        con.Open();
                        var result = cmd.ExecuteNonQuery();
                        con.Close();
                        if (result > 0)
                        {
                            return "Password Reset Successfully";
                        }
                        else
                            return "Unsuccessfully reset";
                    }
                    else
                        return null ;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

