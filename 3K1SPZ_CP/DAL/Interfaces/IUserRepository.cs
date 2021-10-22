﻿namespace _3K1SPZ_CP;

public interface IUserRepository
{
    public bool UpdateDispName(string login, string newDispName);
    public bool UpdatePassword(string login, string newPassword);
    public UserDTO Get(string login);
    public UserDTO Get(int id);
}