namespace In_Anh.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }    
        public string Region { get; set; }  
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ImageModel Image { get; set; }
        public DateTime Created { get; set; }
        
        public int OrderStatus { get; set; } 
    
        public decimal PriceStatus { get; set;}
        public decimal PriceTotal { get; set;}
       
        public int OrderTotal { get; set;}  
        public decimal PricePrice { get; set;}  
        public decimal PriceDiscount { get; set;}   
        public decimal PriceTotalDiscount { get;set;}
        public decimal PriceDiscountTotal { get;set;}
        
        
    }
}
