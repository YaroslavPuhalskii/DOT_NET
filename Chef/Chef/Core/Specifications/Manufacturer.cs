using System;

namespace Chef.Core.Specifications
{
    public class Manufacturer
    {
        private string _name;

        private string _country;

        private string _address;

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name can't be empty or null!");
                }

                _name = value;
            }
        }

        public string Country
        {
            get => _country;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Country can't be empty or null!");
                }

                _country = value;
            }
        }

        public string Address
        {
            get => _address;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Address can't be empty or null!");
                }

                _address = value;
            }
        }

        public Manufacturer(string name, string country, string address)
        {
            Name = name;
            Country = country;
            Address = address;
        }
    }
}
