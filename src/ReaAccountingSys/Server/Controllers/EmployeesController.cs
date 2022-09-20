using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using LoggingService.Interfaces;

using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Infrastructure.Application.Commands.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ILoggerManager _logger;
        private readonly IEmployeeAggregateReadRepository _readRepository;

        public EmployeesController(IMediator mediator, ILoggerManager logger, IEmployeeAggregateReadRepository repo)
            => (_mediator, _logger, _readRepository) = (mediator, logger, repo);

        [HttpGet("detail/{employeeId:Guid}", Name = "Details")]
        public async Task<ActionResult<EmployeeReadModel>> Details(Guid employeeId)
        {
            GetEmployeeParameter queryParams =
            new GetEmployeeParameter
            {
                EmployeeID = employeeId
            };

            OperationResult<EmployeeReadModel> result = await _mediator.Send(new GetEmployeeByIdQry(queryParams));

            if (result.Success)
            {
                return result.Result;
            }

            if (result.Exception is null)
            {
                _logger.LogWarn(result.NonSuccessMessage!);
                return StatusCode(400, result.NonSuccessMessage);
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("list")]
        public async Task<ActionResult<PagedList<EmployeeListItem>>> GetEmployees([FromQuery] GetEmployeesParameters queryParameters)
        {
            OperationResult<PagedList<EmployeeListItem>> result = await _mediator.Send(new GetEmployeesQry(queryParameters));

            if (result.Success)
            {
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Result.MetaData));
                return result.Result;
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("list/bystatus")]
        public async Task<ActionResult<PagedList<EmployeeListItem>>> GetEmployees([FromQuery] GetEmployeesByStatusParameters queryParameters)
        {
            OperationResult<PagedList<EmployeeListItem>> result = await _mediator.Send(new GetEmployeesByStatusQry(queryParameters));

            if (result.Success)
            {
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Result.MetaData));
                return result.Result;
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedList<EmployeeListItem>>> GetEmployees([FromQuery] GetEmployeesByLastNameParameters queryParameters)
        {
            OperationResult<PagedList<EmployeeListItem>> result = await _mediator.Send(new GetEmployeesByNameQry(queryParameters));

            if (result.Success)
            {
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Result.MetaData));
                return result.Result;
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("search/bystatus")]
        public async Task<ActionResult<PagedList<EmployeeListItem>>> GetEmployees([FromQuery] GetEmployeesByNameAndStatusParameters queryParameters)
        {
            OperationResult<PagedList<EmployeeListItem>> result = await _mediator.Send(new GetEmployeesByNameAndStatusQry(queryParameters));

            if (result.Success)
            {
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Result.MetaData));
                return result.Result;
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("managers")]
        public async Task<ActionResult<List<EmployeeManager>>> GetEmployeeManagers()
        {
            GetEmployeeManagersParameters managersParams = new GetEmployeeManagersParameters() { };

            OperationResult<List<EmployeeManager>> result = await _mediator.Send(new GetEmployeeManagersQry(managersParams));

            if (result.Success)
            {
                return result.Result;
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("employeetypes")]
        public async Task<ActionResult<List<EmployeeTypes>>> GetEmployeeTypes()
        {
            GetEmployeeTypesParameters typesParams = new GetEmployeeTypesParameters() { };

            OperationResult<List<EmployeeTypes>> result = await _mediator.Send(new GetEmployeeTypesQry(typesParams));

            if (result.Success)
            {
                return result.Result;
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployeeInfo([FromBody] EmployeeWriteModel writeModel)
        {
            OperationResult<bool> writeResult = await _mediator.Send(new CreateEmployeeCmd(writeModel));
            if (writeResult.Success)
            {
                GetEmployeeParameter queryParams = new() { EmployeeID = writeModel.EmployeeId };
                OperationResult<EmployeeReadModel> queryResult = await _mediator.Send(new GetEmployeeByIdQry(queryParams));

                if (queryResult.Success)
                {
                    return CreatedAtAction(nameof(Details), new { employeeId = writeModel.EmployeeId }, queryResult.Result);
                }
                else
                {
                    return StatusCode(201, "Create employee succeeded; unable to return newly created employee.");
                }
            }

            if (writeResult.Exception is null)
            {
                _logger.LogWarn(writeResult.NonSuccessMessage!);
                return StatusCode(400, writeResult.NonSuccessMessage);
            }

            _logger.LogError(writeResult.Exception.Message);
            return StatusCode(500, writeResult.Exception.Message);
        }
    }
}