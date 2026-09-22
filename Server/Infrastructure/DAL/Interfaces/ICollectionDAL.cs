using Server.Application.DTOs;
using Server.Domain.Entities.Core;
using Server.Domain.Entities.Feature;
using Server.Domain.Models.Feature;

namespace Server.Infrastructure.DAL.Interfaces
{
    public interface ICollectionDAL
    {
        Task<CollectionGridResponse> GetAllData(FilterData filterData);

        Task<CollectionGridResponse> GetRecentData();

        Task<Response> Create(MilkCollectionEntity model);

        Task<Response> Update(MilkCollectionEntity model);

        Task<CollectionsDTO> GetById(Guid id);

        public Task<Response> Delete(MilkCollectionEntity model);
    }
}
