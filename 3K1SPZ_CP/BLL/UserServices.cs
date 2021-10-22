using _3K1SPZ_CP.DAL;

namespace _3K1SPZ_CP;

public class UserServices
{
    public IUserRepository userRepository { get; set; }
    public UserServices()
    {
        userRepository = new UserRepository(Helper.CnnVal());
    }
    public bool CheckPassword(string login, string password) => userRepository.Get(login)?.Password == password;
    public UserDTO Get(string login) => userRepository.Get(login);
    public UserDTO Get(int id) => userRepository.Get(id);
    public void UpdateDispName(string login, string newDispName) => userRepository.UpdateDispName(login, newDispName);
    public void UpdatePassword(string login, string newPassword) => userRepository.UpdatePassword(login, newPassword);
}