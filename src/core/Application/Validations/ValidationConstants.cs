namespace Application.Validations;
public static class ValidationConstants
{
    public const string SubareaNotFound = "Subarea not found.";

    public const string LeadNotFound = "Lead not found.";

    public const string EmailAddressRegex = @"^[A-Za-z0-9][A-Za-z0-9._%+-]*@(?:(?=[A-Za-z0-9-]{2,63}\.)[A-Za-z0-9]+(?:-[A-Za-z0-9]+)*\.){1,8}[A-Za-z]{2,63}$";

    public const string MobileNumberRegex = @"^(?=(?:[^\-\.\ ]*[\-\.\ ]){0,10}[^\-\.\ ]*$)([+][1-9][\d\-\.\ ]?|[(][\d]{1,3}[)][\-\.\ ]?)?([\d]([\-\.\ ])?){0,25}(?<![\-\.\ ])$";

    public const ushort MaximumNameLength = 125;
    public const ushort MaximumPINCodeLength = 25;
    public const ushort MinimumAddressLength = 500;
    public const ushort MaximumEmailLength = 255;
    public const ushort MaximumMobileNumberLength = 25;
}