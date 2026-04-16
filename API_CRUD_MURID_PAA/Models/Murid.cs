using System.Text.Json.Serialization;

namespace API_CRUD_MURID_PAA.Models
{
    public class Murid
    {
        public int idMurid { get; set; }
        public string nama { get; set; }
        public DateOnly tanggalLahir { get; set; }
        public string kelas {  get; set; }
        public string email { get; set; }
        public string alamat { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int id_role_murid { get; set; }

    }
}
