using Dapper;
using Microsoft.Data.SqlClient;
using Server.Application.DTOs;
using Server.Domain.Entities.Core;
using Server.Domain.Entities.Feature;
using Server.Infrastructure.DAL.Interfaces;
using System.Data;

namespace Server.Infrastructure.DAL
{
    public class CollectionDAL : ICollectionDAL
    {
        private readonly string _connectionString;
        public CollectionDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<CollectionGridResponse> GetAllData(FilterData filterData)
        {
            try
            {
                CollectionGridResponse response = new CollectionGridResponse();

                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@value1", "select");
                    parameters.Add("@fdate", filterData.FromDate);
                    parameters.Add("@tdate", filterData.ToDate);
                    parameters.Add("@pageSize", filterData.Pagesize);
                    parameters.Add("@skip", filterData.Skip);
                    parameters.Add("@sqlSortString", filterData.SortString);
                    parameters.Add("@sqlFilterString", filterData.FilterString);

                    using (var multi = await connection.QueryMultipleAsync(
                        "USP_MilkCollectionsGridData",
                        parameters,
                        commandType: CommandType.StoredProcedure))
                    {
                        response.Data = (await multi.ReadAsync<CollectionsDTO>()).ToList();
                        response.TotalCount = await multi.ReadFirstAsync<int>();
                        response.ThisMonthTotalCollection = await multi.ReadFirstAsync<int>();
                        response.TodaysTotalCollection = await multi.ReadFirstAsync<int>();
                    }
                }

                return response;
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
                CollectionGridResponse response = new CollectionGridResponse();

                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@value1", "GetRecentRecords");

                    using (var multi = await connection.QueryMultipleAsync(
                        "USP_MilkCollections",
                        parameters,
                        commandType: CommandType.StoredProcedure))
                    {
                        response.Data = (await multi.ReadAsync<CollectionsDTO>()).ToList();
                        response.ThisMonthTotalCollection = await multi.ReadFirstAsync<int>();
                        response.TodaysTotalCollection = await multi.ReadFirstAsync<int>();
                    }
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CollectionsDTO> GetById(Guid recordId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@value1", "GetById");
                parameters.Add("@recordId", recordId);

                var data = await connection.QueryFirstOrDefaultAsync<CollectionsDTO>(
                    "USP_MilkCollections",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return data;
            }
        }

        public async Task<Response> Create(MilkCollectionEntity model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@value1", "insert");
                parameters.Add("@clientId", model.ClientId);
                parameters.Add("@collectionDate", model.CollectionDate);
                parameters.Add("@collectionTime", model.CollectionTime);
                parameters.Add("@collectionShift", model.CollectionShift);
                parameters.Add("@milkType", model.MilkType);
                parameters.Add("@quantity", model.Quantity);
                parameters.Add("@fat", model.FAT);
                parameters.Add("@SNF", model.SNF);
                parameters.Add("@temperature", model.Temperature);
                parameters.Add("@rate", model.Rate);
                parameters.Add("@totalAmount", model.TotalAmount);
                parameters.Add("@unit", model.UOM);
                parameters.Add("@paymentStatus", model.PaymentStatus);
                parameters.Add("@paymentMode", model.PaymentMode);
                parameters.Add("@paymentReference", model.PaymentReference);
                parameters.Add("@collectionCenter", model.CollectionCenter);
                parameters.Add("@description", model.Description);
                parameters.Add("@formCode", model.FormCode);
                parameters.Add("@user", model.CreatedBy);
                parameters.Add("@isActive", model.IsActive);
                parameters.Add("@isDeleted", model.IsDeleted);
                parameters.Add("@recordId", Guid.NewGuid());

                var data = await connection.ExecuteScalarAsync<int>(
                    "USP_MilkCollections",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new Response { IsSuccess = data > 0 };
            }
        }

        public async Task<Response> Update(MilkCollectionEntity model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@value1", "update");
                parameters.Add("@clientId", model.ClientId);
                parameters.Add("@collectionDate", model.CollectionDate);
                parameters.Add("@collectionTime", model.CollectionTime);
                parameters.Add("@collectionShift", model.CollectionShift);
                parameters.Add("@milkType", model.MilkType);
                parameters.Add("@quantity", model.Quantity);
                parameters.Add("@fat", model.FAT);
                parameters.Add("@SNF", model.SNF);
                parameters.Add("@temperature", model.Temperature);
                parameters.Add("@rate", model.Rate);
                parameters.Add("@totalAmount", model.TotalAmount);
                parameters.Add("@unit", model.UOM);
                parameters.Add("@paymentStatus", model.PaymentStatus);
                parameters.Add("@paymentMode", model.PaymentMode);
                parameters.Add("@paymentReference", model.PaymentReference);
                parameters.Add("@collectionCenter", model.CollectionCenter);
                parameters.Add("@description", model.Description);
                parameters.Add("@formCode", model.FormCode);
                parameters.Add("@user", model.CreatedBy);
                parameters.Add("@isActive", model.IsActive);
                parameters.Add("@isDeleted", model.IsDeleted);
                parameters.Add("@recordId", model.RecordId);

                var data = await connection.ExecuteScalarAsync<int>(
                    "USP_MilkCollections",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new Response { IsSuccess = data > 0, Message = "Record Updated Successfully!" };
            }
        }

        public async Task<Response> Delete(MilkCollectionEntity model)
        {
            await using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@VALUE1", "delete");
            parameters.Add("@user", model.UpdatedBy);
            parameters.Add("@RecordId", model.RecordId);

            var result = await connection.QueryFirstOrDefaultAsync<int>(
                "USP_MilkCollections",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (result <= 0)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Record not deleted!"
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Record deleted successfully!"
            };
        }
    }
}
