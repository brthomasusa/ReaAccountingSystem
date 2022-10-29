#pragma warning disable CS8602

using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Snackbar;
using ReaAccountingSys.Client.Utilities;

namespace ReaAccountingSys.Client.Components.Common
{
    public partial class DataEntryForm<TWriteModel>  // 
    {
        private bool _isLoading;
        private Snackbar? _snackbar;
        private Validations? _validations;

        [Parameter] public string? ReturnUri { get; set; }
        [Parameter] public string? FormTitle { get; set; }
        [Parameter] public string? SnackBarMessage { get; set; }
        [Parameter] public RenderFragment? DataEntryFieldsTemplate { get; set; }
        [Parameter] public TWriteModel? WriteModel { get; set; }
        [Parameter] public Func<Task<OperationResult<bool>>>? SaveClickedEventHandler { get; set; }
        [Inject] public IMessageService? MessageService { get; set; }

        public async Task OnSave()
        {
            if (!await _validations!.ValidateAll())
                return;

            _isLoading = true;
            OperationResult<bool> saveResult = await SaveClickedEventHandler.Invoke();
            _isLoading = false;

            if (saveResult.Success)
            {
                await _snackbar!.Show();
            }
            else
            {
                await MessageService!.Error($"Error while saving info: {saveResult.NonSuccessMessage}", "Error");
            }
        }
    }
}