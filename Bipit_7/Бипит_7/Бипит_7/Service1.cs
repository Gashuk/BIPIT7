using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Бипит_7
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        List<string> messag = new List<string>();
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };

            nextId++;
            SendMsg(" " + user.Name + " подключился к чату.", 0);
            users.Add(user);

            //foreach (var item in users)
            //{
            //    var usefffr = users.FirstOrDefault(i => i.ID == 0);

            //    if (usefffr != null)
            //    {
            //        item.operationContext.GetCallbackChannel<IServerChatCallBack>().MsgCallBack();
            //    }
            //}

            return user.ID; 
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if(user != null)
            {
                users.Remove(user);
                SendMsg(" " + user.Name + " покинул чат.", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            bool flag = true;
            foreach(var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);

                if (user != null)
                {
                    answer += ": " + user.Name+ ": ";
                }

                answer += msg;
                item.operationContext.GetCallbackChannel<IServerChatCallBack>().MsgCallBack(answer);
                if (flag)
                {
                    messag.Add(answer);
                    flag = false;
                }
                
            }
        }

        public List<string> ListB()
        {
            return messag;
        }
    }
}
