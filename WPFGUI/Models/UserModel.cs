using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUI.Helpers
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Login = userDTO.Login;
            Password = userDTO.Password;
            DispName = userDTO.DispName;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }    

        public string ClearPassword { get; set; }
        public string DispName { get; set; }


    }
}
