using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComandSenderManager
{
    public  interface IComandSender
    {
        Task<Session> CreateSession(string plcIP);
        Task<string> SendComandBool(Session session, string plcNodeId, bool value);
        Task SendComandString(Session session, string plcNodeId, string value);
        Task<string> SendComandFloat(Session session, string plcNodeId, float value);
        Task<string> CheckRemoteControlStatus(Session session, string hRemote);

      
    }
}
