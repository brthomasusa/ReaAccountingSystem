#pragma warning disable CS8602

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        private EditContext? _editContext;

        [Parameter] public string? ReturnUri { get; set; }
        [Parameter] public string? FormTitle { get; set; }
        [Parameter] public string? SnackBarMessage { get; set; }
        [Parameter] public RenderFragment? DataEntryFieldsTemplate { get; set; }
        [Parameter] public TWriteModel? WriteModel { get; set; }
        [Parameter] public Func<Task<OperationResult<bool>>>? SaveClickedEventHandler { get; set; }
        [Inject] public IMessageService? MessageService { get; set; }

        protected override void OnInitialized()
        {
            if (_validations is null)
            {
                Console.WriteLine("DataEntryForm: _validations is null");
            }
            else
            {
                Console.WriteLine("DataEntryForm: _validations is not null");
                _editContext = _validations.EditContext;
            }
        }

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