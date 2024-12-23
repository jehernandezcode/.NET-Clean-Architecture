
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record PhoneNumber
    {
        private const int MaxLenght = 15;
        private const string Pattern = @"^\+\d{1,4}\s\d{10}$";
        public string Value { get; init; }


        public PhoneNumber(string value) =>  Value = value;

        [GeneratedRegex(Pattern)]
        private static partial Regex PhoneNumberRegex();

        public static PhoneNumber? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length >= MaxLenght)
            {
                return null;
            }

            return new PhoneNumber(value);
        }
    }
}
