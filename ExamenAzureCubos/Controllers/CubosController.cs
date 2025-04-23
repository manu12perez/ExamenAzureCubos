using ExamenAzureCubos.Helpers;
using ExamenAzureCubos.Models;
using ExamenAzureCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenAzureCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;
        private HelperUsuarioToken helper;

        /*
         * Si incluyo el helper me da error
         */
        //public CubosController(RepositoryCubos repo, HelperUsuarioToken helper)
        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
            //this.helper = helper;
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

        [Authorize]
        [HttpGet("Perfil/{email}")]
        public async Task<ActionResult<Usuario>> GetPerfil(string email)
        {
            var usuario = await this.repo.GetPerfilAsync(email);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound(new { mensaje = "Usuario no encontrado" });
        }

        /*
         * NO ME FUNCIONA LO DEJO CON EL EMAIL
         */
        //[Authorize]
        //[HttpGet("Perfil")]
        //public async Task<ActionResult<UsuarioModel>> Perfil()
        //{
        //    UsuarioModel model = this.helper.GetUsuario();
        //    return model;
        //}

        [HttpGet("Compras/{idUsuario}")]
        [Authorize]
        public async Task<ActionResult<List<Compra>>> GetComprasByUsuario(int idUsuario)
        {
            var pedidos = await this.repo.GetComprasByUsuarioAsync(idUsuario);
            return Ok(pedidos);
        }

        [Authorize]
        [HttpPost("Compra")]
        public async Task<ActionResult> InsertCompra([FromBody] Compra compra)
        {
            await this.repo.InsertPedidoAsync(compra.IdCubo, compra.IdUsuario);
            return Ok(new { mensaje = "Compra realizada correctamente" });
        }
    }
}
