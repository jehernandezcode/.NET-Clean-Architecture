
namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Task<Customer?> GetByIdAsync(CustomerId id);
    }
}
