namespace DI
{
    public interface IDependencyResolver
    {
        T Resolve<T>(string name);

        T Resolve<T>();
    }
}