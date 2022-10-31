using Fluxor;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Actions;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Actions;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Client.Services.Fluxor.HumanResources
{
    public class EmployeeAggregateStateFacade
    {
        private readonly IDispatcher? _dispatcher;

        public EmployeeAggregateStateFacade(IDispatcher dispatcher) => _dispatcher = dispatcher;

        public void GetEmployees(string filterName, int pageNumber, int pageSize)
            => _dispatcher!.Dispatch(new GetEmployeesAction(filterName, pageNumber, pageSize));

        public void SearchEmployeesByLastName(string searchTerm, string filterName, int pageNumber, int pageSize)
            => _dispatcher!.Dispatch(new SearchEmployeesByNameAction(searchTerm, filterName, pageNumber, pageSize));

        public void GetEmployeeDetails(string employeeId)
            => _dispatcher!.Dispatch(new GetEmployeeDetailsAction(employeeId));

        public void InitializeEmployeeCreateModel(EmployeeWriteModel model)
            => _dispatcher!.Dispatch(new InitializeEmployeeWriteModelAction(model));

        public void SetEmployeeCreateModelToNull()
            => _dispatcher!.Dispatch(new SetEmployeeCreateModelToNullAction());

        public Task LoadEmployeeLookups()
        {
            _dispatcher!.Dispatch(new LoadEmployeeLookupsAction());
            return Task.CompletedTask;
        }

        public Task CreateNewEmployee(EmployeeWriteModel model)
        {
            _dispatcher!.Dispatch(new EmployeeSubmitAction(model));
            return Task.CompletedTask;
        }
    }
}