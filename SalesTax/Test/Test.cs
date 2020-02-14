using System.Collections.Generic;
using System.Linq;
using SalesTax.Items;
using SalesTax.Models;

namespace SalesTax.Test
{
    public  class Test
    {
        public ISalesTax SalesTax { get; }
        public IImportTax ImportTax { get; }

        public List<Parser.Parser> Ex1 = new List<Parser.Parser>
            {
                new Parser.Parser("1 Book at 12.49"),
                new Parser.Parser("1 Book at 12.49"),
                new Parser.Parser("1 Music CD at 14.99"),
                new Parser.Parser("1 Chocolate bar at 0.85")
            };

        public List<Parser.Parser> Ex2 = new List<Parser.Parser>
            {
               new Parser.Parser("1 Imported box of chocolates at 10.00"),
               new Parser.Parser("1 Imported bottle of perfume at 47.50"),
            };


        public List<Parser.Parser> Ex3 = new List<Parser.Parser>
            {
                new Parser.Parser("1 Imported bottle of perfume at 27.99"),
                new Parser.Parser( "1 Bottle of perfume at 18.99"),
                new Parser.Parser("1 Packet of headache pills at 9.75"),
                new Parser.Parser("1 Imported box of chocolates at 11.25"),
                new Parser.Parser("1 Imported box of chocolates at 11.25")
             };

        public Test(ISalesTax salesTax, IImportTax importTax)
        {
            SalesTax = salesTax;
            ImportTax = importTax;

        }
        
        public List<Parser.Parser> CustomData(string data)
        {
            List<Parser.Parser> parsed = new List<Parser.Parser>();

            string[] rows = data.Split(':');

            foreach (var row in rows)
            {
                Parser.Parser item = new Parser.Parser(row.Trim());

                if (item.IsValid) parsed.Add(item);
           

            }

            return parsed;
        }


        public Cart RunTest(int testNumber, string data = null )
        {
            List<Parser.Parser> items;
            var cart = new Cart();

            switch (testNumber)
            {
                case 1:
                    items = Ex1;
                    break;
                case 2:
                    items = Ex2;
                    break;
                case 3:
                    items = Ex3;
                    break;
                default:
                    items = CustomData(data);
                    break;
            }

            foreach (var item in items)
            {
                if (item.IsValid)
                {
                    if (cart.Items.Any(s => s.Description == item.Description))
                    {
                        var match = cart.Items.Find(s => s.Description == item.Description);
                        match.IncrementQty(item.Qty);
                    }
                    else
                    {
                        cart.Items.Add(new Item(item.Description, item.Price, item.Qty, item.Category, item.IsTaxExempt, item.IsImport, SalesTax, ImportTax));
                    }

                }
            }

            return cart;

        }
    }
}
