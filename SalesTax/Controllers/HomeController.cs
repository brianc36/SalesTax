using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesTax.Items;
using SalesTax.Test;
using SalesTax.Models;

namespace SalesTax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISalesTax _salesTax;
        private readonly IImportTax _importTax;
  
        public HomeController(ISalesTax salesTax, IImportTax importTax)
        {
            _salesTax = salesTax;
            _importTax = importTax;
        }

        public IActionResult Index()
        {
   
            return View();
        }
        /// <summary>
        /// Run example that corresponds to number.
        /// </summary>
        /// <param name="example"></param>
        /// <returns>Action Result</returns>
        [HttpGet]
        public IActionResult Results(int example)
        {
            ViewData["Example"] = example.ToString();

            //Setup test cases.
            var test = new Test.Test(_salesTax, _importTax );

            //Create cart and run test.
            var cart = test.RunTest(example);

            return View(cart);
        }

        /// <summary>
        /// Run custom input data. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Results(string data)
        {
            ViewData["Example"] = "Other";


            //Setup Test
            var test = new Test.Test(_salesTax, _importTax);

            //Create cart and run test.
            var cart = test.RunTest(data);

            return View(cart);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
