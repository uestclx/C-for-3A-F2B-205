using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary2;

namespace DafuXuTP2
{
    
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Thread> list = new List<Thread>();
        Stack<Thread> stackForBallon = new Stack<Thread>();
        Stack<Thread> stackForPremier = new Stack<Thread>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Thread threa = new Thread(new ThreadStart(ClassLibrary2.Ballon.CreateBallon().Go));
            list.Add(threa);
            stackForBallon.Push(threa);
            threa.Start();
            listBox.Items.Add("Ballon "+threa.ManagedThreadId);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Thread threa = new Thread(new ThreadStart(ClassLibrary2.NombrePremier.ThreadFunction));
            list.Add(threa);
            stackForPremier.Push(threa);
            threa.Start();
            listBox.Items.Add("Premier " + threa.ManagedThreadId);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            int id;
            Thread delete = null;
            if (stackForPremier.Count() != 0)
            {
                Thread threadWillBeDeleted = stackForPremier.Pop();
                id = threadWillBeDeleted.ManagedThreadId;
                listBox.Items.Remove("Premier " + id);
                foreach (Thread twbd in list)
                {
                    if (twbd.ManagedThreadId == id) delete = twbd;
                }
                list.Remove(delete);
                delete.Abort();
            }
            else MessageBox.Show("No more Process for Premier");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            int id;
            Thread delete = null;
            if (stackForBallon.Count() != 0)
            {
                Thread threadWillBeDeleted = stackForBallon.Pop();
                id = threadWillBeDeleted.ManagedThreadId;
                listBox.Items.Remove("Ballon " + id);
                foreach (Thread twbd in list)
                {
                    if (twbd.ManagedThreadId == id) delete = twbd;
                }
                list.Remove(delete);
                delete.Abort();
            }
            else MessageBox.Show("No more Process for Ballon");
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (list.Count() != 0)
            {
                if (stackForBallon.Count != 0 && stackForPremier.Count != 0)
                {
                    Thread delete = list.Last<Thread>();
                    int id = delete.ManagedThreadId;
                    Thread fromBallon = stackForBallon.Pop();
                    Thread fromPremier = stackForPremier.Pop();
                    if (fromBallon.ManagedThreadId == id)
                    {
                        stackForPremier.Push(fromPremier);
                        listBox.Items.Remove("Ballon " + id);
                    }
                    else
                    {
                        stackForBallon.Push(fromBallon);
                        listBox.Items.Remove("Premier " + id);
                    }
                    list.Remove(delete);
                    delete.Abort();
                }
                else if (stackForBallon.Count == 0)
                {
                    Thread delete = list.Last<Thread>();
                    int id = delete.ManagedThreadId;
                    if (stackForPremier.Count() != 0)
                    {
                        Thread fromPremier = stackForPremier.Pop();
                        listBox.Items.Remove("Premier " + id);
                        list.Remove(delete);
                        delete.Abort();
                    }
                    else
                    {
                        MessageBox.Show("No more Process");
                    }
                }
                else
                {
                    Thread delete = list.Last<Thread>();
                    int id = delete.ManagedThreadId;
                    if (stackForBallon.Count() != 0)
                    {
                        Thread fromBallon = stackForBallon.Pop();
                        listBox.Items.Remove("Ballon " + id);
                        list.Remove(delete);
                        delete.Abort();
                    }else
                    {
                        MessageBox.Show("No more Process");
                    }
                }

            }
            else MessageBox.Show("No more Process");
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            int num = list.Count();
            if (num != 0)
            {
                while (num != 0)
                {
                    if (stackForBallon.Count != 0 && stackForPremier.Count != 0)
                    {
                        Thread delete = list.Last<Thread>();
                        int id = delete.ManagedThreadId;
                        Thread fromBallon = stackForBallon.Pop();
                        Thread fromPremier = stackForPremier.Pop();
                        if (fromBallon.ManagedThreadId == id)
                        {
                            stackForPremier.Push(fromPremier);
                            listBox.Items.Remove("Ballon " + id);
                        }
                        else
                        {
                            stackForBallon.Push(fromBallon);
                            listBox.Items.Remove("Premier " + id);
                        }
                        list.Remove(delete);
                        delete.Abort();
                    }
                    else if (stackForBallon.Count == 0)
                    {
                        Thread delete = list.Last<Thread>();
                        int id = delete.ManagedThreadId;
                        if (stackForPremier.Count() != 0)
                        {
                            Thread fromPremier = stackForPremier.Pop();
                            listBox.Items.Remove("Premier " + id);
                            list.Remove(delete);
                            delete.Abort();
                        }
                        else
                        {
                            MessageBox.Show("No more Process");
                        }
                    }
                    else
                    {
                        Thread delete = list.Last<Thread>();
                        int id = delete.ManagedThreadId;
                        if (stackForBallon.Count() != 0)
                        {
                            Thread fromBallon = stackForBallon.Pop();
                            listBox.Items.Remove("Ballon " + id);
                            list.Remove(delete);
                            delete.Abort();
                        }
                        else
                        {
                            MessageBox.Show("No more Process");
                        }
                    }
                    num--;
                }
            }
            else MessageBox.Show("No more Process");
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            int num = list.Count();
            if (num != 0)
            {
                while (num != 0)
                {
                    if (stackForBallon.Count != 0 && stackForPremier.Count != 0)
                    {
                        Thread delete = list.Last<Thread>();
                        int id = delete.ManagedThreadId;
                        Thread fromBallon = stackForBallon.Pop();
                        Thread fromPremier = stackForPremier.Pop();
                        if (fromBallon.ManagedThreadId == id)
                        {
                            stackForPremier.Push(fromPremier);
                            listBox.Items.Remove("Ballon " + id);
                        }
                        else
                        {
                            stackForBallon.Push(fromBallon);
                            listBox.Items.Remove("Premier " + id);
                        }
                        list.Remove(delete);
                        delete.Abort();
                    }
                    else if (stackForBallon.Count == 0)
                    {
                        Thread delete = list.Last<Thread>();
                        int id = delete.ManagedThreadId;
                        if (stackForPremier.Count() != 0)
                        {
                            Thread fromPremier = stackForPremier.Pop();
                            listBox.Items.Remove("Premier " + id);
                            list.Remove(delete);
                            delete.Abort();
                        }
                        else
                        {
                            MessageBox.Show("No more Process");
                        }
                    }
                    else
                    {
                        Thread delete = list.Last<Thread>();
                        int id = delete.ManagedThreadId;
                        if (stackForBallon.Count() != 0)
                        {
                            Thread fromBallon = stackForBallon.Pop();
                            listBox.Items.Remove("Ballon " + id);
                            list.Remove(delete);
                            delete.Abort();
                        }
                        else
                        {
                            MessageBox.Show("No more Process");
                        }
                    }
                    num--;
                }
            }
            System.Environment.Exit(1);
        }
    }
}
