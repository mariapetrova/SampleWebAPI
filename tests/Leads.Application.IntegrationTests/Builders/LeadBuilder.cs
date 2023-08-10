using Domain.Entities;
namespace Leads.Application.IntegrationTests.Builders;
public class LeadBuilder
{
    private readonly Lead _lead = new()
    {
        Name = "Test",
        Address = "asdsdf",
        EmailAddress = "m@gmail.com",
        MobileNumber = "24234",
    };

    public Lead Build()
    {
        return _lead;
    }

    public LeadBuilder WithId(int id)
    {
        _lead.Id = id;
        return this;
    }

    public LeadBuilder WithPINCode(string pinCode)
    {
        _lead.PINCode = pinCode;
        return this;
    }
}