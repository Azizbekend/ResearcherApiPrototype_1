using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;


namespace ComandSenderManager
{
    public class ComandSender : IComandSender
    {
        private ApplicationConfiguration _config = new ApplicationConfiguration() 
        {
            ApplicationName = "OpcCommandSender",
            ApplicationUri = Utils.Format(@"urn:{0}:OpcConnectionTest", System.Net.Dns.GetHostName()),
            ApplicationType = ApplicationType.Client,
            SecurityConfiguration = new SecurityConfiguration()
            {
                ApplicationCertificate = new CertificateIdentifier { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPCFoundation\CertificateStores\CertIndifier" },
                TrustedIssuerCertificates = new CertificateTrustList() { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPCFoundation\CertificateStores\trustedIssuer" },
                TrustedPeerCertificates = new CertificateTrustList() { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPCFoundation\CertificateStores\trustedPeer" },
                RejectedCertificateStore = new CertificateTrustList() { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPCFoundation\CertificateStores\rejectStore" },
                AutoAcceptUntrustedCertificates = true
            },
            TransportConfigurations = new TransportConfigurationCollection(),
            TransportQuotas = new TransportQuotas() { OperationTimeout = 15000 },
            ClientConfiguration = new ClientConfiguration() { DefaultSessionTimeout = 60000 },
            TraceConfiguration = new TraceConfiguration()
        };

        public async Task<Session> CreateSession(string opcIp)
        {
            await _config.ValidateAsync(ApplicationType.Client);
            if (_config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                _config.CertificateValidator.CertificateValidation += (s, e) => { e.Accept = (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted); };
            }
            var application = new ApplicationInstance(_config);
            var identity = new UserIdentity();
            var endpoint = CoreClientUtils.SelectEndpoint(_config, opcIp, useSecurity:false);
            return Session.Create(_config, new ConfiguredEndpoint(null, endpoint, EndpointConfiguration.Create(_config)), false, "", 60000, identity, null).GetAwaiter().GetResult();
        }

        public async Task SendComandString(Session session, string plcNodeId, string value)
        {
            var writeValue = new WriteValue()
            {
                NodeId = plcNodeId,
                AttributeId = Attributes.Value,
                Value = new DataValue(new Variant(value))

            };
            var valueCollection = new WriteValueCollection { writeValue };
            await session.WriteAsync(null, valueCollection, CancellationToken.None);
            session.Dispose();
        }

        public async Task<string> SendComandBool(Session session, string plcNodeId, bool value)
        {
            var writeValue = new WriteValue()
            {
                NodeId = plcNodeId,
                AttributeId = Attributes.Value,
                Value = new DataValue(new Variant(value))

            };
            var valueCollection = new WriteValueCollection { writeValue };
            await session.WriteAsync(null, valueCollection, CancellationToken.None);
            var nodeValue = await session.ReadValueAsync(plcNodeId);
            session.Dispose();
            return nodeValue.Value.ToString();
            
        }

        public async Task<string> SendComandFloat(Session session, string plcNodeId, float value)
        {
            var writeValue = new WriteValue()
            {
                NodeId = plcNodeId,
                AttributeId = Attributes.Value,
                Value = new DataValue(new Variant(value))

            };
            var valueCollection = new WriteValueCollection { writeValue };
            await session.WriteAsync(null, valueCollection, CancellationToken.None);
            var nodeValue = await session.ReadValueAsync(plcNodeId);
            session.Dispose();
            return nodeValue.Value.ToString();
        }
    }
}
