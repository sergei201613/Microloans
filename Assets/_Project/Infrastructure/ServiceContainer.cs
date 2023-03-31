namespace Sgorey.Microloans.Infrastructure
{
    public class ServiceContainer
    {
        public static void RegisterShared<TService>(TService implementation) where TService : class, IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public static void UnregisterShared<TService>() where TService : class, IService
        {
            Implementation<TService>.ServiceInstance = null;
        }

        public static TService GetShared<TService>() where TService : class, IService
        {
            TService instance = Implementation<TService>.ServiceInstance;

            if (instance == null)
                throw new NoSuchServiceException($"No such service of type {typeof(TService)}");

            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : class, IService
        {
            public static TService ServiceInstance;
        }
    }
}