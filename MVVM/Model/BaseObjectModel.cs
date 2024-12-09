using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Project_WPF.MVVM.Model
{
    [DataContract]
    public class BaseObjectModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DataMember]
        public string? Id { get; set; }

        [DataMember]
        public string? UserId { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; init; } = DateTime.Now;

        [DataMember]
        public DateTime LastUpdatedOn { get; set; } = DateTime.Now;

        [DataMember]
        public bool IsDeleted {  get; set; }

        protected void OnUpdate()
        {
            LastUpdatedOn = DateTime.Now;
        }

        public BaseObjectModel(){} // XML HANDLER
    }
}
