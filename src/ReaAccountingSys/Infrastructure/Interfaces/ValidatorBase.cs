using ReaAccountingSys.Shared.Interfaces;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;

namespace ReaAccountingSys.Infrastructure.Interfaces
{
    public abstract class ValidatorBase<TWriteModel, TRepositoryManager>
        where TWriteModel : IWriteModel
        where TRepositoryManager : IReadRepositoryManager
    {
        protected ValidatorBase
        (
            TWriteModel writeModel,
            TRepositoryManager repositoryManager
        )
        {
            WriteModel = writeModel;
            RepositoryManager = repositoryManager;
        }

        protected TWriteModel WriteModel { get; init; }

        protected TRepositoryManager RepositoryManager { get; init; }

        public abstract Task<ValidationResult> Validate();
    }
}