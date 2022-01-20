using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wfc_chat
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceChat" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<ServerUser> users = new List<ServerUser>();
        private int nextID = 1;

        public int Сonncect(string name)
        {
            ServerUser user = new ServerUser()
            {
                Name = name,
                ID = nextID++,
                OperationContext = OperationContext.Current
            };

            SendMsg(user.Name + " подключился к чат.", 0);
            users.Add(user);

            return user.ID;
        }

        public void Disconncect(int id)
        {
            ServerUser user = users.FirstOrDefault(x => x.ID == id);

            if (user != null)
            {
                users.Remove(user);
                SendMsg(user.Name + " покинул чат.", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach (ServerUser user in users)
            {
                string answer = DateTime.UtcNow.ToShortTimeString();

                ServerUser currentUser = users.FirstOrDefault(x => x.ID == id);
                if (user != null)
                    answer += ": " + currentUser.Name + " ";

                answer += msg;

                user.OperationContext.GetCallbackChannel<IServiceChatCallback>().MsgCallback(answer);
            }
        }
    }
}
