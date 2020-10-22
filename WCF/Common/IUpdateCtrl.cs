using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract(CallbackContract = typeof(IUpdateCtrlCallback), SessionMode = SessionMode.Required)]
    public interface IUpdateCtrl
    {
        // Interface IUpdateCtrl có 2 chức năng ExecuteUpdate/ExecuteUpdateSetting
        // UI gửi xuống cho Service thực hiện
        // CreateCallBack để có thể thực hiện callback ngược từ Service lên UI nhằm trả về kết quả

        [OperationContract(IsOneWay = true)] // Sử dụng (IsOneWay = true) cho kiểu void(không return)
        void ExecuteUpdate();

        [OperationContract(IsOneWay = true)] // Sử dụng (IsOneWay = true) cho kiểu void(không return)
        void ExecuteUpdateSetting(string filePath);

        [OperationContract(IsOneWay = true)] // Sử dụng (IsOneWay = true) cho kiểu void(không return)
        void CreateCallBack();
    }
}
