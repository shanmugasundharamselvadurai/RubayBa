using Android.Content;
using Walkland.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorRendererForAndroid))]
namespace Walkland.Droid
{
    public class EditorRendererForAndroid : EditorRenderer
    {
        public EditorRendererForAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            Control.SetCursorVisible(false);
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}