using System;
using System.Collections.Generic;

namespace RemotingInterface
{
    /// <summary>
    /// cette interface contiendra la déclaration de toutes les 
    /// méthodes de l'objet distribué
    /// </summary>
    [Serializable]
    public class Data
    {
        public List<string> users;
        public List<string> messages;

        public Data()
        {
            users = new List<string>();
            messages = new List<string>();
        }

        public List<string> getUsers()
        {
            return users;
        }

        public List<string> getMessages()
        {
            return messages;
        }

        public void addUser(string name)
        {
            users.Add(name);
        }

        public void addMessage(string name,string message)
        {
            messages.Add(name + ": " + message);
        }

    }
    public interface IRemotChaine
	{
        Data SyncMessage();
        Data SendMessage(string name,string message);
        bool Login(string Username);
        bool Disconnect(string name);
    }
}
