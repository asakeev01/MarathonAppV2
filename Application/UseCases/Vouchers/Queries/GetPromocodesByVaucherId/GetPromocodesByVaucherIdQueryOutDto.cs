﻿using Core.Common.Bases;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;
using Gridify;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public record GetPromocodesByVaucherIdQueryOutDto : BaseDto<Voucher, GetPromocodesByVaucherIdQueryOutDto>
{
    public int Id { get; set; }
    public string VoucherName { get; set; }
    public int MarathonId { get; set; }
    public bool IsActive { get; set; }
    public QueryablePaging<PromocodeDto> Promocodes { get; set; }


    public record PromocodeDto : BaseDto<Promocode, PromocodeDto>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreationDate { get; set; }
        public string Distance { get; set; }
        public string? Number { get; set; }
        public UserDto User { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Distance, y => y.Distance.Name)
                .Map(x => x.Number, y => y.Application == null ? null : y.Application.Number.ToString());
        }
    }
        

    public record UserDto : BaseDto<User, UserDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string FullNameR { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.FullName, y => $"{y.Surname} {y.Name}")
                .Map(x => x.FullNameR, y => $"{y.Name} {y.Surname}");
        }
    }
    public class DistanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
