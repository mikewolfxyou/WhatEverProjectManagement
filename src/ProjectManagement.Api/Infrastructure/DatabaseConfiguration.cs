namespace ProjectManagement.Api.Infrastructure
{
    public class DatabaseConfiguration
    {
        public string Server { get; }

        public string Username { get; }

        public string Password { get; }

        public DatabaseConfiguration(string server, string username, string password)
        {
            Server = server;
            Username = username;
            Password = password;
        }
    }
}