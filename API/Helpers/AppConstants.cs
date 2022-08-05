namespace Common.Helpers;

public static class AppConstants
{
    #region VirtualDir
    public static string BaseDir { get; set; }
    #endregion

    #region ServiceUri.Self
    public static string BaseUri { get; set; }

    public static string BaseFrontUri { get; set; }
    #endregion

    #region SuffixOfPaths
    public static string MarathonsSuffixOfPath { get; } = "Marathon";

    public static string PartnersSuffixOfPath { get; } = "Partners";
    #endregion

    #region SuffixOfUries
    public static string FrontSuffixOfChoosePlaceTokenUri { get; } = "PlaceChoices/";

    public static string FrontSuffixOfConfirmAppEmailTokenUri { get; } = "Tokens/";
    public static string FrontSuffixOfConfirmEmailParticipant { get; } = "new-participant/";
    #endregion
}
