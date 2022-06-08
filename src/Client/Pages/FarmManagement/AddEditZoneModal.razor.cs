using PAS.Application.Requests;
using PAS.Client.Extensions;
using PAS.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using PAS.Client.Infrastructure.Managers.FarmManagement.Zone;
using PAS.Client.Infrastructure.Managers.FarmManagement.Farm;
using PAS.Application.Features.Farms.Queries.GetAll;
using PAS.Application.Features.Zones.Commands.AddEdit;

namespace PAS.Client.Pages.FarmManagement
{
    public partial class AddEditZoneModal
    {
        [Inject] private IZoneManager ZoneManager { get; set; }
        [Inject] private IFarmManager FarmManager { get; set; }

        [Parameter] public AddEditZoneCommand AddEditZoneCommand { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllFarmsResponse> _farms = new();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await ZoneManager.SaveAsync(AddEditZoneCommand);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await LoadFarmsAsync();
        }

        private async Task LoadFarmsAsync()
        {
            var data = await FarmManager.GetAllAsync();
            if (data.Succeeded)
            {
                _farms = data.Data;
            }
        }

        private void DeleteAsync()
        {
            AddEditZoneCommand.UploadRequest = new UploadRequest();
        }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                AddEditZoneCommand.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.ProfilePicture, Extension = extension };
            }
        }

        private async Task<IEnumerable<int>> SearchFarms(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _farms.Select(x => x.Id);

            return _farms.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
    }
}