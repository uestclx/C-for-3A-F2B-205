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
    public  class ChainePartagee{

        
            public static string hello = "chaine de depart";
            public static Data data = new Data(); 
        }


    public class Serveur  : MarshalByRefObject, RemotingInterface.IRemotChaine  
    {
        
        static void Main()
		{
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

		public string Hello()
		{
			// TODO : ajoutez l'implémentation de Serveur.Hello
			return  ChainePartagee.hello;
		}

        public Data Login(string name)
        {
            ChainePartagee.data.error = false;
            List<string> l = ChainePartagee.data.getUsers();
            foreach (string n in l)
            {
                if (n.Equals(name))
                {
                    ChainePartagee.data.error = true;
                    return ChainePartagee.data;
                }
            }
            ChainePartagee.data.addUser(name);
            return ChainePartagee.data;
        }
        public Data SendMessage(string name,string message)
        {
            ChainePartagee.data.addMessage(name, message);
            return ChainePartagee.data;
        }

        public Data Fresh()
        {
            return ChainePartagee.data;
        }

        public void Disconnect(string name)
        {
            ChainePartagee.data.users.Remove(name);
        }

        #endregion
    }
}
