namespace API_CRUD_MURID_PAA.Models
{
    public class Login
    {
        public int idMurid { get; set; }
        public string nama { get; set; }
        public DateOnly tanggalLahir { get; set; }
        public string kelas { get; set; }
        public string email { get; set; }
        public string alamat { get; set; }
        public int IdRoleMurid {  get; set; }
        public string NamaRole {  get; set; }
        public string Token { get; set; } 
    }
}
