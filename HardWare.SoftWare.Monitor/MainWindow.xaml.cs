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
using System.Management;
using Microsoft.Win32;

namespace HardWare.SoftWare.Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Szoftverek> szoftv = new List<Szoftverek>();
        public MainWindow()
        {
            InitializeComponent();
            Szoftverek();
        }

        public void Szoftverek()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey k = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string subkeyn in k.GetSubKeyNames())
                {
                    using (RegistryKey sk = k.OpenSubKey(subkeyn))
                    {
                        if (sk.GetValue("DisplayName") != null)
                        {
                            szoftv.Add(new Szoftverek
                            {
                                Nev = (string)sk.GetValue("DisplayName"),
                                Verzioszam = (string)sk.GetValue("DisplayVersion"),
                                Kozzetevo = (string)sk.GetValue("Publisher"),
                                Telepiteshelye = (string)sk.GetValue("InstallLocation"),
                            });
                        }
                    }
                }
            }
            szoftverdatagrid.ItemsSource = szoftv;

        }

        private void szoftverdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
