using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class WindowApp : Window
    {
        public WindowApp()
        {
            InitializeComponent();

            lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
            lbResult.Items.Add(this.FindResource("strWindow").ToString());
            lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());


        }

        public void Error()
        {
            string s = null;
            try
            {
                // Error 1
                s.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("try-catch: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Error 2
            s.Trim();
        }

        private void btnClickMe_Click(object sender, RoutedEventArgs e)
        {
            Error();
        }
    }
}
