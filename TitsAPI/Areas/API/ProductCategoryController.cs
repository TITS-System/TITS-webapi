using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Responses;
using TitsAPI.Controllers;

namespace TitsAPI.Areas.API
{
    public class ProductCategoryController : TitsController
    {
        private IMapper _mapper;
        public ProductCategoryController(TitsDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategoryDto>> GetAll()
        {
            var productCategories = await Context.ProductCategories.ToListAsync();

            return _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);
        }
    }
}