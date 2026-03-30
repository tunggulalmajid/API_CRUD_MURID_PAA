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
            string query = string.Format(@"select * from murids order by id_murid");
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListMurid.Add(new Murid()
                    {
                        idMurid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        tanggalLahir = DateOnly.Parse(reader["tanggal_lahir"].ToString()),
                        email = reader["email"].ToString(),
                        alamat = reader["alamat"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex) {
                throw new Exception($"error : {ex.Message}");
            }

            return ListMurid;
        }

        public void TambahMurid(Murid murid)
        {
            string query = string.Format(@"insert into murids(nama, tanggal_lahir, email, alamat) 
                    values (@nama, @tanggal_lahir, @email, @alamat)");

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", murid.nama);
                cmd.Parameters.AddWithValue("@tanggal_lahir", murid.tanggalLahir);
                cmd.Parameters.AddWithValue("@email", murid.email);
                cmd.Parameters.AddWithValue("@alamat", murid.alamat);

                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex) 
            {
                throw new Exception($"gagal: {ex.Message}");
            }
        }

        public void UpdateMurid(Murid murid) 
        {
            string query = string.Format(@"UPDATE murids 
                     SET nama = @nama, tanggal_lahir = @tanggal_lahir, email = @email, alamat = @alamat 
                     WHERE id_murid = @id");

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", murid.idMurid);
                cmd.Parameters.AddWithValue("@nama", murid.nama);
                cmd.Parameters.AddWithValue("@tanggal_lahir", murid.tanggalLahir);
                cmd.Parameters.AddWithValue("@email", murid.email);
                cmd.Parameters.AddWithValue("@alamat", murid.alamat);

                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception($"gagal: {ex.Message}");
            }
        }

        public int DeleteMurid(int id)
        {
            string query = string.Format(@"DELETE From murids where id_murid = @id");

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                int kolomTerhapus = cmd.ExecuteNonQuery();
                db.closeConnection();

                return kolomTerhapus;
            }
            catch (Exception ex)
            {
                throw new Exception($"gagal: {ex.Message}");
            }
        }
    }
}
