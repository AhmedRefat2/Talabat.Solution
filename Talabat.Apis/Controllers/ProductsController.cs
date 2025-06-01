using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Apis.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo)
        {
            _productsRepo = productsRepo;
        }

        // Get All 
        // BaseUrl/api/Products [GET]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await _productsRepo.GetAllAsync();

            //JsonResult result = new JsonResult(products);
            //result.StatusCode = 200;
            //return result;

            //OkObjectResult okObjectResult = new OkObjectResult(products);
            //return okObjectResult;

            return Ok(products);
        }

        // Get BY Id
        // BaseUrl/api/Products/id [GET]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsRepo.GetAsync(id);
            if (product != null)
                return Ok(product); // 200 
            return NotFound(); // 404
        }
    }
}   
