using System;
using System.Windows;

namespace Modeling.LabOne.WpfGui
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow
    {
        public MessageWindow()
        {
            InitializeComponent();
            this.btnOK.Focus();
        }

        public MessageWindow(Window parent, String message): this()
        {
            this.Title = "Warning";
            this.lblMEssage.Content = message;
            this.Left = parent.Left + parent.Width / 2 - this.Width / 2;
            this.Top = parent.Top + parent.Height / 2 - this.Height / 2;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
