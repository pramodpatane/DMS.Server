using Server.Domain.Models.Core;
using Server.Domain.Models.Feature;

namespace Server.Application.DTOs
{
    public class CollectionGridResponse: GridResponse<CollectionsDTO>
    {
        public Decimal ThisMonthTotalCollection { get; set; }
        public Decimal TodaysTotalCollection { get; set; }
    }
}
