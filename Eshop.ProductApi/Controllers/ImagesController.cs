using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Core.Extensions.ErrorHandling;
using Eshop.ProductApi.Contracts;
using Eshop.ProductApi.ErrorHandling;
using Eshop.Products.Model.Context;
using Eshop.Products.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.ProductApi.Controllers
{
    /// <summary>
    /// Images control
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productContext">Product database context</param>
        /// <param name="mapper"></param>
        public ImagesController(ProductContext productContext, IMapper mapper)
        {
            _productContext = productContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id">Image identity</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<FileContentResult> Get([FromRoute][Required] long id)
        {
            var entity = await GetEntity(id);
            return File(entity.Data, entity.Type, entity.Name);
        }

        /// <summary>
        /// Upload image 
        /// </summary>
        /// <param name="uploadRequest">Upload image request</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UploadImageResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<UploadImageResponse> Upload([FromForm] UploadImageRequest uploadRequest)
        {
            var entity = _mapper.Map<Image>(uploadRequest);

            await _productContext.AddAsync(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return _mapper.Map<UploadImageResponse>(entity);
        }

        /// <summary>
        /// Delete image 
        /// </summary>
        /// <param name="id">Image identity</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(StatusResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AppError<ErrorCode>), (int)HttpStatusCode.BadRequest)]
        public async Task<StatusResponse> Delete([FromRoute][Required] long id)
        {
            var entity = await GetEntity(id);

            _productContext.Images.Remove(entity);

            await _productContext.SaveChangesAsync(HttpContext.RequestAborted);

            return new StatusResponse("OK");
        }

        private async Task<Image> GetEntity(long id)
        {
            var entity = await _productContext.Images
                .FirstOrDefaultAsync(x => x.Id == id, HttpContext.RequestAborted);

            if (entity == null)
            {
                throw ErrorFactory.Create(ErrorCode.NotFound, $"Image with identity '{id}' not found");
            }

            return entity;
        }
    }
}
