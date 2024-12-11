
using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class CustomerRepositoryImp : ICustomerRepository
    {

        private readonly ApplicationDbContext _context;

        public CustomerRepositoryImp(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Customer customer) => _context.Customers.AddAsync(customer);

        public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(customer => customer.Id == id);   
    }
}
