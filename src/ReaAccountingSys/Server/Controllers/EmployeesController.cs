using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using LoggingService.Interfaces;

using ReaAccountingSys.Infrastructure.Interfaces;
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
        private ILoggerManager _logger;
        private readonly IReadRepositoryManager _readRepository;

        public EmployeesController(ILoggerManager logger, IReadRepositoryManager repo)
            => (_logger, _readRepository) = (logger, repo);

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
                _logger.LogWarn(result.NonSuccessMessage!);
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

        // [HttpPost("create")]
        // public async Task<IActionResult> CreateEmployeeInfo([FromBody] EmployeeWriteModel writeModel)
        // {
        //     OperationResult<bool> writeResult = await _cmdSvc.CreateEmployeeInfo(writeModel);
        //     if (writeResult.Success)
        //     {
        //         GetEmployeeParameter queryParams = new() { EmployeeID = writeModel.EmployeeId };
        //         OperationResult<EmployeeReadModel> queryResult = await _qrySvc.GetEmployeeReadModel(queryParams);

        //         if (queryResult.Success)
        //         {
        //             return CreatedAtAction(nameof(Details), new { employeeId = writeModel.EmployeeId }, queryResult.Result);
        //         }
        //         else
        //         {
        //             return StatusCode(201, "Create employee succeeded; unable to return newly created employee.");
        //         }
        //     }

        //     if (writeResult.Exception is null)
        //     {
        //         _logger.LogWarning(writeResult.NonSuccessMessage);
        //         return StatusCode(400, writeResult.NonSuccessMessage);
        //     }

        //     _logger.LogError(writeResult.Exception.Message);
        //     return StatusCode(500, writeResult.Exception.Message);
        // }
    }
}