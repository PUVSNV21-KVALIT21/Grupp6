using hakimslivs.Data;
using hakimslivs.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace hakimslivs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartreadController : Controller
    {
        ApplicationDbContext context;
        public CartreadController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("/GetCartItems/{json?}")]
        public object GetCartItems([FromBody] object jObject)
        {
            // Lista som ska returneras
            var items = new List<CartItems>();
            if(jObject != null)
            {
                items = GetListWithItems(jObject);
            }
            
            // Returnera listan till javascriptet i JSON form
            return JsonConvert.SerializeObject(items);
        }

        //TODO: Anropa denna från Javascript när betalningen är klar
        [HttpPost]
        [Route("/GenerateOrder/{json?}")]
        public object GenerateOrder([FromBody] object jObject)
        {
            // Exempeldata
            // {"shopping-cart":"{"1":1,"2":1}","i18nextLng":"en-US"}

            var success = false;
            var items = new List<CartItems>();

            // Lista som ska returneras
            if(jObject != null)
            {
                items = GetListWithItems(jObject);
                //TODO: Spara beställning i databasen
                
                // Om allt gått bra sätt sucess till true och javascriptet
                // får rensa kundkorgen!
                success = true;
            }

            return JsonConvert.SerializeObject(success);
        }

        private List<CartItems> GetListWithItems(object jObject)
        {
            List<CartItems> items=new();
            // Konvertera objektet till string och sedan till en Dictionary
            var jsonString = jObject.ToString();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            foreach(var item in data)
            {
                if(item.Key == "shopping-cart")
                {
                    var cart = item.Value;
                    // Konvertera shoppingkarten till en Dictionary
                    var cartList = JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
                    foreach(var cartItem in cartList)
                    {
                        // Omvandla ID till int då den skickas in som sträng
                        _ = int.TryParse(cartItem.Key, out int id);
                        // Hitta varan som kunden vill ha
                        var curItem = context.Items.FirstOrDefault(item => item.ID == id);
                        if(curItem != null)
                        {
                            // Lägg till varan i listan om den inte är null
                            items.Add(new CartItems { Item = curItem, Amount = cartItem.Value });
                        }
                    }
                }
            }
            
            // Sortera varorna i listan efter namn
            items = items.OrderBy(item => item.Item.Product).ToList();
            return items;
        }
    }
}
