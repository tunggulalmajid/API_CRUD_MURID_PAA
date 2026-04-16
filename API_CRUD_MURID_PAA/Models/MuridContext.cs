using API_CRUD_MURID_PAA.Helper;
using Npgsql;

namespace API_CRUD_MURID_PAA.Models
{
    public class MuridContext
    {
        private SqlDBHelper db;

        public MuridContext(string connStr)
        {
            db = new SqlDBHelper(connStr);
        }

        public List<Murid> ListMurid()
        {
            List<Murid> ListMurid = new List<Murid>();
          
            string query = @"SELECT id_murid, nama, tanggal_lahir, kelas, email, alamat, 
                             username, password, id_role_murid FROM murids ORDER BY id_murid";
            try
            {
                using NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListMurid.Add(new Murid()
                    {
                        idMurid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        tanggalLahir = DateOnly.Parse(reader["tanggal_lahir"].ToString()),
                        kelas = reader["kelas"].ToString(),
                        email = reader["email"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        id_role_murid = int.Parse(reader["id_role_murid"].ToString())
                    });
                }
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            return ListMurid;
        }

        public void TambahMurid(Murid murid)
        {
            string query = @"INSERT INTO murids(nama, tanggal_lahir, kelas, email, alamat, username, password, id_role_murid) 
                             VALUES (@nama, @tanggal_lahir, @kelas, @email, @alamat, @username, @password, @role)";
            try
            {
                using NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", murid.nama);
                cmd.Parameters.AddWithValue("@tanggal_lahir", murid.tanggalLahir);
                cmd.Parameters.AddWithValue("@kelas", murid.kelas);
                cmd.Parameters.AddWithValue("@email", murid.email);
                cmd.Parameters.AddWithValue("@alamat", murid.alamat);
                cmd.Parameters.AddWithValue("@username", murid.username);
                cmd.Parameters.AddWithValue("@password", murid.password);
                cmd.Parameters.AddWithValue("@role", murid.id_role_murid);

                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal: {ex.Message}");
            }
        }

        public void UpdateMurid(Murid murid)
        {
            string query = @"UPDATE murids 
                             SET nama = @nama, tanggal_lahir = @tanggal_lahir, kelas = @kelas, 
                                 email = @email, alamat = @alamat, username = @username, 
                                 password = @password, id_role_murid = @role 
                             WHERE id_murid = @id";
            try
            {
                using NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", murid.idMurid);
                cmd.Parameters.AddWithValue("@nama", murid.nama);
                cmd.Parameters.AddWithValue("@tanggal_lahir", murid.tanggalLahir);
                cmd.Parameters.AddWithValue("@kelas", murid.kelas);
                cmd.Parameters.AddWithValue("@email", murid.email);
                cmd.Parameters.AddWithValue("@alamat", murid.alamat);
                cmd.Parameters.AddWithValue("@username", murid.username);
                cmd.Parameters.AddWithValue("@password", murid.password);
                cmd.Parameters.AddWithValue("@role", murid.id_role_murid);

                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal: {ex.Message}");
            }
        }

        public int DeleteMurid(int id)
        {
            string query = "DELETE FROM murids WHERE id_murid = @id";
            try
            {
                using NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                int barisTerhapus = cmd.ExecuteNonQuery();
                db.closeConnection();
                return barisTerhapus;
            }
            catch (Exception ex)
            {
                throw new Exception($"Gagal: {ex.Message}");
            }
        }
    }
}