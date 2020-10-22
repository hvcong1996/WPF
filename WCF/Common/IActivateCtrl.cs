using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IActivateCtrl
    {
        // Interface IActivateCtrl có chức năng Activation/Deactivation
        // UI gửi xuống cho Service thực hiện

        [OperationContract] // Default (IsOneWay = false), Áp dụng cho kiểu trả về giá trị
        int Activation(string activateInfo);

        [OperationContract] // Default (IsOneWay = false), Áp dụng cho kiểu trả về giá trị
        int Deactivation();
    }
}
