using ChatApp.Contacts.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Contacts.Contact
{
    public interface IMessageServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void FowardToClient(Message message);

        [OperationContract(IsOneWay = true)]
        void UserConnected(ObservableCollection<User> users);
    }
}
