namespace WebApplication.Mappers
{
    public interface IMapper<in TSource, out TDestination>
    {
        public TDestination Map(TSource source);
    }
}
