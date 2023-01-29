// See https://aka.ms/new-console-template for more information
using CPC.Models;
//Sample shopping cart
string[] cartList = { "7310865004703", "7310865004703", "5000112637922", "7310865004703", "7310865004703", "5000112637939" };
Cart myCart = new Cart() { cartItems = cartList.Select(c => new CartItem { EAN = c }).ToList()};
//Create Volume campaign
VolumeCampaign volumeCampaign = new VolumeCampaign() { campaignProduct = new Product { EAN = "7310865004703" }, minimumQuantity = 2, name = "Test Volume Campaign", Price = 30 };
//Create combo campaign
string[] comboItems = { "5000112637922","5000112637939","7310865004703","7340005404261","7310532109090","7611612222105"};
ComboCampaign comboCampaign = new ComboCampaign() { Price = 30 , campaignItems = comboItems.Select(c => new Product { EAN = c }).ToList() };

//Calculate Cart price for each campaign
var volumePrice = volumeCampaign.CalculatePrice(myCart);
var comboPrice = comboCampaign.CalculatePrice(myCart);


//OUTPUT
Console.WriteLine("/YOUR SHOPPING CART"); 
Console.WriteLine("EAN         PRICE   |");
myCart.cartItems.ForEach(i => Console.WriteLine($"{i.EAN}   | {i.Price}"));
Console.WriteLine("----------------------------");
Console.WriteLine($"ORIGINAL PRICE: {myCart.CalculatePrice()} SEK");
Console.WriteLine("----------------------------");
Console.WriteLine($"Combo Price: {comboPrice} SEK");
Console.WriteLine($"Volume Price: {volumePrice} SEK");