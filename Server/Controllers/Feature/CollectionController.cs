using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using Server.Controllers.Core;
using Server.Domain.Entities.Core;
using Server.Domain.Entities.Feature;

namespace Server.Controllers.Feature
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : BaseApiController
    {
        private readonly ICollectionService _collectionService;
        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost]
        [Route("GetAllData")]
        public async Task<ActionResult> GetAllData([FromBody] FilterData model)
        {
            try
            {
                var result = await _collectionService.GetAllData(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetRecentData")]
        public async Task<ActionResult> GetRecentData()
        {
            try
            {
                var result = await _collectionService.GetRecentData();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Create([FromBody] MilkCollectionEntity model)
        {
            try
            {
                model.CreatedBy = UserEmail;
                var result = await _collectionService.Create(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] MilkCollectionEntity model)
        {
            try
            {
                model.UpdatedBy = UserEmail;
                var result = await _collectionService.Update(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetById{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _collectionService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("Delete{recordId}")]
        public async Task<ActionResult> Delete(Guid recordId)
        {
            try
            {
                MilkCollectionEntity model = new MilkCollectionEntity();
                model.UpdatedBy = UserEmail;
                model.RecordId = recordId;
                var result = await _collectionService.Delete(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
