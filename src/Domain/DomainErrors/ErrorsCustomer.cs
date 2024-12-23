
using ErrorOr;
namespace Domain.DomainErrors;

public static partial class CustomErrors
{
    public static class Customer
    {
        public static Error PhoneNumberWithBabFormat => Error.Validation("Customer.PhoneNumber", "Phone  number is not valid format");
		public static Error AddressWithFormatInvalid => Error.Validation("Customer.Address", "Address is not valid");
	}
}
