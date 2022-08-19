using Common.Helpers;
using Mapster;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Partners;
using Models.Applications;
using Models.SavedFiles;
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

        #region SavedFiles
        TypeAdapterConfig<SavedFile, SavedFileModel.GetFiles>
        .NewConfig()
        .Map(x => x.Path, x => AppConstants.BaseUri + x.Path);
        #endregion

        #region Applications
        TypeAdapterConfig<Application, ApplyModel>
            .NewConfig();
        #endregion
    }
}
