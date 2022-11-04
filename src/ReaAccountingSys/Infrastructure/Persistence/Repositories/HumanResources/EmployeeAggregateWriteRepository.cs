#pragma warning disable CS8600, CS8602, CS8604

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReaAccountingSys.SharedKernel.CommonValueObjects;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces.HumanResources;
using ReaAccountingSys.Core.Shared;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources
{
    public class EmployeeAggregateWriteRepository : IEmployeeAggregateWriteRepository, IDisposable
    {
        private bool isDisposed;
        private readonly AppDbContext _dbContext;

        public EmployeeAggregateWriteRepository(AppDbContext ctx) => _dbContext = ctx;

        ~EmployeeAggregateWriteRepository() => Dispose(false);

        public async Task<OperationResult<Employee>> GetByIdAsync(Guid id)
        {
            try
            {
                Employee employee = await _dbContext.Employees.Where(ee => ee.Id == id)
                                                              .Include(emp => emp.TimeCards.Where(card => card.EmployeeId == id))
                                                              .FirstOrDefaultAsync();

                if (employee is null)
                {
                    string msg = $"Unable to locate employee with id '{id}'.";
                    return OperationResult<Employee>.CreateFailure(msg);
                }

                return OperationResult<Employee>.CreateSuccessResult(employee);
            }
            catch (Exception ex)
            {
                return OperationResult<Employee>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<Employee>> GetByConditionAsync
        (
           Expression<Func<Employee, bool>> expression,
           bool trackChanges
        )
        {
            try
            {
                Employee employee = trackChanges ? await _dbContext.Employees.Where(expression).SingleOrDefaultAsync() :
                                                   await _dbContext.Employees.Where(expression).AsNoTracking().SingleOrDefaultAsync();

                if (employee is not null)
                    return OperationResult<Employee>.CreateSuccessResult(employee);
                else
                    return OperationResult<Employee>.CreateFailure($"Failed to retrieve employee matching expression: {expression.ToString()}");
            }
            catch (Exception ex)
            {
                return OperationResult<Employee>.CreateFailure(ex.Message);
            }
        }

        public async Task<OperationResult<bool>> Exists(Guid id)
        {
            try
            {
                bool exist = await _dbContext.Employees.FindAsync(id) != null;
                if (exist)
                {
                    return OperationResult<bool>.CreateSuccessResult(exist);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure($"An employee with id '{id}' could not be found.");
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<bool>> AddAsync(Employee entity)
        {
            try
            {
                OperationResult<bool> result = await Exists(entity.Id);
                if (result.Success)
                {
                    string errMsg = $"Create employee failed! There is already an employee in the database with employee id '{entity.Id}'.";
                    return OperationResult<bool>.CreateFailure(errMsg);
                }
                else
                {
                    ExternalAgent agent = new(EntityGuidID.Create(entity.Id), AgentTypeEnum.Employee);
                    await _dbContext.ExternalAgents.AddAsync(agent);
                    await _dbContext.Employees.AddAsync(entity);
                    return OperationResult<bool>.CreateSuccessResult(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        public OperationResult<bool> Update(Employee entity)
        {
            try
            {
                foreach (TimeCard timeCard in entity.TimeCards)
                {
                    EntityState entityState = _dbContext.Entry(timeCard).State;

                    if (entityState == EntityState.Added)
                    {
                        EconomicEvent economicEvent = new(EntityGuidID.Create(timeCard.Id), EventTypeEnum.LaborAcquisition);
                        _dbContext.EconomicEvents.Add(economicEvent);
                    }
                }

                _dbContext.Employees.Update(entity);
                return OperationResult<bool>.CreateSuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        public OperationResult<bool> Delete(Employee entity)
        {
            try
            {
                string errMsg = $"Delete employee failed, unable to locate external agent with id: {entity.Id}";
                ExternalAgent agent = _dbContext.ExternalAgents.Find(entity.Id) ?? throw new ArgumentNullException(errMsg);

                _dbContext.Employees.Remove(entity);
                _dbContext.ExternalAgents.Remove(agent);

                return OperationResult<bool>.CreateSuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<bool>> DeleteTimeCardAsync(Guid timeCardId)
        {
            TimeCard timeCard = await _dbContext.TimeCards.FindAsync(timeCardId);

            if (timeCard is not null)
            {
                EconomicEvent economicEvent = _dbContext.EconomicEvents.Find(timeCardId);
                if (economicEvent is null)
                    return OperationResult<bool>.CreateFailure("Delete employee timecard failed! Unable to locate associated EconomicEvent.");

                _dbContext.TimeCards.Remove(timeCard);
                _dbContext.EconomicEvents.Remove(economicEvent);
                return OperationResult<bool>.CreateSuccessResult(true);
            }
            else
            {
                return OperationResult<bool>.CreateFailure("Delete employee timecard failed! Unable to timecard.");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                _dbContext.Dispose();
            }

            isDisposed = true;
        }

        private string GetExceptionMessage(Exception ex)
                => ex.InnerException == null ? ex.Message : ex.InnerException.Message;
    }
}