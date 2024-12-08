
namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(CustomerId id);
        Task<Customer> Add(Customer customer);
    }
}
