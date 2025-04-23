using ExamenAzureCubos.Data;
using ExamenAzureCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenAzureCubos.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<Cubo> FindCuboAsync(string marca)
        {
            return await this.context.Cubos.FirstOrDefaultAsync(x => x.Marca == marca);
        }

        public async Task<int> GetMaxIdUsuario()
        {
            var maxId = await this.context.Usuarios.MaxAsync(p => p.IdUsuario);
            return maxId + 1;
        }

        public async Task InsertUsuarioAsync
            (string nombre, string email, string pass, string imagen)
        {
            int idUsuario = GetMaxIdUsuario().Result;

            Usuario usuario = new Usuario();
            usuario.IdUsuario = idUsuario;
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Pass = pass;
            usuario.Imagen = imagen;

            await this.context.Usuarios.AddAsync(usuario);
            await this.context.SaveChangesAsync();
        }

        public async Task<Usuario> GetPerfilAsync(string email)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Usuario> LogInUsuairosAsync(string email, string pass)
        {
            return await this.context.Usuarios
                .Where(x => x.Email == email && x.Pass == pass)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Compra>> GetComprasByUsuarioAsync(int idUsuario)
        {
            return await this.context.Compras
                .Where(p => p.IdUsuario == idUsuario)
                .ToListAsync();
        }

        public async Task InsertPedidoAsync(int idCubo, int idUsuario)
        {
            Compra compra = new Compra
            {
                IdCubo = idCubo,
                IdUsuario = idUsuario,
                FechaPedido = DateTime.Now
            };
            this.context.Compras.Add(compra);
            await this.context.SaveChangesAsync();
        }
    }
}
