﻿using Domain.Entities.Languages;
using Domain.Entities.SavedFiles;

namespace Domain.Entities.Marathons;

public class MarathonTranslation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string Place { get; set; }
    public int LanguageId { get; set; }
    public int MarathonId { get; set; }
    public SavedFile? Logo { get; set; }
    public int? LogoId { get; set; }
    public Marathon Marathon { get; set; }
    public Language Language { get; set; }
}
