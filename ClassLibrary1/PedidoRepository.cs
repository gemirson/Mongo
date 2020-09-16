using DroneDelivery.Data.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mongo
{
    public class PedidoRepository:IPedidoRepository
    {
        private readonly DroneDbContext _context;

        public PedidoRepository(DroneDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.InsertOneAsync(pedido);
        }

        public async Task<IEnumerable<Pedido>> ObterDoClienteAsync(Guid usuarioId)
        {
            var query = Builders<Pedido>.Filter.Eq("UsuarioId", usuarioId);

            var options = new FindOptions<Pedido>()
            {
                Projection = Builders<Pedido>.Projection
                    .Include(p => p.Usuario)
                    .Include(p => p.Drone)
                    
            };

            return await _context.Pedidos.Find(query, options).ToListAsync();
           
        }


        public async Task CriarHistoricoPedidoAsync(IEnumerable<Pedido> pedidos)
        {
            foreach (var pedido in pedidos)
                await _context.HistoricoPedidos.InsertOneAsync(HistoricoPedido.Criar(pedido.DroneId.GetValueOrDefault(), pedido.Id));
        }

        public async Task<IEnumerable<HistoricoPedido>> ObterHistoricoPedidosDoDroneAsync(Guid droneId)
        {
            return await _context.HistoricoPedidos.Find(Builders<HistoricoPedido>.Filter.Eq("DroneId",droneId))
                                .ToListAsync();

        }
        public async Task<Pedido> ObterPorIdAsync(Guid pedidoId)
        {
            return await _context.Pedidos.Include(x => x.Usuario).FirstOrDefaultAsync(x => x.Id == pedidoId);
        }
    

    }
}
