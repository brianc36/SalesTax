using System;

namespace SalesTax.Items
{
    public class ImportTax : IImportTax
    {
        public decimal ImportTaxPct { get; }

        public ImportTax(string importTax)
        {
            bool result  = decimal.TryParse(importTax, out var importTaxValue);

            if (result)
            {
                ImportTaxPct = importTaxValue;
            }
            else
            {
                throw new Exception("Sales Tax Percent Error.");
            }
        }

        public decimal CalculateImportTax(decimal price)
        {
            var tax = price * ImportTaxPct;

            return TaxNickels(tax); 

        }

        private static decimal TaxNickels(decimal tax)
        {
            var taxNickels = Math.Round((Math.Round(tax * 20, MidpointRounding.AwayFromZero) / 20), 2);

            if (tax > taxNickels) taxNickels += .05m;

            return taxNickels;
        }
    }
}
