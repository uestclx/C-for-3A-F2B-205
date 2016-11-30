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
        private bool loginOk = false;

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
            if (loginOk)
            {
                string name = unameIn.Text;
                string message = messageIn.Text;
                data = LeRemot.SendMessage(name, message);
                userListBox.Items.Clear();
                msgListBox.Items.Clear();
                messages = data.getMessages();
                users = data.getUsers();
                foreach (string n in messages)
                {
                    msgListBox.Items.Add(n);
                }

                foreach (string n in users)
                {
                    userListBox.Items.Add(n);
                }
                messageIn.Clear();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string name = unameIn.Text;

            if(LeRemot.Login(name) == false)
            {
                MessageBox.Show("Error login,please choose another user name and try again");
                return;
            }
            loginOk = true;
            data = LeRemot.SyncMessage();
            users = data.getUsers();
            userListBox.Items.Clear();
            foreach (string n in users)
            {
                userListBox.Items.Add(n);
            }

            if (loginOk) {
                loginBotton.IsEnabled = false;
                unameIn.IsEnabled = false;
                sendButton.IsEnabled = true;
                messageIn.IsEnabled = true;
                disconnect.IsEnabled = true;
                dispatcherTimer.Start();
            }
        }

        public void checkout(object sender, EventArgs e)
        {
            if (loginOk) {
                data = LeRemot.SyncMessage();
                msgListBox.Items.Clear();
                userListBox.Items.Clear();
                messages = data.getMessages();
                users = data.getUsers();
                foreach (string n in messages)
                {
                    msgListBox.Items.Add(n);
                }

                foreach (string n in users)
                {
                    userListBox.Items.Add(n);
                }
            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string name = unameIn.Text;
            if (LeRemot.Disconnect(name))
            {
                loginOk = false;
                userListBox.Items.Clear();
                msgListBox.Items.Clear();
                loginBotton.IsEnabled = true;
                disconnect.IsEnabled = false;
                unameIn.IsEnabled = true;
                sendButton.IsEnabled = false;
                messageIn.IsEnabled = false;
                dispatcherTimer.Stop();
            }
            else {
                MessageBox.Show("log out failed. user is not connected.");
            }
            
            //System.Environment.Exit(1);
        }
    }
}
