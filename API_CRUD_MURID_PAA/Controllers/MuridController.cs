using API_CRUD_MURID_PAA.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD_MURID_PAA.Controllers
{
    public class MuridController : Controller
    {
        private string connStr;
        private MuridContext context;
        public MuridController(IConfiguration config)
        {
            this.connStr = config.GetConnectionString("ConnStr");
            context = new MuridContext(connStr);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/api/murid")]
        public ActionResult<Murid> GetAllMurid() 
        {
            try
            {
                List<Murid> data = context.ListMurid();
                return Ok(data);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, $"Error : {ex.Message}");
            }
        }

        [HttpPost("/api/murid")]
        public ActionResult<Murid> tambahMurid([FromBody] Murid murid)
        {
            try
            {
                context.TambahMurid(murid);
                return Ok(new {message = $"Data Murid {murid.nama} berhasil ditambahkan"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Gagal : {ex.Message}");
            }
        }

        [HttpPut("/api/murid/{id}")]
        public ActionResult<Murid> UpdateMurid(int id, [FromBody] Murid murid)
        {
            try
            {
                murid.idMurid = id;
                context.UpdateMurid(murid);
                return Ok(new { message = $"Data Murid {murid.nama} berhasil diupdate" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Gagal : {ex.Message}");
            }
        }

        [HttpDelete("/api/murid/{id}")]
        public ActionResult DeleteMurid(int id)
        {
            try
            {
                int barisTerhapus = context.DeleteMurid(id);
                if(barisTerhapus > 0)
                {

                    return Ok(new { message = $"Data dengan id : {id} berhasil dihapus" });
                }
                else
                {
                    return NotFound(new { message = $"Data dengan id : {id} tidak ditemukan" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Gagal : {ex.Message}");
            }
        }




    }
}
