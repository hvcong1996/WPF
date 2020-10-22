using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public partial class Service1 : ServiceBase
    {
        ServiceCommunicate serviceCommunicate = null;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Utilities.ServiceLog("OnStart Start");

            if (serviceCommunicate == null)
            {
                // Init service Communication
                serviceCommunicate = new ServiceCommunicate();
            }

            serviceCommunicate.ActivateEvent += ServiceActivateProcessing;
            serviceCommunicate.DeactivateEvent += ServiceDeactivateProcessing;
            serviceCommunicate.UpdateEvent += ServiceUpdateProcessing;
            serviceCommunicate.UpdateSettingEvent += ServiceUpdateSettingProcessing;

            Utilities.ServiceLog("OnStart End");
        }

        private int ServiceActivateProcessing(string activateInfo)
        {
            int rtn = 0;
            try
            {
                Utilities.ServiceLog("Service ServiceActivateProcessing");
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ServiceActivateProcessing Exception: {ex.ToString()}");
            }
            return rtn;
        }

        private int ServiceDeactivateProcessing()
        {
            int rtn = 0;
            try
            {
                Utilities.ServiceLog("Service ServiceDeactivateProcessing");
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ServiceDeactivateProcessing Exception: {ex.ToString()}");
            }
            return rtn;
        }

        private void ServiceUpdateProcessing()
        {
            try
            {
                Utilities.ServiceLog("ServiceUpdateProcessing");

                Utilities.ServiceLog("Service CallBack DisplayUpdateResult");
                int updateResult = 1;
                serviceCommunicate.updateCtrlCallback.DisplayUpdateResult(updateResult);
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ServiceUpdateProcessing Exception: {ex.ToString()}");
            }
        }

        private void ServiceUpdateSettingProcessing(string filePath)
        {
            try
            {
                Utilities.ServiceLog($"ServiceUpdateSettingProcessing filePath :{filePath}");


                Utilities.ServiceLog("Service CallBack DisplayUpdateSettingResult");
                int updateSettingResult = 1;
                serviceCommunicate.updateCtrlCallback.DisplayUpdateSettingResult(updateSettingResult);
            }
            catch (Exception ex)
            {
                Utilities.ServiceLog($"ServiceUpdateSettingProcessing Exception: {ex.ToString()}");
            }
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            //Utilities.ServiceLog($"OnSessionChange Start: {changeDescription.Reason}");


            base.OnSessionChange(changeDescription);
            //Utilities.ServiceLog($"OnSessionChange End {changeDescription.Reason}");
        }

        protected override void OnStop()
        {
            Utilities.ServiceLog("Service OnStop");
        }
    }
}
