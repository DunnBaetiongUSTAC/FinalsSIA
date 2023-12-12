using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using ActionResult = System.Web.Mvc.ActionResult;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace Shop.Controllers
{
    [Route("api/MySqlProducts")]
    [ApiController]
    public class MySqlProductsController : Controller
    {
        [HttpGet]
        public List<Product> Get()
        {
            var products = new MySqlRepository().GetProducts();
            return products;
        }

        [HttpPost]
        public Product Add(Product product)
        {
            var repository = new MySqlRepository();
            repository.AddProduct(product);
            return product;
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            var repository = new MySqlRepository();
            repository.UpdateProduct(product);

            return Json(new { Success = true });
        }

        [HttpDelete]
        public ActionResult DeleteProduct(int productId)
        {
            var repository = new MySqlRepository();
            repository.DeleteProduct(productId);

            return Json(new { Success = true });
        }

    }
}
