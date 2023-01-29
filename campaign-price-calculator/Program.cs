// See https://aka.ms/new-console-template for more information
using CPC.Models;
Console.WriteLine("Hello, World!");
//New cart
Cart mycart = new Cart();
string[] cartlist = {"7340005404261", "7310532109090" ,"5000112637922" ,"7310865004703"};
mycart.CartItems = cartlist.Select(c => new Product{EAN = c, Price=60});

//Volume campaign
VolumeCampaign vc = new VolumeCampaign();
vc.campaignProduct = new Product {EAN = "5000112637939"};
vc.Price = 90;
vc.name = "test";
//Create combo campaign
ComboCampaign cc = new ComboCampaign();
string[] combolist = {"5000112637922","5000112637939","7310865004703","7340005404261","7310532109090","7611612222105"};
cc.campaignItems = list.Select(c => new Product{EAN = c, Price=60});


void SeedData(){

}