namespace SalesTax.Items
{
    public interface ITax
    {
        decimal CalculateImportTax(decimal price);
    }
}
