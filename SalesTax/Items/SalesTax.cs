using System;

namespace SalesTax.Items
{
    public class SalesTax : ISalesTax
    {
        public decimal SalesTaxPct { get; }

        public SalesTax(string salesTax)
        {
            bool result  = decimal.TryParse(salesTax, out var salesTaxValue);

           if (result)
           {
               SalesTaxPct = salesTaxValue;
           }
           else
           {
               throw new Exception("Sales Tax Percent Error.");
           }

        }
        public decimal CalculateTax(decimal price)
        {
            var tax = price *  SalesTaxPct;

            return CalculateTaxNickels(tax); ;

        }

        private static decimal CalculateTaxNickels(decimal tax)
        {
            var taxNickels = Math.Round((Math.Round(tax * 20, MidpointRounding.AwayFromZero) / 20), 2);

            if (tax > taxNickels) taxNickels += .05m;

            return taxNickels;
        }
    }

  
}
