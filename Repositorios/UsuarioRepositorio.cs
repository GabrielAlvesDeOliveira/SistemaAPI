using Microsoft.EntityFrameworkCore;
using SistemaAPI.Data;
using SistemaAPI.Model;
using SistemaAPI.Repositorios.Interfaces;

namespace SistemaAPI.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaApiDBContex _dbContex;
        public UsuarioRepositorio(SistemaApiDBContex sistemaApiDBContex)
        {
            _dbContex = sistemaApiDBContex;
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContex.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContex.Usuarios.ToListAsync();
        }
        
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContex.Usuarios.AddAsync(usuario);
            await _dbContex.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuario Para o ID: {id} não foi encontrado no banco de dados.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContex.Usuarios.Update(usuarioPorId);
            await _dbContex.SaveChangesAsync();

            return usuarioPorId;
        }
        
        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario Para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContex.Usuarios.Remove(usuarioPorId);
            await _dbContex.SaveChangesAsync();
            return true;
        }

    }
}
