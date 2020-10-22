using Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UpdateCommunicate updateCommunicate;

        ActivateCommunicate activateCommunicate;

        public MainWindow()
        {
            InitializeComponent();

            updateCommunicate = new UpdateCommunicate();
            activateCommunicate = new ActivateCommunicate();

            updateCommunicate.DisplayUpdateResultEvent += DisplayUpdateResult;
            updateCommunicate.DisplayUpdateSettingResultEvent += DisplayUpdateSettingResult;
        }

        private void DisplayUpdateResult(int updateResult)
        {
            try
            {
                Utilities.GUILog($"Main DisplayUpdateResult");

                if (updateResult == (int)Utilities.UpdateResult.Update_OK)
                {
                    MessageBox.Show("Main DisplayUpdateResult OK");
                }
                else if (updateResult == (int)Utilities.UpdateResult.Update_Fail)
                {
                    MessageBox.Show("Main DisplayUpdateResult Fail");
                }
                else
                {
                    MessageBox.Show("Main DisplayUpdateResult Unknown");
                }
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"Main DisplayUpdateResult Exception: {ex.ToString()}");
            }
        }

        private void DisplayUpdateSettingResult(int updateSettingResult)
        {
            try
            {
                Utilities.GUILog($"Main DisplayUpdateSettingResult");

                if (updateSettingResult == (int)Utilities.UpdateSettingResult.UpdateSetting_OK)
                {
                    MessageBox.Show("Main DisplayUpdateSettingResult OK");
                }
                else if (updateSettingResult == (int)Utilities.UpdateSettingResult.UpdateSetting_Fail)
                {
                    MessageBox.Show("Main DisplayUpdateSettingResult Fail");
                }
                else
                {
                    MessageBox.Show("Main DisplayUpdateSettingResult Unknown");
                }
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"Main DisplayUpdateSettingResult Exception: {ex.ToString()}");
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            updateCommunicate.ExecuteUpdate();
        }

        private void UpdateSetting_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\Code\UpdateSettingPath";
            updateCommunicate.ExecuteUpdateSetting(filePath);
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            string activatePath = @"C:\Code\ActivatePath";
            activateCommunicate.Activation(activatePath);
        }

        private void Deactivate_Click(object sender, RoutedEventArgs e)
        {
            activateCommunicate.Deactivation();
        }
    }
}
