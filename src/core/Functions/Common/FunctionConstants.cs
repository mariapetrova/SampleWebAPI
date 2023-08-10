namespace Functions.Common;
public static class FunctionConstants
{
    public const string HttpTriggerMethodPost = "post";
    public const string HttpTriggerMethodGet = "get";
    public const string HttpTriggerMethodPut = "put";
    public const string HttpTriggerMethodPatch = "patch";
    public const string SearchSubareasSummary = "Search subareas.";
    public const string SearchSubareasResponse = "Subareas data.";
    public const string SubareasParameterPINCodeSummary = "PinCode of a subarea.";
    public const string GetLeadSummary = "Get lead.";
    public const string LeadParameterLeadSummary = "Lead identifier.";
    public const string GetLeadResponse = "Lead data";
    public const string UpdateLeadSummary = "Updates lead.";
    public const string UpdateLeadParameterSummary = "Id of lead that needs to be updated.";
    public const string CreateLeadSummary = "Creates a new lead.";
    public const string LeadWithInvalidId = "Invalid Id supplied";
    public const string UploadLeadSummary = "Uploads a lead.";
    public const string DownloadLeadResponse = "Download lead from storage";
}