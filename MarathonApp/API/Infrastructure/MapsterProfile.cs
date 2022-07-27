﻿using Mapster;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Partners;

namespace MarathonApp.Infrastructure;

internal static class MapsterProfile
{
    public static void Register()
    {
        #region Partners
        TypeAdapterConfig<Partner, PartnerDto.List>
            .NewConfig();
        TypeAdapterConfig<Partner?, PartnerDto.Get>
            .NewConfig();
        #endregion
    }
}
