using System.Collections.Generic;
using SalesTax.Items;
using SalesTax.Models;

namespace SalesTax.Test
{
    public class Test
    {
        public ISalesTax SalesTax { get; }
        public IImportTax ImportTax { get; }

        private readonly List<List<Parser.Parser>> _examples = new List<List<Parser.Parser>>
        {
            new List<Parser.Parser>
            {
                new Parser.Parser("1 Book at 12.49"),
                new Parser.Parser("1 Book at 12.49"),
                new Parser.Parser("1 Music CD at 14.99"),
                new Parser.Parser("1 Chocolate bar at 0.85")
            },
            new List<Parser.Parser>
            {
                new Parser.Parser("1 Imported box of chocolates at 10.00"),
                new Parser.Parser("1 Imported bottle of perfume at 47.50"),

            },
            new List<Parser.Parser>
            {
            new Parser.Parser("1 Imported bottle of perfume at 27.99"),
            new Parser.Parser( "1 Bottle of perfume at 18.99"),
            new Parser.Parser("1 Packet of headache pills at 9.75"),
            new Parser.Parser("1 Imported box of chocolates at 11.25"),
            new Parser.Parser("1 Imported box of chocolates at 11.25")
        }

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

        public Cart RunTest(string data)
        {
            return ProcessItems(CustomData(data));
        }

        public Cart RunTest(int testNumber)
        {
            return ProcessItems(_examples[testNumber - 1]);
        }

        private Cart ProcessItems(List<Parser.Parser> parserData)
        {
            var cart = Cart.CreateInstance();

            foreach (var item in parserData)
            {
                if (item.IsValid)
                {
                    cart.AddItem(new Item(item.Description, item.Price, item.Qty, item.Category, item.IsTaxExempt,
                        item.IsImport, SalesTax, ImportTax));
                }
            }

            return cart;
        }
    }
}
