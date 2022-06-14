using System;
using Walkland.Core.Services;
using Java.Lang;
using Xamarin.Forms;

using Walkland.Droid;
using Application = Android.App.Application;
using Android.Gms.Auth.Api.Phone;
using Object = Java.Lang.Object;
using Android.Gms.Tasks;

[assembly: Dependency(typeof(ListenToSms))]
namespace Walkland.Droid
{
    public class ListenToSms : IListenToSmsRetriever
    {
        public void ListenToSmsRetriever()
        {
            SmsRetrieverClient client = SmsRetriever.GetClient(Application.Context);
            var task = client.StartSmsRetriever();
            task.AddOnSuccessListener(new SuccessListener());
            task.AddOnFailureListener(new FailureListener());
        }
        private class SuccessListener : Object, IOnSuccessListener
        {
            public void OnSuccess(Object result)
            {
            }
        }
        private class FailureListener : Object, IOnFailureListener
        {
            public void OnFailure(System.Exception e)
            {
            }

            public void OnFailure(Java.Lang.Exception e)
            {
                throw new NotImplementedException();
            }
        }
    }

}