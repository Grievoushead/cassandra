namespace DI
{
    public static class IoC
    {
        private static IDependencyResolver resolver;

        public static void Initialize(IDependencyResolver resolver)
        {
            IoC.resolver = resolver;
        }

        public static T Resolve<T>()
        {
            return resolver.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return resolver.Resolve<T>(name);
        }
    }
}