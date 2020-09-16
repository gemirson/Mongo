using DroneDelivery.Data.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mongo
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DroneDbContext _context;

        public UsuarioRepository(DroneDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuarios.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.InsertOneAsync(usuario);
           
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios
                                 .Find(Builders<Usuario>.Filter.Eq("Email", email))
                                 .FirstOrDefaultAsync();
          
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuarios
                                .Find(Builders<Usuario>.Filter.Eq("UsuarioId", id))
                                .FirstOrDefaultAsync();
        }

    }
}

