using Common.Helpers;
using Mapster;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Partners;
using System.Configuration;

namespace MarathonApp.Infrastructure;

internal static class MapsterProfile
{
    public static void Register()
    {
        #region Partners
        TypeAdapterConfig<Partner, PartnerModel.ListPartner>
            .NewConfig()
            .Map(x => x.Image, x => AppConstants.BaseUri +  x.Image.Path);
        TypeAdapterConfig<Partner, PartnerModel.Get>
            .NewConfig()
            .Map(x => x.Image, x => AppConstants.BaseUri + x.Image.Path);
        #endregion

        #region Marathons
        TypeAdapterConfig<Marathon, MarathonModel.ListMarathon>
            .NewConfig();
        TypeAdapterConfig<Marathon, MarathonModel.GetMarathon>
    .NewConfig();
        #endregion
    }
}
