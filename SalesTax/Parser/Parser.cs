using System.Collections.Generic;
using System.Text;

namespace SalesTax.Parser
{
    public class Parser
    {
        public bool IsValid { get; }
        public string Description { get; }
        public decimal Price { get; }
        public int Qty { get; }
        public string Category { get; private set; }
        public bool IsTaxExempt { get; }
        public bool IsImport { get; }

        public Parser(string input)
        {
            string[] fields = input.Split(" ");

            if (fields.Length < 4) return;

            bool qtyValid = int.TryParse(fields[0], out var qty);

            if(!qtyValid) return;

            Qty = qty;

            int start = 1;

            if (fields[1].ToLower() == "imported")
            {
                IsImport = true;
                start = 2;
            }

            StringBuilder strBuilder = new StringBuilder();

            for (int i = start; i < fields.Length - 2; i++)
            {
                 strBuilder.Append(fields[i] + " ");

            }

            Description = strBuilder.ToString().TrimEnd();

            bool priceValid = decimal.TryParse(fields[fields.Length - 1], out var price);

            if (!priceValid) return;

            Price = price;

            Dictionary<string, string> itemTypes = new Dictionary<string, string>
            {
                {"book", "book"},
                {"box of chocolates", "food"},
                {"chocolate bar", "food"},
                {"packet of headache pills", "medical"}
            };


            // exception of books, food, and medical product
            if (itemTypes.ContainsKey(Description.ToLower()))
            {
                IsTaxExempt = true;
                Category = itemTypes[Description.ToLower()];
            }

            IsValid = true;

        }

    }

}
