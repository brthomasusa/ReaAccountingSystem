using Fluxor;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions;

namespace ReaAccountingSys.Client.Services.Fluxor.HumanResources
{
    public class StateFacade
    {
        private readonly IDispatcher? _dispatcher;

        public StateFacade(IDispatcher dispatcher) => _dispatcher = dispatcher;

        public void GetEmployees(string filterName, int pageNumber, int pageSize)
            => _dispatcher!.Dispatch(new GetEmployeesAction(filterName, pageNumber, pageSize));

        public void SearchEmployeesByLastName(string searchTerm, string filterName, int pageNumber, int pageSize)
            => _dispatcher!.Dispatch(new SearchEmployeesByNameAction(searchTerm, filterName, pageNumber, pageSize));
    }
}