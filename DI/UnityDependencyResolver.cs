using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace DI
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        public UnityDependencyResolver()
        {
            container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return container.Resolve<T>(name);
        }
    }
}