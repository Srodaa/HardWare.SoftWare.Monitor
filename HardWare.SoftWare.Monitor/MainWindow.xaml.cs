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
using System.Diagnostics;

namespace HardWare.SoftWare.Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Szoftverek> szoftv = new List<Szoftverek>();
        //PerformanceCounter CPUMonitor = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        public MainWindow()
        {
            InitializeComponent();
            Szoftverek();
            CPU();
            GPU();
            Motherboard();
            //RAM();
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

        public void CPU()
        {
            ManagementObjectSearcher CPU = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_Processor");
            foreach (var x in CPU.Get())
            {
                cpunev.Content = x["Name"];
                cpumagok.Content = x["NumberOfCores"];
                cpuszalak.Content = x["ThreadCount"];
                cpuorajel.Content = x["MaxClockSpeed"];
            }

        }

        public void GPU()
        {
            ManagementObjectSearcher GPU = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_VideoController");
            foreach (var x in GPU.Get())
            {
                gpunev.Content = x["Name"];
                gpudriver.Content = x["DriverVersion"];

            }


        }

        public void Motherboard()
        {
            ManagementObjectSearcher Motherboard = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_Baseboard");
            foreach (var x in Motherboard.Get())
            {
                mbmodell.Content = x["Model"];
                mbgyarto.Content = x["Manufacturer"];
                mbnev.Content = x["Product"];
                mbszam.Content = x["SerialNumber"];

            }
        }

        public void RAM()
        {
            //ManagementObjectSearcher RAM = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_");

        }

        private void szoftverdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
