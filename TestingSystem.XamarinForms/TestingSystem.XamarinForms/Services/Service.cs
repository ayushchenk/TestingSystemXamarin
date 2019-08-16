using Plugin.Connectivity;

namespace TestingSystem.XamarinForms.Services
{
    public static class Service
    {
        public static bool HasInternetConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
