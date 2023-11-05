using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CartServices.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CartId { get; set; }
        public string UserId { get; set; }
        public int Qty { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public DateTime Added_On { get; set; }
        public DateTime Updated_On { get; set; }
        public bool Is_Active { get; set; }

    }
}