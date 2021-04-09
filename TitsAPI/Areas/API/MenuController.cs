using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Requests;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class MenuController : TitsController
    {
        public MenuController(TitsDbContext context) : base(context)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task CreateIngredient([FromBody] CreateIngredientDto createIngredientDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task CreateProductPack([FromBody] CreateProductPackDto createProductPackDto)
        {
            throw new NotImplementedException();
        }
    }
}