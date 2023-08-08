using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace In_Anh.Models
{
    public class OrderModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string OrderId { get; set; }

        public string UserEmail { get; set; }
        
       public string Phone { get; set; }
        public int OrderTotal { get; set;}  
        public decimal PricePrice { get; set;}  
        public decimal PriceDiscount { get; set;}   
        public decimal PriceTotalDiscount { get;set;}
        public decimal PriceDiscountTotal { get;set;}
        
        
    }
}
