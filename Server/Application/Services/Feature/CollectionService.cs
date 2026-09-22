using Server.Application.DTOs;
using Server.Application.Interfaces;
using Server.Domain.Entities.Core;
using Server.Domain.Entities.Feature;
using Server.Infrastructure.DAL.Interfaces;

namespace Server.Application.Services.Feature
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionDAL _collectionDAL;
        public CollectionService(ICollectionDAL collectionDAL)
        {
            _collectionDAL = collectionDAL;
        }


        public async Task<CollectionGridResponse> GetAllData(FilterData filterData)
        {
            try
            {
                var result = await _collectionDAL.GetAllData(filterData);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CollectionGridResponse> GetRecentData()
        {
            try
            {
                var result = await _collectionDAL.GetRecentData();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> Create(MilkCollectionEntity model)
        {
            try
            {
                return await _collectionDAL.Create(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> Update(MilkCollectionEntity model)
        {
            try
            {
                return await _collectionDAL.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<CollectionsDTO> GetById(Guid id)
        {
            try
            {
                var response = await _collectionDAL.GetById(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> Delete(MilkCollectionEntity employee)
        {
            try
            {
                var response = await _collectionDAL.Delete(employee);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
