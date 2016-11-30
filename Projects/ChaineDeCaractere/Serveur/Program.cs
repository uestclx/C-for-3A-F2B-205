using RemotingInterface;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace remotServeur
{
	/// <summary>
	/// Description résumée de demarreServeur.
	/// </summary>
		[Serializable]
    public  class ChainePartagee
    {
        public static string hello = "chaine de depart";
    }


    public class Serveur  : MarshalByRefObject, RemotingInterface.IRemotChaine  
    {
        private static Data data;

        static void Main()
		{
            data = new Data();
            // Création d'un nouveau canal pour le transfert des données via un port 
            TcpChannel canal = new TcpChannel(2333);

			// Le canal ainsi défini doit être Enregistré dans l'annuaire
			ChannelServices.RegisterChannel(canal, false);

			// Démarrage du serveur en écoute sur objet en mode Singleton
			// Publication du type avec l'URI et son mode 
			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(Serveur), "Serveur",  WellKnownObjectMode.Singleton);

			Console.WriteLine("Le serveur est bien démarré");
			// pour garder la main sur la console
			ChainePartagee.hello = Console.ReadLine();
		}

		// Pour laisser le serveur fonctionner sans time out
		public override object  InitializeLifetimeService()
		{
			return null;
		}


        #region Membres de IRemotChaine

        public bool UserExsited(string name)
        {
            List<string> l = data.getUsers();
            foreach (string n in l)
            {
                if (n.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Login(string name)
        {
            bool loginOk = UserExsited(name);
            if (UserExsited(name))
            {
                return false;
            } else
            {
                data.addUser(name);
                return true;
            }
        }

        public Data SendMessage(string name,string message)
        {
            data.addMessage(name, message);
            return data;
        }

        public Data SyncMessage()
        {
            return data;
        }

        public bool Disconnect(string name)
        {
            if (UserExsited(name))
            {
                data.users.Remove(name);
                return true;
            }
            else {
                return false;
            }
        }

        #endregion
    }
}
