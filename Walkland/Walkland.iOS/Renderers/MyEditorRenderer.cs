using UIKit;
using Walkland.Mobile.Admin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(MyEditorRenderer))]
namespace Walkland.Mobile.Admin.iOS.Renderers
{
    public class MyEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var view = (Editor)Element;
                Control.TintColor = UIColor.White;
            }
        }
    }
}