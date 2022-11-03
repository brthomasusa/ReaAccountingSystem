namespace ReaAccountingSys.Infrastructure.Persistence.Dapper.HumanResources.Queries
{
    public static class EmployeeAggregateQueries
    {
        public const string SelectReadModel =
        @"SELECT 
            ee.EmployeeId, ee.SupervisorId, types.EmployeeTypeId, types.EmployeeTypeName, 
            CONCAT(supv.FirstName,' ',COALESCE(supv.MiddleInitial,''),' ',supv.LastName) as ManagerFullName,
            ee.LastName, ee.FirstName, ee.MiddleInitial, 
            CONCAT(ee.FirstName,' ',COALESCE(ee.MiddleInitial,''),' ',ee.LastName) as EmployeeFullName, 
            ee.SSN, ee.EmailAddress, ee.Telephone, ee.AddressLine1, ee.AddressLine2, ee.City, ee.StateCode, ee.Zipcode,                                
            ee.MaritalStatus, ee.Exemptions, ee.PayRate, ee.StartDate, ee.IsActive, ee.IsSupervisor, ee.CreatedDate, ee.LastModifiedDate                
        FROM HumanResources.Employees ee
        INNER JOIN
        (
            SELECT 
                EmployeeId, LastName, FirstName, MiddleInitial 
            FROM HumanResources.Employees supv
            WHERE IsSupervisor = 1
        ) supv ON ee.SupervisorId = supv.EmployeeId
        JOIN HumanResources.EmployeeTypes types ON ee.EmployeeTypeId = types.EmployeeTypeId";

        public const string SelectAllEmployeeListItems =
        @"SELECT 
            ee.EmployeeId,
            CONCAT(ee.FirstName,' ',COALESCE(ee.MiddleInitial,''),' ',ee.LastName) as EmployeeFullName,
            ee.Telephone, ee.IsActive, ee.IsSupervisor,
            CONCAT(supv.FirstName,' ',COALESCE(supv.MiddleInitial,''),' ',supv.LastName) as ManagerFullName ,
            ISNULL(cards.TimeCards, 0 ) AS TimeCards             
        FROM HumanResources.Employees ee
        LEFT JOIN
        (
            SELECT 
                EmployeeId, LastName, FirstName, MiddleInitial 
            FROM HumanResources.Employees supv
            WHERE IsSupervisor = 1
        ) supv ON ee.SupervisorId = supv.EmployeeId
        LEFT JOIN
        (
            SELECT 
                cards.EmployeeId, COUNT(cards.TimeCardId) AS TimeCards
            FROM HumanResources.TimeCards cards
            GROUP BY cards.EmployeeId
        ) cards ON ee.EmployeeId = cards.EmployeeId";

        public const string SelectEmployeeManagers =
        @"SELECT 
            ee.EmployeeId AS ManagerId,
            CONCAT(ee.FirstName,' ',COALESCE(ee.MiddleInitial,''),' ',ee.LastName) as ManagerFullName,
            types.EmployeeTypeId,
            types.EmployeeTypeName AS JobTitle,
            CASE
                WHEN types.EmployeeTypeName = 'Salesperson' THEN 'Sales'        
                WHEN types.EmployeeTypeName = 'Maintenance' THEN 'Maintenance'
                WHEN types.EmployeeTypeName = 'Materials Handler' THEN 'Warehouse'
                WHEN types.EmployeeTypeName = 'Purchasing Agent' THEN 'Purchasing'
                WHEN types.EmployeeTypeName = 'Accountant' THEN 'Accounting'
                WHEN types.EmployeeTypeName = 'Administrator' THEN 'Administrators'
            END AS [Group]               
        FROM HumanResources.Employees ee
        JOIN HumanResources.EmployeeTypes types ON ee.EmployeeTypeId = types.EmployeeTypeId
        WHERE ee.IsSupervisor = 1
        ORDER BY ee.LastName, ee.FirstName";

        public const string SelectEmployeeTypes =
        @"SELECT 
            EmployeeTypeId, 
            EmployeeTypeName 
        FROM HumanResources.EmployeeTypes
        ORDER BY EmployeeTypeName";
    }
}