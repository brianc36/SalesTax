using System.Text;

namespace SalesTax.Items
{
    public class Item
    {
        public string Description { get; }
        public decimal Price { get; }
        public int Qty { get; set; }
        public string Category { get; }
        public bool  IsTaxExempt { get; }
        public bool IsImport { get; }

        public ISalesTax SalesTax { get; }

        public IImportTax ImportTax { get; }

        public Item(string description, decimal price, int qty, string category, bool isTaxExempt, bool isImport, ISalesTax salesTax, IImportTax importTax)
        {
            Description = description;
            Price = price;
            Qty = qty;
            Category = category;
            IsTaxExempt = isTaxExempt;
            IsImport = isImport;
            SalesTax = salesTax;
            ImportTax = importTax;
            
        }

        public void IncrementQty(int qty)
        {
            Qty += qty;
        }

        public decimal CalculateSalesTax()
        {
            if (IsTaxExempt)
                return 0m;

            return SalesTax.CalculateTax(Price);
        }

        public decimal CalculateImportTax()
        {
            if (IsImport)
            {
                return ImportTax.CalculateImportTax(Price);

            }

            return 0m;           
        }

        public decimal TotalPrice()
        {
            var total = (CalculateSalesTax() + CalculateImportTax() +  Price);

            return total;

        }


        public decimal TotalCost()
        {
            var total = (CalculateSalesTax() + CalculateImportTax() +  Price) * Qty;
            
            return total;

        }

        public string PrintTotal()
        {
            StringBuilder line = new StringBuilder();

            if (IsImport) line.Append("Imported ");

            line.Append(Description + ": ");

            if (Qty > 1)
            {
                line.Append($"{TotalCost()} ({Qty.ToString()} @ {TotalPrice()})");
            }
            else
            {
                line.Append(TotalCost());
            }

            return line.ToString();
        }
    }
}
