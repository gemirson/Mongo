using System;
using Microsoft.Extensions.Options;
using Mongo;
using MongoDB.Driver;
namespace DroneDelivery.Data.Data
{
    public class DroneDbContext
    {

        private readonly IMongoDatabase _database = null;

        public DroneDbContext(IOptions<DroneSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Usuario> Usuarios
        {
            get
            {
                return _database.GetCollection<Usuario>("Usuario");
            }
        }

        public IMongoCollection<Drone> Drones
        {
            get
            {
                return _database.GetCollection<Drone>("Drone");
            }


        }

        public IMongoCollection<Pedido> Pedidos
        {
            get
            {
                return _database.GetCollection<Pedido>("Pedido");
            }


        }

        public IMongoCollection<HistoricoPedido> HistoricoPedidos
        {
            get
            {
                return _database.GetCollection<HistoricoPedido>("HistoricoPedido");
            }


        }

    }
}
