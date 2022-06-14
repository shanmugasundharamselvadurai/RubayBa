using MvvmCross.Forms.Views;
using System.Collections.Generic;
using System.ComponentModel;
using Walkland.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Walkland.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class RFTMPINPage : MvxContentPage<RFTMPINPageViewModel>
    {
        List<Label> labels;
        public RFTMPINPage()
        {
            InitializeComponent();
            labels = new List<Label>();
            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;
            Editor editor = sender as Editor;
            string editorStr = editor.Text;
            //if string.length lager than max length
            if (editorStr.Length > 4)
            {
                editor.Text = editorStr.Substring(0, 4);
            }
            //dismiss keyboard
            if (editorStr.Length >= 4)
            {
                editor.Unfocus();
            }
            for (int i = 0; i < labels.Count; i++)
            {
                Label lb = labels[i];
                if (i < editorStr.Length)
                {
                    lb.Text = editorStr.Substring(i, 1);
                }
                else
                {
                    lb.Text = "";
                }
            }
        }
    }
}