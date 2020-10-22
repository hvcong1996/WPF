using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Common.Utilities;

namespace Common
{
    [ServiceContract]
    public interface IUpdateCtrlCallback
    {
        // Interface IUpdateCtrlCallback có chức năng DisplayUpdateResult/DisplayUpdateSettingResult
        // Service gửi lên cho UI thực hiện

        [OperationContract(IsOneWay = true)]
        void DisplayUpdateResult(int updateResult);

        [OperationContract(IsOneWay = true)]
        void DisplayUpdateSettingResult(int updateSettingResult);
    }
}
