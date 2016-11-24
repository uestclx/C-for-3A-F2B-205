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
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemotingInterface;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RemotingInterface.IRemotChaine LeRemot;
        private Data data = new Data();
        private List<string> users = new List<string>();
        private List<string> messages = new List<string>();
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            // création d'un canal recepteur TCP
            TcpChannel canal = new TcpChannel();
            // enregistrement du canal
            ChannelServices.RegisterChannel(canal, false);

            // l'ojet LeRemot  récupére ici la référence de l'objet du serveur
            // on donne l'URI (serveur, port, classe du serveur)  et le nom de l'interface
            LeRemot = (RemotingInterface.IRemotChaine)Activator.GetObject(
                typeof(RemotingInterface.IRemotChaine), "tcp://localhost:2333/Serveur");

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(checkout);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string name = textBox1.Text;
            string message = textBox.Text;
            data = LeRemot.SendMessage(name,message);
            listBox.Items.Clear();
            listBox1.Items.Clear();
            messages = data.getMessages();
            users = data.getUsers();
            foreach (string n in messages)
            {
                listBox1.Items.Add(n);
            }

            foreach (string n in users)
            {
                listBox.Items.Add(n);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string name = textBox1.Text;
            data = LeRemot.Login(name);
            if(data.error == true)
            {
                MessageBox.Show("Error login,please choose another user name and try one try");
                return ;
            }

            users = data.getUsers();
            listBox.Items.Clear();
            foreach (string n in users)
            {
                listBox.Items.Add(n);
            }

            textBox1.IsEnabled = false;
            button.IsEnabled = true;
            textBox.IsEnabled = true;
            disconnect.IsEnabled = true;
            dispatcherTimer.Start();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
           
            
            
            /* while (true)
            {
                System.Threading.Thread.Sleep(3000);
                data = LeRemot.Fresh();
                listBox.Items.Clear();
                listBox1.Items.Clear();
                messages = data.getMessages();
                users = data.getUsers();
                foreach (string n in messages)
                {
                    listBox1.Items.Add(n);
                }

                foreach (string n in users)
                {
                    listBox.Items.Add(n);
                }
            }
            */
        }
        public void checkout(object sender, EventArgs e)
        {
            data = LeRemot.Fresh();
            listBox.Items.Clear();
            listBox1.Items.Clear();
            messages = data.getMessages();
            users = data.getUsers();
            foreach (string n in messages)
            {
                listBox1.Items.Add(n);
            }

            foreach (string n in users)
            {
                listBox.Items.Add(n);
            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string name = textBox1.Text;
            LeRemot.Disconnect(name);
            listBox.Items.Clear();
            listBox1.Items.Clear();
            textBox1.IsEnabled = true;
            button.IsEnabled = false;
            textBox.IsEnabled = false;
            disconnect.IsEnabled = false;
            dispatcherTimer.Stop();
            //System.Environment.Exit(1);
        }
    }
}
