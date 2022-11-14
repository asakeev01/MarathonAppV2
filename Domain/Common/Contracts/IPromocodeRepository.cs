using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using Domain.Entities.Vouchers;

namespace Domain.Common.Contracts;

public interface IPromocodeRepository : IBaseRepository<Promocode>{
    Task GeneratePromocodes(Voucher voucher, Marathon marathon, Distance distance, int quantity);
}