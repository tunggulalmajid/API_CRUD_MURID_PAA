using API_CRUD_MURID_PAA.Helper;
using Npgsql;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Text;

namespace API_CRUD_MURID_PAA.Models
{
    public class LoginContext
    {
        private readonly string __constr;

        public LoginContext(string connStr)
        {
            __constr = connStr;
        }

        public List<Login> Authentifikasi(string pUsername, string pPassword, IConfiguration pConfig)
        {
            List<Login> list1 = new List<Login>();
            
            string query = @"SELECT m.id_murid, m.nama, m.tanggal_lahir, m.kelas, m.email, m.alamat, 
                             m.id_role_murid, r.nama_role 
                             FROM murids m
                             JOIN role_murids r ON m.id_role_murid = r.id_role_murid
                             WHERE m.username = @username AND m.password = @pass";
            try
            {
                SqlDBHelper db = new SqlDBHelper(this.__constr);
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@username", pUsername);
                cmd.Parameters.AddWithValue("@pass", pPassword);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list1.Add(new Login()
                    {
                        idMurid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        tanggalLahir = DateOnly.Parse(reader["tanggal_lahir"].ToString()),
                        kelas = reader["kelas"].ToString(),
                        email = reader["email"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        IdRoleMurid = int.Parse(reader["id_role_murid"].ToString()),
                        NamaRole = reader["nama_role"].ToString(),
                        Token = GenerateJwtToken(pUsername, reader["nama_role"].ToString(), pConfig)
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
            return list1;
        }

        private string GenerateJwtToken(string namaUser, string peran, IConfiguration pConfig)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(pConfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, namaUser),
                new Claim(ClaimTypes.Role, peran)
            };

            var token = new JwtSecurityToken(
                pConfig["Jwt:Issuer"],
                pConfig["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}