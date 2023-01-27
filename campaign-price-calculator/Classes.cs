using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace campaign_price_calculator
{
   
    public class Cart { 
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void AddItem(Product product) {
            var item = new CartItem(CartItems.Count);
            CartItems.Add(item);
        }
    }
    public class Product {
        public string EAN { get; set; }
        public int Price { get; set; } = 0;
    }
    public class CartItem: Product {
        public int order { get; set; } = 0;
        public CartItem(int count) { order = count + 1; }
    }

    public class CampaignItem { 
        public List<CartItem> CartItems { get; set; } = new List<CartItem> ();
        public int Price { get; set; }
    }
    public class Campaign {
        public string name { get; set; }
        public virtual int CalculatePrice(List<Product> products) {
            return products.Sum(p => p.Price);
        }
    }
    public class VolumeCampaign:Campaign {
        public Product campaignProduct { get; set; }
        public int minimumQuantity { get; set; } = 1;
        public int Price { get; set; } = 0;

        public override int CalculatePrice(List<Product> products)
        {
            var units = products.Where(p => p.EAN == campaignProduct.EAN).ToList();
            var rem = units.Count % minimumQuantity;
            return ((units.Count / minimumQuantity) * Price) + (rem * campaignProduct.Price);
         }
    }
    public class ComboCampaign: Campaign {
        private List<Product> campaignItems= new List<Product>();
        public int Price { get; set; } = 0;
        public override int CalculatePrice(List<Product> products)
        {
            List<Product> ComboItems = new List<Product>();
            foreach (var p in products) {
                if (campaignItems.Contains(p))
                {
                    ComboItems.Add(p);
                    products.Remove(p);
                }
            }
            return (ComboItems.Count() / 2) * Price;
        }
    }
}
