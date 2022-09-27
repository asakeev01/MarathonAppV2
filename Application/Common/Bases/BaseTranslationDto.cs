using Mapster;

namespace Core.Common.Bases;

public abstract record BaseTranlationDto<TSource, TTranslation, TDestination> : IRegister
    where TDestination : class, new()
    where TSource : class, new()
    where TTranslation : class, new()
{
    private TypeAdapterConfig Config { get; set; }

    public virtual void AddCustomMappings() { }

    public static TDestination MapFrom((TSource, TTranslation) entity)
    {
        return entity.Adapt<TDestination>();
    }

    protected TypeAdapterSetter<(TSource, TTranslation), TDestination> SetCustomMappings() => Config.ForType<(TSource, TTranslation), TDestination>();

    public void Register(TypeAdapterConfig config)
    {
        Config = config;
        AddCustomMappings();
    }
}
