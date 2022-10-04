using ReaAccountingSys.Shared.EmployeeService.gRPC.v1;
using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Application.Handlers.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.Shared.gRpcMappers.HumanResources;

using EmployeeServiceBase = ReaAccountingSys.Shared.EmployeeService.gRPC.v1.EmployeeService.EmployeeServiceBase;
using ReadModelEmployeeListItem = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem;
using GrpcEmployeeListItem = ReaAccountingSys.Shared.EmployeeService.gRPC.v1.EmployeeListItem;
using ReadModelEmployeeManager = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeManager;
using GrpcEmployeeManager = ReaAccountingSys.Shared.EmployeeService.gRPC.v1.EmployeeManager;
using ReadModelEmployeeTypes = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeTypes;
using GrpcEmployeeType = ReaAccountingSys.Shared.EmployeeService.gRPC.v1.EmployeeType;

namespace ReaAccountingSys.Server.Services.HumanResources
{
    public class EmployeeGrpcService : EmployeeServiceBase
    {
        private readonly ILogger<EmployeeGrpcService> _logger;
        private readonly IReadRepositoryManager _readRepository;
        private readonly IWriteRepositoryManager _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeGrpcService
        (
            ILogger<EmployeeGrpcService> logger,
            IReadRepositoryManager readRepository,
            IWriteRepositoryManager writeRepository,
            IUnitOfWork unitOfWork
        )
            : base()
        {
            _logger = logger;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }
        public override async Task<EmployeeListItemResponse> GetAll
        (
            GetEmployeesRequest request,
            ServerCallContext context
        )
        {
            EmployeeListItemResponse response = new();

            OperationResult<PagedList<ReadModelEmployeeListItem>> result =
                await _readRepository.EmployeeAggregate.GetAllListItems(request.ToParam());

            if (result.Success)
            {
                List<GrpcEmployeeListItem> responseList = new();
                result.Result.ForEach(item => responseList.Add(item.ToResponse()));

                response.EmployeeListItems.AddRange(responseList);
                response.MetaData.Add(LoadMetaData(result.Result!.MetaData));
            }

            return response;
        }

        public override async Task<EmployeeListItemResponse> GetAllByStatus
        (
            GetEmployeesStatusRequest request,
            ServerCallContext context
        )
        {
            EmployeeListItemResponse response = new();

            OperationResult<PagedList<ReadModelEmployeeListItem>> result =
                await _readRepository.EmployeeAggregate.GetAllListItemsByStatus(request.ToParam());

            if (result.Success)
            {
                List<GrpcEmployeeListItem> responseList = new();
                result.Result.ForEach(item => responseList.Add(item.ToResponse()));

                response.EmployeeListItems.AddRange(responseList);
                response.MetaData.Add(LoadMetaData(result.Result!.MetaData));
            }

            return response;
        }

        public override async Task<EmployeeListItemResponse> SearchByName
        (
            GetEmployeesByLastNameRequest request,
            ServerCallContext context
        )
        {
            EmployeeListItemResponse response = new();

            OperationResult<PagedList<ReadModelEmployeeListItem>> result =
                await _readRepository.EmployeeAggregate.GetAllListItemsByName(request.ToParam());

            if (result.Success)
            {
                List<GrpcEmployeeListItem> responseList = new();
                result.Result.ForEach(item => responseList.Add(item.ToResponse()));

                response.EmployeeListItems.AddRange(responseList);
                response.MetaData.Add(LoadMetaData(result.Result!.MetaData));
            }

            return response;
        }

        public override async Task<EmployeeListItemResponse> SearchByNameAndStatus
        (
            GetEmployeesByNameAndStatusRequest request,
            ServerCallContext context
        )
        {
            EmployeeListItemResponse response = new();

            OperationResult<PagedList<ReadModelEmployeeListItem>> result =
                await _readRepository.EmployeeAggregate.GetAllListItemsByNameAndStatus(request.ToParam());

            if (result.Success)
            {
                List<GrpcEmployeeListItem> responseList = new();
                result.Result.ForEach(item => responseList.Add(item.ToResponse()));

                response.EmployeeListItems.AddRange(responseList);
                response.MetaData.Add(LoadMetaData(result.Result!.MetaData));
            }

            return response;
        }

        public override async Task GetManagers
        (
            Empty request,
            IServerStreamWriter<GrpcEmployeeManager> responseStream,
            ServerCallContext context
        )
        {
            GetEmployeeManagersParameters managersParams = new GetEmployeeManagersParameters() { };

            OperationResult<List<ReadModelEmployeeManager>> result =
                await _readRepository.EmployeeAggregate.GetEmployeeManagers(managersParams);

            if (result.Success)
            {
                result.Result.ForEach(async item => await responseStream.WriteAsync(item.ToResponse()));
            }
        }

        public override async Task GetTypes
        (
            Empty request,
            IServerStreamWriter<GrpcEmployeeType> responseStream,
            ServerCallContext context
        )
        {
            GetEmployeeTypesParameters typesParams = new GetEmployeeTypesParameters() { };

            OperationResult<List<ReadModelEmployeeTypes>> result =
                await _readRepository.EmployeeAggregate.GetEmployeeTypes(typesParams);

            if (result.Success)
            {
                result.Result.ForEach(async item => await responseStream.WriteAsync(item.ToResponse()));
            }
        }

        public override Task<EmployeeReadModelResponse> GetById
        (
            GetEmployeeRequest request,
            ServerCallContext context
        )
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override Task<EmployeeReadModelResponse> Create
        (
            EmployeeWriteModelRequest request,
            ServerCallContext context
        )
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override Task<Empty> Delete
        (
            GetEmployeeRequest request,
            ServerCallContext context
        )
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override Task<Empty> Update
        (
            EmployeeWriteModelRequest request,
            ServerCallContext context
        )
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        private Dictionary<string, int> LoadMetaData(MetaData data)
        {
            Dictionary<string, int> metaData = new();
            metaData.Add("TotalCount", data.TotalCount);
            metaData.Add("PageSize", data.PageSize);
            metaData.Add("CurrentPage", data.CurrentPage);
            metaData.Add("TotalPages", data.TotalPages);

            return metaData;
        }
    }
}