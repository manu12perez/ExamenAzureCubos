using ExamenAzureCubos.Helpers;
using ExamenAzureCubos.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ExamenAzureCubos.Repositories;

namespace ExamenAzureCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryCubos repo;
        private HelperActionServicesOAuth helper;

        public AuthController(RepositoryCubos repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Usuario usuario =
                await this.repo.LogInUsuairosAsync(model.Email, model.Pass);

            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);

                UsuarioModel modelUsu = new UsuarioModel();
                modelUsu.IdUsuario = usuario.IdUsuario;
                modelUsu.Email = usuario.Email;
                modelUsu.Pass = usuario.Pass;
                modelUsu.Imagen = usuario.Imagen;

                string jsonUsuario = JsonConvert.SerializeObject(modelUsu);
                string jsonCifrado = HelperCryptography.EncryptString(jsonUsuario);

                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonCifrado)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: informacion,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    notBefore: DateTime.UtcNow
                    );
                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }
    }
}
