namespace Domain.Common.Constants;

public static class AppConstants
{
    public const string AppName = "MarathonApp";

    public static readonly string[] SupportedLanguages = { "en-us", "ru-ru", "ky-kg"};
    public static readonly int[] SupportedLanguagesIds = { 1, 2, 3 };
    public static readonly int NumberOfSupportedLanguages = 3;

    public static readonly string DefaultLanguage = "en";
    /// <summary>
    /// Update this value when you throw new Exceptions
    /// </summary>
    public const int CurrentMaxErrorCode = 9;
}