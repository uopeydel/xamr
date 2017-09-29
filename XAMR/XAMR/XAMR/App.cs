#if __ANDROID__
using XAMR.Droid;
#elif __IOS__
using XAMR.iOS.Helpers;
#elif WINDOWS_UWP
using XAMR.UWP.Helpers;
#endif
using XAMR.Helpers;
using XAMR.Interfaces;
using XAMR.Services;
using XAMR.Model;

namespace XAMR
{
    public partial class App 
    {
        public App()
        {
        }

        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
            ServiceLocator.Instance.Register<IMessageDialog, MessageDialog>();
            ServiceLocator.Instance.Register<IDataStore<Item>, MockDataStore>();
        }
    }
}