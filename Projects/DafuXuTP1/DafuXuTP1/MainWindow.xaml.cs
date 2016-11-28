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
        private List<Process> list;
        private int ballon_num;
        private int premier_num;
        private const int max_instance_num = 5;

        public MainWindow()
        {
            InitializeComponent();
            this.list = new List<Process>();
        }

        private void killall()
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
            this.ballon_num = 0;
            this.premier_num = 0;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.premier_num < max_instance_num)
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = "Premier.exe";
                list.Add(p);
                this.premier_num++;
                p.Start();
                listBox.Items.Add(p.StartInfo.FileName + " " + p.Id);
            }
            else {
                MessageBox.Show("Error: maximum 5 Premier processes.");
            }
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.ballon_num < max_instance_num)
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo();
                p.StartInfo.FileName = "Ballon.exe";
                list.Add(p);
                this.ballon_num++;
                p.Start();
                listBox.Items.Add(p.StartInfo.FileName + " " + p.Id);
            }
            else {
                MessageBox.Show("Error: maximum 5 Ballon processes.");
            }
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
                this.ballon_num--;
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
                this.premier_num--;
                listBox.Items.Remove(lp.StartInfo.FileName + " " + lp.Id);
            }
            else
            {
                MessageBox.Show("Error: No such process");
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (list.Count() != 0)
            {
                Process last = list.Last();
                listBox.Items.Remove(last.StartInfo.FileName + " " + last.Id);
                if (last.ProcessName.Equals("Premier"))
                {
                    this.premier_num--;
                }
                else if (last.ProcessName.Equals("Ballon"))
                {
                    this.ballon_num--;
                }
                last.Kill();
                list.Remove(last);
            }
            else
            {
                MessageBox.Show("Error: There is no Process running now");
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            killall();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            killall();
            System.Environment.Exit(1);
        }

        private void menu_create_MouseEnter(object sender, RoutedEventArgs e)
        {
            create_premier.FontStyle = (this.premier_num < max_instance_num) ? FontStyles.Normal : FontStyles.Italic;
            create_ballon.FontStyle = (this.ballon_num < max_instance_num) ? FontStyles.Normal : FontStyles.Italic;
        }

        private void menu_delete_MouseEnter(object sender, RoutedEventArgs e)
        {
            int process_num = this.list.Count();

            delete_premier.IsEnabled = (this.premier_num > 0) ? true: false;
            delete_ballon.IsEnabled = (this.ballon_num > 0) ? true : false;
            delete_last.IsEnabled = (process_num > 0) ? true : false;
            delete_all.IsEnabled = (process_num > 0) ? true : false;
        }
    }
}
