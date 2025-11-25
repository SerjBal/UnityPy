public interface IService {
    void Init();
}

public static class DI
{
    public static void Register<TService>(TService implementation) where TService : IService
    {
        Implementation<TService>.ServiceInstance = implementation;
    }


    public static TService Get<TService>() where TService : IService
    {
        return Implementation<TService>.ServiceInstance;
    }

    private class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}