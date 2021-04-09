using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.DTOs.General;
using Models.DTOs.Misc;
using Models.DTOs.Requests;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class OrderController : TitsController
    {
        private IMapper _mapper;

        public OrderController(TitsDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> Get(long id)
        {
            var order = await Context.Orders
                .Include(o => o.ProductPacks)
                .ThenInclude(pp => pp.Products)
                .ThenInclude(p => p.Ingredients)
                .Include(o=>o.ProductPacks)
                .ThenInclude(pp=>pp.Products)
                .ThenInclude(p=>p.ProductCategory)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (order != null)
            {
                var orderDto = _mapper.Map<OrderDto>(order);

                return orderDto;
            }
            else
            {
                return TitsError(MessagesVerbatim.IdNotFound);
            }
        }
        
        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var accountSession = await GetRequestSession();
            
            var productPackTemplateIds = createOrderDto.ProductPackTemplateIds;

            var productPackTemplates = await Context.ProductPackTemplates
                .Include(ppt => ppt.Products)
                .ThenInclude(pt => pt.Ingredients)
                .Where(ppt => productPackTemplateIds.Contains(ppt.Id))
                .ToListAsync();
            
            var order = new Order
            {
                CreationDateTime = DateTime.Now,
                AccountId = accountSession.AccountId
            };

            foreach (var productPackTemplate in productPackTemplates)
            {
                OrderProductPack orderProductPack = new OrderProductPack()
                {
                    Price = productPackTemplate.Price,
                    Title = productPackTemplate.Title,
                    Order = order
                };

                foreach (var productTemplate in productPackTemplate.Products)
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        Price = productTemplate.Price,
                        Title = productTemplate.Title,
                        ProductCategoryId = productTemplate.ProductCategoryId,
                        ProductPack = orderProductPack
                    };
                    foreach (var ingredientTemplate in productTemplate.Ingredients)
                    {
                        OrderIngredient orderIngredient = new OrderIngredient()
                        {
                            Title = ingredientTemplate.Title,
                            Weight = ingredientTemplate.Weight,
                            Product = orderProduct
                        };

                        await Context.OrderIngredients.AddAsync(orderIngredient);
                    }

                    await Context.OrderProducts.AddAsync(orderProduct);
                }

                await Context.OrderProductPacks.AddAsync(orderProductPack);
            }

            await Context.Orders.AddAsync(order);
            
            await Context.SaveChangesAsync();
            
            return new CreatedDto(order.Id);
        }
    }
}