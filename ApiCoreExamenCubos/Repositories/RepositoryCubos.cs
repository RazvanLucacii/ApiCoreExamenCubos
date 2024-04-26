using ApiCoreExamenCubos.Data;
using ApiCoreExamenCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCoreExamenCubos.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<UsuarioCubo> LogInUsuarioAsync(string email, string password)
        {
            return await this.context.Usuarioscubos.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<List<Cubo>> GetCubosMarcaAsync(string marca)
        {
            return await this.context.Cubos.Where(z => z.Marca == marca).ToListAsync();
        }

        public async Task InsertUsuarioAsync(int idusuario, string nombre, string email, string pass, string imagen)
        {
            UsuarioCubo usu = new UsuarioCubo();
            usu.IdUsuario = idusuario;
            usu.Nombre = nombre;
            usu.Email = email;
            usu.Password = pass;
            usu.Imagen = imagen;
            await this.context.SaveChangesAsync();
        }

        public async Task<List<CompraCubos>> GetPedidosUsuarioAsync(int idusuario)
        {
            return await this.context.Compracubos.Where(p => p.IDUsuario == idusuario).ToListAsync();
        }
    }
}
