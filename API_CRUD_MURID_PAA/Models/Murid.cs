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
        
    }
}
