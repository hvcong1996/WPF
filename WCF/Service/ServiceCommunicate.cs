using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceCommunicate : IUpdateCtrl, IActivateCtrl, IDisposable
    {
        ServiceHost m_WCFHost;

        public IUpdateCtrlCallback updateCtrlCallback = null;

        // TODO - Test using Func event
        //public event Func<string, int> ActivateEvent;
        //public event Func<int> DeactivateEvent;

        // Active delegate
        public delegate int ActivateEventHandler(string activateInfo);
        // Event của xử lý Activate từ UI
        public event ActivateEventHandler ActivateEvent;

        // Deactive delegate
        public delegate int DeactivateEventHandler();
        // Event của xử lý Deactivate từ UI
        public event DeactivateEventHandler DeactivateEvent;

        public event Action UpdateEvent;
        public event Action<string> UpdateSettingEvent;

        public ServiceCommunicate()
        {
            StartServiceHost();
        }

        private void StartServiceHost()
        {
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate StartServiceHost");

                if (m_WCFHost == null)
                {
                    string serviceEndPoint = Utilities.localHost + "/" + Utilities.serviceName + "/";
                    string activateEndPoint = serviceEndPoint + Utilities.activeName;
                    string updateEndPoint = serviceEndPoint + Utilities.updateName;

                    Utilities.ServiceLog($"serviceEndPoint {serviceEndPoint}");
                    Utilities.ServiceLog($"activateEndPoint {activateEndPoint}");
                    Utilities.ServiceLog($"updateEndPoint {updateEndPoint}");

                    m_WCFHost = new ServiceHost(this, new Uri(serviceEndPoint));

                    Utilities.ServiceLog($"new ServiceHost");

                    var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport)
                    {
                        SendTimeout = TimeSpan.MaxValue,
                        ReceiveTimeout = TimeSpan.MaxValue
                    };

                    Utilities.ServiceLog($"new NetNamedPipeBinding");

                    m_WCFHost.AddServiceEndpoint(typeof(IActivateCtrl), binding, new Uri(activateEndPoint));

                    Utilities.ServiceLog($"AddServiceEndpoint IActivateCtrl");

                    m_WCFHost.AddServiceEndpoint(typeof(IUpdateCtrl), binding, new Uri(updateEndPoint));

                    Utilities.ServiceLog($"AddServiceEndpoint IUpdateCtrl");
                    m_WCFHost.Open();
                }

            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"StartServiceHost Exception: {ex.ToString()}");
            }
        }

        public void ExecuteUpdate()
        {
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate ExecuteUpdate");

                if (UpdateEvent != null)
                {
                    foreach (var item in UpdateEvent.GetInvocationList())
                    {
                        var act = (Action)item;
                        act.Invoke();
                    }
                }

            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ExecuteUpdate Exception: {ex.ToString()}");
            }
        }

        public void ExecuteUpdateSetting(string filePath)
        {
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate ExecuteUpdateSetting");

                if (UpdateSettingEvent != null)
                {
                    foreach (var item in UpdateSettingEvent.GetInvocationList())
                    {
                        var act = (Action<string>)item;
                        act.Invoke(filePath);
                    }
                }

            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ExecuteUpdateSetting Exception: {ex.ToString()}");
            }
        }

        public void ProcessingToDisplayUpdateResult(int updateResult)
        {
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate ProcessingToDisplayUpdateResult");

                if (updateCtrlCallback == null)
                {
                    updateCtrlCallback = OperationContext.Current.GetCallbackChannel<IUpdateCtrlCallback>();
                }
                updateCtrlCallback.DisplayUpdateResult(updateResult);

            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ProcessingToDisplayUpdateResult Exception: {ex.ToString()}");
            }
        }

        public void ProcessingToDisplayUpdateSettingResult(int updateResult)
        {
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate ProcessingToDisplayUpdateSettingResult");

                if (updateCtrlCallback == null)
                {
                    updateCtrlCallback = OperationContext.Current.GetCallbackChannel<IUpdateCtrlCallback>();
                }
                updateCtrlCallback.DisplayUpdateSettingResult(updateResult);
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ProcessingToDisplayUpdateSettingResult Exception: {ex.ToString()}");
            }
        }

        public void CreateCallBack()
        {
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate CreateCallBack");

                updateCtrlCallback = OperationContext.Current.GetCallbackChannel<IUpdateCtrlCallback>();
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"CreateCallBack Exception: {ex.ToString()}");
            }
        }

        public int Activation(string activateInfo)
        {
            int rtn = 0;
            try
            {
                Utilities.ServiceLog($"ServiceCommunicate Activation");

                // TODO - Test using Func event
                //if (ActivateEvent != null)
                //{
                //    foreach (var item in ActivateEvent.GetInvocationList())
                //    {
                //        var act = (ActivateEvent)item;
                //        rtn = act.Invoke(activateInfo);
                //    }
                //}

                if (ActivateEvent != null)
                {
                    foreach (var item in ActivateEvent.GetInvocationList())
                    {
                        var act = (ActivateEventHandler)item;
                        rtn = act.Invoke(activateInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                rtn = 1;
                Utilities.ServiceLog($"Activation Exception: {ex.ToString()}");
            }

            return rtn;
        }

        public int Deactivation()
        {
            int rtn = 0;
            try
            {
                // TODO - Test using Func event
                //if (DeactivateEvent != null)
                //{
                //    foreach (var item in DeactivateEvent.GetInvocationList())
                //    {
                //        var act = (DeactivateEvent)item;
                //        rtn = act.Invoke();
                //    }
                //}
                Utilities.ServiceLog($"ServiceCommunicate Deactivation");

                if (DeactivateEvent != null)
                {
                    foreach (var item in DeactivateEvent.GetInvocationList())
                    {
                        var act = (DeactivateEventHandler)item;
                        rtn = act.Invoke();
                    }
                }

            }
            catch (Exception ex)
            {
                rtn = 1;
                Utilities.ServiceLog($"Deactivation Exception: {ex.ToString()}");
            }

            return rtn;
        }

        public void Dispose()
        {
            try
            {
                Dispose(true);
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"Dispose Exception: {ex.ToString()}");
            }
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        m_WCFHost.Close();
                    }
                    catch (Exception ex)
                    {
                        m_WCFHost.Abort();
                        Utilities.ServiceLog($"Dispose(bool) Exception: {ex.ToString()}");
                    }
                }

                disposedValue = true;
            }
        }
    }
}
