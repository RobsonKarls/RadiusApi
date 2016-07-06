using System.Collections.Generic;
using Freedom.Domain.Entities;
using Freedom.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Freedom.MeuQueijo.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _repository.All();
        }
    }
}
