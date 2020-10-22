using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Common.Utilities;

namespace GUI
{
    public class UpdateCommunicate : IUpdateCtrlCallback, IDisposable
    {
        private ChannelFactory<IUpdateCtrl> m_ChannelFactory;

        public IUpdateCtrl m_UpdateCtrlChannel;

        string endPoint = Utilities.localHost + "/" + Utilities.serviceName + "/" + Utilities.updateName;

        public event Action<int> DisplayUpdateResultEvent;

        public event Action<int> DisplayUpdateSettingResultEvent;

        public UpdateCommunicate()
        {
            Utilities.GUILog($"UpdateCommunicate Start");

            ConnectServiceChanel();
            if (m_UpdateCtrlChannel != null)
            {
                m_UpdateCtrlChannel.CreateCallBack();
            }
            Utilities.GUILog($"UpdateCommunicate End");
        }

        public void ConnectServiceChanel()
        {
            try
            {
                Utilities.GUILog($"ConnectServiceChanel Start");
                if (m_ChannelFactory == null)
                {
                    // Create Pipe for callback
                    IUpdateCtrlCallback callback = this;
                    InstanceContext context = new InstanceContext(callback);
                    NetNamedPipeBinding binding = new NetNamedPipeBinding()
                    {
                        SendTimeout = TimeSpan.MaxValue
                    };
                    EndpointAddress endPointAddress = new EndpointAddress(new Uri(endPoint));

                    m_ChannelFactory = new DuplexChannelFactory<IUpdateCtrl>(context, binding, endPointAddress);
                }

                // Create channel to connect service
                if (m_ChannelFactory.State != CommunicationState.Opening && m_ChannelFactory.State != CommunicationState.Opened)
                {
                    m_ChannelFactory.Open();
                    m_UpdateCtrlChannel = m_ChannelFactory.CreateChannel();
                }
                Utilities.GUILog($"ConnectServiceChanel End");
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"ConnectServiceChanel Exception: {ex.ToString()}");
            }
        }

        public void ExecuteUpdate()
        {
            m_UpdateCtrlChannel.ExecuteUpdate();
        }

        public void ExecuteUpdateSetting(string updateSettingPath)
        {
            m_UpdateCtrlChannel.ExecuteUpdateSetting(updateSettingPath);
        }

        public void DisplayUpdateResult(int updateResult)
        {
            try
            {
                Utilities.GUILog($"Start DisplayUpdateStatus");
                if (DisplayUpdateResultEvent != null)
                {
                    foreach (var item in DisplayUpdateResultEvent.GetInvocationList())
                    {
                        var action = (Action<int>)item;
                        action.Invoke(updateResult);
                    }
                }
                Utilities.GUILog($"End DisplayUpdateStatus");
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"Exception DisplayUpdateStatus : {ex.ToString()}");
            }
        }

        public void DisplayUpdateSettingResult(int updateSettingResult)
        {
            try
            {
                Utilities.GUILog($"Start DisplayUpdateStatus");
                if (DisplayUpdateSettingResultEvent != null)
                {
                    foreach (var item in DisplayUpdateSettingResultEvent.GetInvocationList())
                    {
                        var action = (Action<int>)item;
                        action.Invoke(updateSettingResult);
                    }
                }
                Utilities.GUILog($"End DisplayUpdateStatus");
            }
            catch (Exception ex)
            {
                Utilities.GUILog($"Exception DisplayUpdateStatus : {ex.ToString()}");
            }
        }

        public void Dispose()
        {
            try
            {
                Utilities.GUILog($"Dispose Start");
                DisconnectServiceChanel();
                Utilities.GUILog($"Dispose End");
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
                Utilities.GUILog($"DisconnectServiceChanel Start");
                m_UpdateCtrlChannel = null;
                m_ChannelFactory.Close();
                Utilities.GUILog($"DisconnectServiceChanel End");
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
;