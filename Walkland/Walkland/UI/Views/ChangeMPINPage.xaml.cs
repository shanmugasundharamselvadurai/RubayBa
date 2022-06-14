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
    public partial class ChangeMPINPage : MvxContentPage<ChangeMPINPageViewModel>
    {
        List<Label> labels;
        List<Label> labels1;
        List<Label> labels2;
        public ChangeMPINPage()
        {
            InitializeComponent();
            label1.Focus();
            //Old MPIN
            labels = new List<Label>
            {
                label1,
                label2,
                label3,
                label4
            };

            //New MPIN
            labels1 = new List<Label>
            {
                label5,
                label6,
                label7,
                label8
            };

            //Confirm New MPIN
            labels2 = new List<Label>
            {
                label9,
                label10,
                label11,
                label12
            };

        }

        #region Old MPIN
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
        #endregion Old MPIN

        #region New MPIN
        private void Editor_TextChanged1(object sender, TextChangedEventArgs e)
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
            for (int i = 0; i < labels1.Count; i++)
            {
                Label lb = labels1[i];
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
        #endregion New MPIN

        #region Confirm New MPIN 
        private void Editor_TextChanged2(object sender, TextChangedEventArgs e)
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
            for (int i = 0; i < labels2.Count; i++)
            {
                Label lb = labels2[i];
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
        #endregion Confirm New MPIN       
    }
}