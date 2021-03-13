using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Core.Extensions.ErrorHandling;
using Eshop.ProductApi.Contracts;
using Eshop.ProductApi.ErrorHandling;
using Eshop.ProductApi.Extensions;
using Eshop.Products.Model.Context;
using Eshop.Products.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.ProductApi.Controllers
{
    /// <summary>
    /// Сontrol product groups
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productContext">Product database context</param>
        /// <param name="mapper"></param>
        public ProductsController(ProductContext productContext, IMapper mapper)
        {
            _productContext = productContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get products list
        /// </summary>
        /// <param name="findRequest">Search query</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<List<ProductModel>> Get([FromQuery] [Required] FindRequest findRequest)
        {
            var groupsEntities = await _productContext.Products
                .AsNoTracking()
                .ApplyFilter(x => x.Name.Contains(findRequest.Search),
                    () => !string.IsNullOrWhiteSpace(findRequest.Search?.Trim()),
                    skip: findRequest.Skip, take: findRequest.Take)
                .ToListAsync(HttpContext.RequestAborted);

            return groupsEntities.Select(_mapper.Map<ProductModel>).ToList();
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product identity</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductModel> Get([FromRoute][Required] long id)
        {
            var entity = await GetEntity(id);
            return _mapper.Map<ProductModel>(entity);
        }

        /// <summary>
        /// Create product 
        /// </summary>
        /// <param name="createRequest">Create request</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductModel> Create([FromBody][Required] CreateProductRequest createRequest)
        {
            var entity = _mapper.Map<Product>(createRequest);

            await _productContext.AddAsync(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<ProductModel>(entity);
        }

        /// <summary>
        /// Update product 
        /// </summary>
        /// <param name="productGroup">Product  model</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductModel> Update([FromBody][Required] ProductModel productGroup)
        {
            var entity = await GetEntity(productGroup.Id);

            _mapper.Map(productGroup, entity);

            _productContext.Update(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<ProductModel>(entity);
        }

        /// <summary>
        /// Delete product 
        /// </summary>
        /// <param name="id">Product identity</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductModel> Delete([FromRoute][Required] long id)
        {
            var entity = await GetEntity(id);

            _productContext.Products.Remove(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<ProductModel>(entity);
        }

        private async Task<Product> GetEntity(long id)
        {
            var entity = await _productContext.Products
                .FirstOrDefaultAsync(x => x.Id == id, HttpContext.RequestAborted);

            if (entity == null)
            {
                throw ErrorFactory.Create(ErrorCode.NotFound, $"Product with identity '{id}' not found");
            }

            return entity;
        }
    }
}
