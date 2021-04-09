using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.Db;
using Models.DTOs.Misc;
using Models.DTOs.Requests;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class MenuController : TitsController
    {
        private IMapper _mapper;

        public MenuController(TitsDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateIngredient([FromBody] CreateIngredientDto createIngredientDto)
        {
            var menuIngredient = _mapper.Map<MenuIngredient>(createIngredientDto);

            await Context.MenuIngredients.AddAsync(menuIngredient);
            await Context.SaveChangesAsync();

            return new CreatedDto(menuIngredient.Id);
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var menuProduct = _mapper.Map<MenuProduct>(createProductDto);

            await Context.MenuProducts.AddAsync(menuProduct);
            await Context.SaveChangesAsync();

            return new CreatedDto(menuProduct.Id);
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateProductPack(
            [FromBody] CreateProductPackDto createProductPackDto)
        {
            var menuProductPack = _mapper.Map<MenuProductPack>(createProductPackDto);

            await Context.MenuProductPacks.AddAsync(menuProductPack);
            await Context.SaveChangesAsync();

            return new CreatedDto(menuProductPack.Id);
        }
    }
}