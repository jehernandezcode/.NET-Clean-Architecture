
using Application.Customers.Create;
using Domain.Customers;
using Domain.DomainErrors;
using Domain.Primitives;
using ErrorOr;
using FluentAssertions;
using Moq;

namespace Application.Customers.UnitTests.Create;

public class CreateCustomerCommandHandlerUnitTests
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCustomerCommandHandler _handler;
    public CreateCustomerCommandHandlerUnitTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        CreateCustomerCommand command = new CreateCustomerCommand("JHonn","Vega", "jhonnn@sdfsdf.com", "+57 12345678901", "col", "line 1","line 2","Paez", "Boyaca","15003");
        var result = await _handler.Handle(command, default);
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(CustomErrors.Customer.PhoneNumberWithBabFormat.Code);
        result.FirstError.Description.Should().Be(CustomErrors.Customer.PhoneNumberWithBabFormat.Description);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenAddressHasBadFormat_ShouldReturnValidationError()
    {
        CreateCustomerCommand command = new CreateCustomerCommand("JHonn", "Vega", "jhonnn@sdfsdf.com", "+57 1234567890", "col", "", "line 2", "Paez", "Boyaca", "15003");
        var result = await _handler.Handle(command, default);
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(CustomErrors.Customer.AddressWithFormatInvalid.Code);
        result.FirstError.Description.Should().Be(CustomErrors.Customer.AddressWithFormatInvalid.Description);
    }
}
