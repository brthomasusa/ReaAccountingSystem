using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions
{
    public readonly record struct EmployeeSubmitFailureAction(string ErrorMessage);
}