using System.Collections.Generic;
using Freedom.Domain.Entities;
using Freedom.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Freedom.MeuQueijo.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;

        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.All();
        }
    }
}
