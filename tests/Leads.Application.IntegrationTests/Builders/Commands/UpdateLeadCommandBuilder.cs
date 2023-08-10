using Leads.Application.LeadsHandlers.Commands.UpdateLead;

namespace Leads.Application.IntegrationTests.Builders.Commands;
public class UpdateLeadCommandBuilder
{
    private readonly UpdateLeadCommand _command = new()
    {
        Name = "Updated name",
        Address = "asdsdf",
        EmailAddress = "m@gmail.com",
        MobileNumber = "24234",
    };

    public UpdateLeadCommand Build()
    {
        return _command;
    }


    public UpdateLeadCommandBuilder WithId(int id)
    {
        _command.Id = id;
        return this;
    }
    public UpdateLeadCommandBuilder WithPINCode(string pinCode)
    {
        _command.PINCode = pinCode;
        return this;
    }
}