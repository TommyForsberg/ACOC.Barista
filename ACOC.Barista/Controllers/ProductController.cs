using ACOC.Barista.Models;
using ACOC.Barista.Repositiories;
using Microsoft.AspNetCore.Mvc;

namespace ACOC.Barista.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRepository<ProductTemplate> productRepository;

        public ProductController(ILogger<ProductController> logger, IRepository<ProductTemplate> productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts"), ProducesResponseType(typeof(IEnumerable<Product>),200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await productRepository.GetAsync());
        }
        [HttpPost(Name = "PostProduct"), ProducesResponseType(200)]
        public async Task<IActionResult> Post(ProductTemplate product)
        {
            await productRepository.CreateAsync(product);
            return Ok();
        }
    }

}
