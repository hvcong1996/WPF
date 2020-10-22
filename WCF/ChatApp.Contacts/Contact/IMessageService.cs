using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ChatApp.Contacts.Domain;
using System.Collections.ObjectModel;

namespace ChatApp.Contacts.Contact
{
    [ServiceContract(CallbackContract = typeof(IMessageServiceCallBack), SessionMode = SessionMode.Required)]
    // Need add reference System.ServiceModel to can using
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(User user);

        [OperationContract(IsOneWay = true)]
        void SendMessage(Message message);

        [OperationContract(IsOneWay = true)]
        ObservableCollection<User> GetConnectedUsers();
    }
}
