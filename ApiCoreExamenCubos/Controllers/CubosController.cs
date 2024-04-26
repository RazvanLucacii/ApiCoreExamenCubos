using ApiCoreExamenCubos.Models;
using ApiCoreExamenCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCoreExamenCubos.Controllers
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

        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> CubosMarca(string marca)
        {
            return await this.repo.GetCubosMarcaAsync(marca);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<UsuarioCubo>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.FindFirst(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            UsuarioCubo usuario = JsonConvert.DeserializeObject<UsuarioCubo>(jsonUsuario);
            return usuario;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<CompraCubos>>> GetPedidosUsuario(int id)
        {
            return await this.repo.GetPedidosUsuarioAsync(id);
        }

    }
}
