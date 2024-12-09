using KCK_Project_WPF.MVVM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Model
{
    [DataContract]
    public class UserModel
    {
        [DataMember]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Unique id
        [DataMember]
        public string? Name { get; set; } // What is shown to _users
        [DataMember]
        public string? ProfilePicture { get; set; } // Picture Local

        [DataMember]
        public string? Email { get; set; } // Login
        [DataMember]
        public string? Password { get; set; } // Passwd

        [XmlIgnore]
        public UserType _Type { get; set; } = UserType.Anonymous; // type of user

        [XmlElement("Type")]
        public string TypeString
        {
            get => _Type.ToString();
            set
            {
                if (Enum.TryParse(value, out UserType result))
                {
                    _Type = result;
                }
                else
                {
                    _Type = UserType.Anonymous;
                }
            }
        }

        public UserModel(string id, string name, string ppfPic, string email, string passwd, UserType type) 
        { 
            this.Id = id;
            this.Name = name;
            this.ProfilePicture = ppfPic;
            this.Email = email;
            this.Password = passwd;
            this._Type = type;
        }

        public UserModel(string name, string ppfPic, string email, string passwd)
        {
            this.Name = name;
            this.ProfilePicture = ppfPic;
            this.Email = email;
            this.Password = passwd;
        }

        public UserModel(string name, string ppfPic, string email, string passwd, UserType type)
        {
            this.Name = name;
            this.ProfilePicture = ppfPic;
            this.Email = email;
            this.Password = passwd;
            this._Type = type;
        }

        public UserModel() { }

        public override string ToString()
        {
            return $"{Id.PadRight(40)} {Name.PadLeft(25)} {Email.PadRight(25)} {TypeString.PadRight(18)}";
        }
    }
}
