using API_CRUD_MURID_PAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD_MURID_PAA.Controllers
{
    [ApiController]
    [Route("api/murid")]
    [Authorize]
    public class MuridController : Controller
    {
        private MuridContext context;

        public MuridController(IConfiguration config)
        {
            context = new MuridContext(config.GetConnectionString("ConnStr"));
        }

        [HttpGet]
        public ActionResult GetAll() => Ok(context.ListMurid());

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Tambah([FromBody] Murid m)
        {
            context.TambahMurid(m);
            return Ok(new { message = "Data ditambahkan" });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Murid m)
        {
            m.idMurid = id;
            context.UpdateMurid(m);
            return Ok(new { message = "Data diupdate" });
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            context.DeleteMurid(id);
            return Ok(new { message = "Data dihapus" });
        }
    }
}