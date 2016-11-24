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
using System.Diagnostics;


namespace DafuXuTP1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Process> list = new List<Process>();
        public MainWindow()
        {
            InitializeComponent();
            //listBox.ItemsSource = list;
            
            
        }

       

        
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //listBox.Items.Refresh();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo();
            p.StartInfo.FileName = "Premier.exe";
            list.Add(p);
            p.Start();
            //listBox.Items.Refresh();
            listBox.Items.Add(p.StartInfo.FileName + " " + p.Id);

            //MessageBox.Show(p.SessionId+ " " + p.Id,"ProcessID");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo();
            p.StartInfo.FileName = "Ballon.exe";
            list.Add(p);
            p.Start();
            //listBox.Items.Refresh();
            listBox.Items.Add(p.StartInfo.FileName + " " + p.Id);

            //MessageBox.Show(p.SessionId+ " " + p.Id,"ProcessID");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Process lp = list.FindLast(
               delegate (Process itr)
               {
                   return itr.ProcessName.Equals("Ballon");

               });
            if (lp != null)
            {
                lp.Kill();
                list.Remove(lp);
                listBox.Items.Remove(lp.StartInfo.FileName + " " + lp.Id);
            }
            else MessageBox.Show("Error: No such process");
        }


        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Process lp = list.FindLast(
             delegate (Process itr)
             {
                 return itr.ProcessName.Equals("Premier");

             });
            if (lp != null)
            {
                lp.Kill();
                list.Remove(lp);
                listBox.Items.Remove(lp.StartInfo.FileName + " " + lp.Id);
            }
            else MessageBox.Show("Error: No such process");
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (list.Count() != 0)
            {
                Process last = list.Last();
                listBox.Items.Remove(last.StartInfo.FileName + " " + last.Id);
                last.Kill();
                list.Remove(last);
            }else MessageBox.Show("Error: There is no Process running now");

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            int num = list.Count();
            while (num != 0) {
                Process last = list.Last();
                listBox.Items.Remove(last.StartInfo.FileName + " " + last.Id);
                last.Kill();
                list.Remove(last);
                num--;
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            int num = list.Count();
            while (num != 0)
            {
                Process last = list.Last();
                listBox.Items.Remove(last.StartInfo.FileName + " " + last.Id);
                last.Kill();
                list.Remove(last);
                num--;
            }
            System.Environment.Exit(1);
        }
    }
}
