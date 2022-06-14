using Xamarin.Forms;

namespace Walkland.Core.Services
{
    public static class CommonServices
    {
        public static void ListenToSmsRetriever()
        {
            DependencyService.Get<IListenToSmsRetriever>()?.ListenToSmsRetriever();
        }
    }
    public interface IListenToSmsRetriever
    {
        void ListenToSmsRetriever();
    }

}