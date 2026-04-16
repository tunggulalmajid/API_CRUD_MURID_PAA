using API_CRUD_MURID_PAA.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD_MURID_PAA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly string _connStr;
        private readonly IConfiguration _config;

       
        public LoginController(IConfiguration config)
        {
            _config = config;
            _connStr = config.GetConnectionString("ConnStr");
        }

        [HttpPost]
        public ActionResult LoginUser([FromBody] Auth auth)
        {
            try
            {
                LoginContext context = new LoginContext(_connStr);
                List<Login> data = context.Authentifikasi(auth.Username, auth.Password, _config);
                if (data != null && data.Count > 0)
                {
                    return Ok(new
                    {
                        status = "success",
                        message = "Login berhasil",
                        data = data[0] 
                    });
                }
                else
                {

                    return Unauthorized(new
                    {
                        status = "error",
                        message = "Username atau Password salah"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = $"Terjadi kesalahan pada server: {ex.Message}"
                });
            }
        }
    }
}