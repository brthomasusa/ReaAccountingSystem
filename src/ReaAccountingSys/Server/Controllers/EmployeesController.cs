using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Application.Handlers.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IReadRepositoryManager _readRepository;
        private readonly IWriteRepositoryManager _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController
        (
            ILogger<EmployeesController> logger,
            IReadRepositoryManager readRepository,
            IWriteRepositoryManager writeRepository,
            IUnitOfWork unitOfWork
        )
            => (_logger, _readRepository, _writeRepository, _unitOfWork) =
               (logger, readRepository, writeRepository, unitOfWork);

        [HttpGet("detail/{employeeId:Guid}", Name = "Details")]
        public async Task<ActionResult<EmployeeReadModel>> Details(Guid employeeId)
        {
            GetEmployeeParameter queryParams =
            new GetEmployeeParameter
            {
                EmployeeID = employeeId
            };

            OperationResult<EmployeeReadModel> result = await _readRepository.EmployeeAggregate.GetReadModelById(queryParams);

            if (result.Success)
            {
                return result.Result;
            }

            if (result.Exception is null)
            {
                _logger.LogWarning(result.NonSuccessMessage!);
                return StatusCode(400, result.NonSuccessMessage);
            }

            _logger.LogError(result.Exception.Message);
            return StatusCode(500, result.Exception.Message);
        }

        [HttpGet("list")]
        public async Task<ActionResult<PagedList<EmployeeListItem>>> GetEmployees([FromQuery] GetEmployeesParameters queryParameters)
        {
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItems(queryParameters);

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
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByStatus(queryParameters);

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
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByName(queryParameters);

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
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByNameAndStatus(queryParameters);

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

            OperationResult<List<EmployeeManager>> result = await _readRepository.EmployeeAggregate.GetEmployeeManagers(managersParams);

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

            OperationResult<List<EmployeeTypes>> result = await _readRepository.EmployeeAggregate.GetEmployeeTypes(typesParams);

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
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = writeModel };
            CreateEmployeeCommandHandler handler = new(_readRepository, _writeRepository, _unitOfWork);

            OperationResult<bool> result = await handler.Handle(command);

            if (result.Success)
            {
                GetEmployeeParameter queryParams = new() { EmployeeID = writeModel.EmployeeId };
                OperationResult<EmployeeReadModel> queryResult = await _readRepository.EmployeeAggregate.GetReadModelById(queryParams);

                if (queryResult.Success)
                {
                    return CreatedAtAction(nameof(Details), new { employeeId = writeModel.EmployeeId }, queryResult.Result);
                }
                else
                {
                    return StatusCode(201, "Create employee succeeded; unable to return newly created employee.");
                }
            }
            _logger.LogWarning(result.NonSuccessMessage!);
            return StatusCode(400, result.NonSuccessMessage);
        }
    }
}