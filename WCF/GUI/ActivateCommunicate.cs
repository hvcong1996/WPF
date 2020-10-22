using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ActivateCommunicate : IDisposable
    {
        private ChannelFactory<IActivateCtrl> m_ChannelFactory;

        public IActivateCtrl m_ActivateCtrlChannel;

        string endPoint = Utilities.localHost + "/" + Utilities.serviceName + "/" + Utilities.activeName;

        public ActivateCommunicate()
        {
            ConnectServiceChanel();
        }

        public void ConnectServiceChanel()
        {
            try
            {
                Utilities.GUILog($"ConnectServiceChanel");
                if (m_ChannelFactory == null)
                {
                    // Create Pipe for callback
                    NetNamedPipeBinding binding = new NetNamedPipeBinding()
                    {
                        SendTimeout = TimeSpan.MaxValue
                    };
                    EndpointAddress endPointAddress = new EndpointAddress(new Uri(endPoint));

                    m_ChannelFactory = new ChannelFactory<IActivateCtrl>(binding, endPointAddress);
                }

                // Create channel to connect service
                if (m_ChannelFactory.State != CommunicationState.Opening && m_ChannelFactory.State != CommunicationState.Opened)
                {
                    m_ChannelFactory.Open();
                    m_ActivateCtrlChannel = m_ChannelFactory.CreateChannel();
                }
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"ConnectServiceChanel Exception: {ex.ToString()}");
            }
        }

        public int Activation(string activateInfo)
        {
            int rtn = 0;

            try
            {
                Utilities.GUILog($"Activation");
                rtn = m_ActivateCtrlChannel.Activation(activateInfo);
            }
            catch (Exception ex)
            {
                rtn = 1;
                Utilities.GUILog($"ConnectServiceChanel Exception: {ex.ToString()}");
            }

            return rtn;
        }

        public int Deactivation()
        {
            int rtn = 0;

            try
            {
                Utilities.GUILog($"Deactivation");
                rtn = m_ActivateCtrlChannel.Deactivation();
            }
            catch (Exception ex)
            {
                rtn = 1;
                Utilities.GUILog($"ConnectServiceChanel Exception: {ex.ToString()}");
            }

            return rtn;
        }

        public void Dispose()
        {
            try
            {
                Utilities.GUILog($"Dispose");
                DisconnectServiceChanel();
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"Dispose Exception: {ex.ToString()}");
            }
            finally
            {
                ((IDisposable)m_ChannelFactory)?.Dispose();
            }
        }
        private void DisconnectServiceChanel()
        {
            try
            {
                Utilities.GUILog($"DisconnectServiceChanel");
                m_ActivateCtrlChannel = null;
                m_ChannelFactory.Close();
            }
            catch (Exception ex)
            {
                m_ChannelFactory.Abort();
                Utilities.GUILog($"DisconnectServiceChanel Exception: {ex.ToString()}");
            }
            finally
            {
                m_ChannelFactory = null;
            }
        }
    }
}
