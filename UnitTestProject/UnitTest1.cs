using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTax.Parser;
using static SalesTax.Parser.Parser;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ItemValid()
        {
            //Arrange
            var data = "1 Book at 12.49";

            //Act
            var item = new Parser(data);

            //Assert
            Assert.IsTrue(item.IsValid);

        }

        [TestMethod]
        public void ItemInvalid()
        {
            //Arrange
            var data = "1 Book 12.49";

            //Act
            var item = new Parser(data);

            //Assert
            Assert.IsFalse(item.IsValid);

        }

        [TestMethod]
        public void ItemTaxExempt()
        {
            //Arrange
            var data = "1 Packet of headache pills at 9.75";

            //Act
            var item = new Parser(data);

            //Assert
            Assert.IsTrue(item.IsTaxExempt);

        }

        [TestMethod]
        public void ItemIsImported()
        {
            //Arrange
            var data = "1 Imported bottle of perfume at 27.9";

            //Act
            var item = new Parser(data);

            //Assert
            Assert.IsTrue(item.IsImport);

        }

        [TestMethod]
        public void ImportTax()
        {
            //Arrange
            var data = "1 Imported box of chocolates at 11.25";
            var itemData = new Parser(data);
            var importTaxCalculator = new SalesTax.Items.ImportTax(".05");
            var salesTaxCalculator = new  SalesTax.Items.SalesTax (".10");

            var item =  new SalesTax.Items.Item(itemData.Description, itemData.Price, itemData.Qty, itemData.Category, itemData.IsTaxExempt, itemData.IsImport, salesTaxCalculator, importTaxCalculator);



            //Act
            var importTax = item.CalculateImportTax();

            //Assert
            Assert.IsTrue(importTax == .60m);

        }

        [TestMethod]
        public void SalesTax()
        {
            //Arrange
            var data = "1 Bottle of perfume at 18.99";
            var itemData = new Parser(data);
            var importTaxCalculator = new SalesTax.Items.ImportTax(".05");
            var salesTaxCalculator = new  SalesTax.Items.SalesTax (".10");

            var item =  new SalesTax.Items.Item(itemData.Description, itemData.Price, itemData.Qty, itemData.Category, itemData.IsTaxExempt, itemData.IsImport, salesTaxCalculator, importTaxCalculator);
            
            //Act
            var salesTax = item.CalculateSalesTax();

            //Assert
            Assert.IsTrue(salesTax == 1.90m);

        }

        [TestMethod]
        public void ItemTotalCost()
        {
            //Arrange
            var data = "1 Imported Bottle of perfume at 27.99";
            var itemData = new Parser(data);
            var importTaxCalculator = new SalesTax.Items.ImportTax(".05");
            var salesTaxCalculator = new  SalesTax.Items.SalesTax (".10");

            var item =  new SalesTax.Items.Item(itemData.Description, itemData.Price, itemData.Qty, itemData.Category, itemData.IsTaxExempt, itemData.IsImport, salesTaxCalculator, importTaxCalculator);

            //Act
            var totalCost = item.TotalCost();

            //Assert
            Assert.IsTrue(totalCost == 32.19m);

        }

    }
}
