namespace Domain.Common.Constants;

public static class AppConstants
{
    public const string AppName = "MarathonApp";

    public static readonly string[] SupportedLanguages = { "en-us", "ru-ru", "ky-kg" };
    public static readonly int[] SupportedLanguagesIds = { 1, 2, 3 };
    public static readonly int NumberOfSupportedLanguages = 3;
    public static readonly List<Tuple<string, string>> ApplicationExcelColumns = new List<Tuple<string, string>>() {
        Tuple.Create("A1", "Id" ),
        Tuple.Create("B1", "Магнит"),
        Tuple.Create("C1", "Номер"),
        Tuple.Create("D1", "Имя"),
        Tuple.Create("E1", "Фамилия"),
        Tuple.Create("F1", "Почта"),
        Tuple.Create("G1", "Дата рождения"),
        Tuple.Create("H1", "Страна"),
        Tuple.Create("I1", "Телефон"),
        Tuple.Create("J1", "Пол"),
        Tuple.Create("K1", "Дистанция"),
        Tuple.Create("L1", "Способ оплаты"),
        Tuple.Create("M1", "Категория")
    };

    public static readonly string DefaultLanguage = "en";
    /// <summary>
    /// Update this value when you throw new Exceptions
    /// </summary>
    public const int CurrentMaxErrorCode = 9;
}