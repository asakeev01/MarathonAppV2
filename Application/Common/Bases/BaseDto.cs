using Mapster;

namespace Core.Common.Bases;

public abstract record BaseDto<TSource, TDestination> : IRegister
    where TDestination : class, new()
    where TSource : class, new()
{
    private TypeAdapterConfig Config { get; set; }

    public virtual void AddCustomMappings() { }
    
    public static TDestination MapFrom(TSource entity)
    {
        return entity.Adapt<TDestination>();
    }

    protected TypeAdapterSetter<TSource, TDestination> SetCustomMappings() => Config.ForType<TSource, TDestination>();
    protected TypeAdapterSetter<TDestination, TSource> SetCustomMappingsInverse() => Config.ForType<TDestination, TSource>();

    public void Register(TypeAdapterConfig config)
    {
        Config = config;
        AddCustomMappings();
    }
}
    