using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Contacts.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ObservableCollection<Message> UserMessage { get; set; }
    }
}
