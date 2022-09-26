namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationQueries
{
    public static class EmployeeAggregateValidationSqlStatements
    {
        public const string IsUniqueEmployeeName =
        @"SELECT 
            COUNT(EmployeeId) 
        FROM HumanResources.Employees
        WHERE FirstName = @fname AND LastName = @lname";

        public const string IsUniqueEmployeeEmail =
        @"SELECT 
            COUNT(EmployeeId) 
        FROM HumanResources.Employees
        WHERE EmailAddress = @email";

        public const string IsUniqueEmployeeSSN =
        @"SELECT 
            COUNT(EmployeeId) 
        FROM HumanResources.Employees
        WHERE SSN = @ssn";
    }
}