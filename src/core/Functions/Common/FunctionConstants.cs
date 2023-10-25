namespace Functions.Common;
public static class FunctionConstants
{
    public const string HttpTriggerMethodPost = "post";
    public const string HttpTriggerMethodGet = "get";
    public const string HttpTriggerMethodPut = "put";
    public const string HttpTriggerMethodPatch = "patch";
    public const string SearchDepartmentsSummary = "Search Departments.";
    public const string SearchDepartmentsResponse = "Departments data.";
    public const string DepartmentsParameterIdSummary = "Id of a Department.";
    public const string GetPersonSummary = "Get person.";
    public const string PersonParameterPersonSummary = "Person identifier.";
    public const string GetPersonResponse = "Person data";
    public const string UpdatePersonSummary = "Updates person.";
    public const string UpdatePersonParameterSummary = "Id of person that needs to be updated.";
    public const string CreatePersonSummary = "Creates a new person.";
    public const string PersonWithInvalidId = "Invalid Id supplied";
    public const string UploadPersonSummary = "Uploads a person.";
    public const string DownloadPersonResponse = "Download person from storage";
}