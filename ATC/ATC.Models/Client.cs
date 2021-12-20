using ATC.Abstractions;

namespace ATC.Models
{
    public class Client : IClient
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format($"{FirstName} {LastName}");
        }
    }
}
