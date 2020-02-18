using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesTax.Items;
using SalesTax.Models;

namespace SalesTax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISalesTax _salesTax;
        private readonly IImportTax _importTax;
        private readonly Test.Test _test;
  
        public HomeController(ISalesTax salesTax, IImportTax importTax, Test.Test test)
        {
            _salesTax = salesTax;
            _importTax = importTax;
            _test = test;
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

            //Create cart and run test.
            var cart = _test.RunTest(example);

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

            //Create cart and run test.
            var cart = _test.RunTest(data);

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
