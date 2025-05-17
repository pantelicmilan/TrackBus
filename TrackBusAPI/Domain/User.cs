using Domain.Primitives;

namespace Domain;

public class User : AggregateRoot
{
    protected User() { }
    public User(string username, string password) 
    {
        Username = username;
        Password = password;
    }

    public string Username { get; protected set; }
    public string Password { get; protected set; }
}
