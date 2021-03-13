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
    public class ProductGroupsController: ControllerBase
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productContext">Product database context</param>
        /// <param name="mapper"></param>
        public ProductGroupsController(ProductContext productContext, IMapper mapper)
        {
            _productContext = productContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get product groups list
        /// </summary>
        /// <param name="findRequest">Search query</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductGroupModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<List<ProductGroupModel>> Get([FromQuery] [Required] FindRequest findRequest)
        {
            var groupsEntities = await _productContext.ProductGroups
                .AsNoTracking()
                .ApplyFilter(x => x.Name.Contains(findRequest.Search),
                    () => !string.IsNullOrWhiteSpace(findRequest.Search?.Trim()),
                    skip: findRequest.Skip, take: findRequest.Take)
                .ToListAsync(HttpContext.RequestAborted);

            return groupsEntities.Select(_mapper.Map<ProductGroupModel>).ToList();
        }

        /// <summary>
        /// Get product goup by id
        /// </summary>
        /// <param name="id">Product group identity</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductGroupModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductGroupModel> Get([FromRoute][Required] long id)
        {
            var entity = await GetEntity(id);
            return _mapper.Map<ProductGroupModel>(entity);
        }

        /// <summary>
        /// Create product group
        /// </summary>
        /// <param name="createRequest">Create request</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductGroupModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductGroupModel> Create([FromBody][Required] CreateProductGroupRequest createRequest)
        {
            var entity = _mapper.Map<ProductGroup>(createRequest);

            await _productContext.AddAsync(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<ProductGroupModel>(entity);
        }

        /// <summary>
        /// Update product group
        /// </summary>
        /// <param name="productGroup">Product group model</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ProductGroupModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductGroupModel> Update([FromBody][Required] ProductGroupModel productGroup)
        {
            var entity = await GetEntity(productGroup.Id);

            _mapper.Map(productGroup, entity);

            _productContext.Update(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<ProductGroupModel>(entity);
        }

        /// <summary>
        /// Delete product group
        /// </summary>
        /// <param name="id">Product group identity</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductGroupModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<ProductGroupModel> Delete([FromRoute][Required] long id)
        {
            var entity = await GetEntity(id);

            _productContext.ProductGroups.Remove(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<ProductGroupModel>(entity);
        }

        private async Task<ProductGroup> GetEntity(long id)
        {
            var entity = await _productContext.ProductGroups
                .FirstOrDefaultAsync(x => x.Id == id, HttpContext.RequestAborted);

            if (entity == null)
            {
                throw ErrorFactory.Create(ErrorCode.NotFound, $"Product group with identity '{id}' not found");
            }

            return entity;
        }
    }
}
