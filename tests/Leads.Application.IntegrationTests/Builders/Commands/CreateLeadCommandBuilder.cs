using Leads.Application.LeadsHandlers.Commands.CreateLead;

namespace Leads.Application.IntegrationTests.Builders.Commands;
public class CreateLeadCommandBuilder
{
    private readonly CreateLeadCommand _command = new()
    {
        Name = "name",
        PINCode = "234",
        Address = "asdsdf",
        EmailAddress = "m@gmail.com",
        MobileNumber = "24234",
    };

    public CreateLeadCommand Build()
    {
        return _command;
    }

    public CreateLeadCommandBuilder WithPINCode(string pinCode)
    {
        _command.PINCode = pinCode;
        return this;
    }
}