using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System.Collections.Generic;
using Walkland.Core.ViewModels;
using Xamarin.Forms;

namespace Walkland.UI.Views
{
    [MvxContentPagePresentation(NoHistory = true)]
    public partial class SetupMPINPage : MvxContentPage<SetupMPINPageViewModel>
    {
        private List<Label> _labels;

        public SetupMPINPage()
        {
            InitializeComponent();

            _labels = new List<Label>
            {
                label1,
                label2,
                label3,
                label4
            };
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

            for (int i = 0; i < _labels.Count; i++)
            {
                Label lb = _labels[i];
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