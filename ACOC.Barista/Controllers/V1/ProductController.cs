using ACOC.Barista.Contracts.http;
using ACOC.Barista.Models;
using ACOC.Barista.Repositiories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ACOC.Barista.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRepository<ProductTemplate> productRepository;
        private readonly IMapper mapper;

        public ProductController(ILogger<ProductController> logger, IRepository<ProductTemplate> productRepository, IMapper mapper)
        {
            _logger = logger;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet(Name = "GetProducts"), ProducesResponseType(typeof(IEnumerable<Product>),200)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await productRepository.GetAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when getting products");
                throw;
            }
        
        }
        [HttpPost(Name = "PostProduct"), ProducesResponseType(200)]
        public async Task<IActionResult> Post(ProductTemplateDTO productTemplateDTO)
        {
            try
            {
                var productTemplate = mapper.Map<ProductTemplate>(productTemplateDTO);               
                return Ok(await productRepository.CreateAsync(productTemplate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error when posting producttemplate");
                throw;
            }
      
        }
    }

}
