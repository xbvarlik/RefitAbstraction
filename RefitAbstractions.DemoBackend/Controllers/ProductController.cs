using Microsoft.AspNetCore.Mvc;
using Ntt.Exceptions.ExceptionTypes;
using Ntt.RefitAbstraction.Server;
using RefitAbstractions.DemoBackend.Models;

namespace RefitAbstractions.DemoBackend.Controllers;

[ApiController]
[Route("/product")]
public class ProductController(RequestHeaders requestHeaders) : ControllerBase
{
    [HttpGet("{id}")]
    public ProductViewModel GetProduct(int id)
    {
        // get request headers from http context
        var headers = HttpContext.Request.Headers;
        var reqHeaders = requestHeaders;
        
        return new ProductViewModel()
        {
            Id = id,
            Name = $"Product {id}"
        };
    }
    
    [HttpGet]
    public List<ProductViewModel> GetProducts()
    {
        return new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Product 1"
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Product 2"
            }
        };
    }
    
    [HttpPost]
    public ProductViewModel CreateProduct(ProductCreateModel model)
    {
        return new ProductViewModel()
        {
            Id = model.Id,
            Name = model.Name
        };
    }
    
    [HttpPut("{id}")]
    public ProductViewModel UpdateProduct(int id, ProductUpdateModel model)
    {
        return new ProductViewModel()
        {
            Id = id,
            Name = model.Name ?? $"Product {id}"
        };
    }
    
    [HttpDelete("{id}")]
    public void DeleteProduct(int id)
    {
        throw new ForbiddenException("You are not allowed to delete products.");
    }
}