using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using Domain.Entities.Vouchers;
using Domain.Entities.Vouchers.Exceptions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class PromocodeRepository : BaseRepository<Promocode>, IPromocodeRepository
{
    public PromocodeRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }


    public async Task GeneratePromocodes(Voucher voucher, Marathon marathon, Distance distance, int quantity)
    {
        if (quantity > distance.RemainingPlaces)
        {
            throw new NoPlacesForPromocodesException(distance.Name);
        }

        Random random = new Random();
        int lengthOfVoucher = 6;
        var generatedPromocodes = new List<string>();
        if (voucher.Promocodes != null)
        {
            generatedPromocodes = voucher.Promocodes.Where(x => x.DistanceId == distance.Id).Select(x => x.Code).ToList();
        }

        char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".ToCharArray();

        int i = 0;
        while (i < quantity)
        {
            var promocode = Enumerable
            .Range(1, lengthOfVoucher) // for(i.. )
            .Select(k => keys[random.Next(0, keys.Length - 1)])  // generate a new random char
            .Aggregate("", (e, c) => e + c); // join into a string
            promocode = promocode + "_" + voucher.Name +  "_" + distance.Name;
            promocode = promocode.Replace(" ", "");

            if (!generatedPromocodes.Contains(promocode))
            {
                i += 1;
                generatedPromocodes.Add(promocode);
                var entity = new Promocode
                {
                    Distance = distance,
                    Code = promocode,
                    Voucher = voucher,
                    CreationDate = DateTime.Now,
                };
                await this.CreateAsync(entity, save:true);
            }
        }
        distance.ReservedPlaces += quantity;
    }
}
