using ExamenAzureCubos.Models;
using ExamenAzureCubos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenAzureCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet("Cubos")]
        public async Task<ActionResult<List<Cubo>>> GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet("FindCubo/{marca}")]
        public async Task<ActionResult<Cubo>> FindCubo(string marca)
        {
            return await this.repo.FindCuboAsync(marca);
        }

        [HttpPost("InsertUsuario")]
        public async Task<ActionResult> InsertUsuario
            (string nombre, string email, string pass, string imagen)
        {
            await this.repo.InsertUsuarioAsync(nombre, email, pass, imagen);
            return Ok();
        }
    }
}
