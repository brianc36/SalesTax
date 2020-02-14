using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SalesTax.Items;

namespace SalesTax.Models
{
    public class Cart
    {
        public List<Item> Items { get; set; }  = new List<Item>();
        
        public void AddItem(Item item)
        {
            Item listItem = Items.FirstOrDefault(s => s.Description == item.Description);

            if (listItem == null)
            {
                Items.Add(item);

            }
            else
            {
                listItem.IncrementQty(item.Qty);

            }
        }

        public string PrintTax()
        {
            var totalSalesTax = Items.Sum(x => x.CalculateSalesTax() * x.Qty  );
            var totalImportTax = Items.Sum(x => x.CalculateImportTax() * x.Qty);
            var line = $"{(totalSalesTax + totalImportTax).ToString(CultureInfo.InvariantCulture)}";
            if (line.LastIndexOf('.') == line.Length - 2) line += "0";

            return line;

        }

        public string PrintTotal()
        {
            return $"{(Items.Sum(x => x.TotalCost()).ToString(CultureInfo.InvariantCulture))}";

        }




    }
}
