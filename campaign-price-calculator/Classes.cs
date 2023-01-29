using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CPC.Models
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
        public int Price { get; set; } = 50;
    }
    public class CartItem: Product {
        public int order { get; set; } = 0;
        public CartItem(int count) { order = count + 1; }
    }

    public class CampaignItem { 
        public List<CartItem> CartItems { get; set; } = new List<CartItem> ();
        public int Price { get; set; }
    }
    //Base class for new campaign
    public class Campaign {
        public string name { get; set; }
        //Calculates price when a campaign is applied
        public virtual int CalculatePrice(Cart cart) {
            return cart.CartItems.Sum(p => p.Price);
        }
    }
    public class VolumeCampaign:Campaign {
        public Product campaignProduct { get; set; }
        public int minimumQuantity { get; set; } = 1;
        public int Price { get; set; } = 0;

        public override int CalculatePrice(Cart cart)
        {
            var units = cart.CartItems.Where(p => p.EAN == campaignProduct.EAN).ToList();
            var otherunits = cart.CartItems.Where(p => p.EAN != campaignProduct.EAN).ToList();
            var otherprice = otherunits.Sum(o => o.Price);
            var rem = units.Count % minimumQuantity;
            return ((units.Count / minimumQuantity) * Price) + (rem * campaignProduct.Price) + otherprice;
         }
    }
    public class ComboCampaign: Campaign {
        private List<Product> campaignItems= new List<Product>();
        public int Price { get; set; } = 0;
        public override int CalculatePrice(Cart cart)
        {
            List<Product> ComboItems = new List<Product>();
            var units = cart.CartItems.Where( u => campaignItems.Contains(u)).ToList();
            var otherunits = cart.CartItems.RemoveAll(units);
            var otherprice = otherunits.Sum(o => o.Price);
            var rem = units.Count % 2;
            return ((units.Count / 2) * Price) + (rem * units.IndexOf(0).Price) + otherprice;
        }
    }
}
