using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbOrder.Entities;
using System.Collections.Generic;

namespace MongoDbOrder.Services
{
    public class OrderTransaction
    {
        public void AddOrder(Order order)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetCollection();

            var document = new BsonDocument
            {
                { "CustomerName", order.CustomerName },
                { "District", order.District },
                { "City", order.City },
                { "TotalPrice", order.TotalPrice }
            };

            orderCollection.InsertOne(document);
        }

        public List<Order> GetAllOrders()
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetCollection();

            var orders = orderCollection.Find(new BsonDocument()).ToList();

            List<Order> orderList = new List<Order>();

            foreach (var order in orders)
            {
                orderList.Add(new Order
                {
                    OrderId = order["_id"].ToString(),
                    CustomerName = order["CustomerName"].ToString(),
                    District = order["District"].ToString(),
                    City = order["City"].ToString(),
                    TotalPrice = order["TotalPrice"].AsDecimal
                });

            }
            return orderList;
        }

        public void DeleteOrder(string orderId)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            orderCollection.DeleteOne(filter);
        }

        public void UpdateOrder(Order order)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(order.OrderId));
            var update = Builders<BsonDocument>.Update
                .Set("CustomerName", order.CustomerName)
                .Set("District", order.District)
                .Set("City", order.City)
                .Set("TotalPrice", order.TotalPrice);
            orderCollection.UpdateOne(filter, update);
        }

        public Order GetOrder(string orderId)
        {
            var connection = new MongoDbConnection();
            var orderCollection = connection.GetCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(orderId));
            var order = orderCollection.Find(filter).FirstOrDefault();
            if (order == null)
            {
                return null;
            }
            else
            {
                return new Order
                {
                    OrderId = order["_id"].ToString(),
                    CustomerName = order["CustomerName"].ToString(),
                    District = order["District"].ToString(),
                    City = order["City"].ToString(),
                    TotalPrice = order["TotalPrice"].AsDecimal
                };
            }
            
        }
    }
}
