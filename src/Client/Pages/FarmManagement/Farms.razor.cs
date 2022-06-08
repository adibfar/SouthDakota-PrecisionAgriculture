using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using PAS.Application.Features.Farms.Commands.AddEdit;
using PAS.Application.Features.Farms.Queries.GetAll;
using PAS.Client.Extensions;
using PAS.Client.Infrastructure.Managers.FarmManagement.Farm;
using PAS.Shared.Constants.Application;
using PAS.Shared.Constants.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PAS.Client.Pages.FarmManagement
{
    public partial class Farms
    {
        [Inject] private IFarmManager FarmManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllFarmsResponse> _farmList = new();
        private GetAllFarmsResponse _farm = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateFarms;
        private bool _canEditFarms;
        private bool _canDeleteFarms;
        private bool _canExportFarms;
        private bool _canSearchFarms;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateFarms = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Farms.Create)).Succeeded;
            _canEditFarms = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Farms.Edit)).Succeeded;
            _canDeleteFarms = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Farms.Delete)).Succeeded;
            _canExportFarms = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Farms.Export)).Succeeded;
            _canSearchFarms = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Farms.Search)).Succeeded;

            await GetFarmsAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetFarmsAsync()
        {
            var response = await FarmManager.GetAllAsync();
            if (response.Succeeded)
            {
                _farmList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = "Delete Content";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await FarmManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task ExportToExcel()
        {
            var response = await FarmManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Farms).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? "Farms exported" : "Filtered Farms exported", Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _farm = _farmList.FirstOrDefault(c => c.Id == id);
                if (_farm != null)
                {
                    parameters.Add(nameof(AddEditFarmModal.AddEditFarmModel), new AddEditFarmCommand
                    {
                        Id = _farm.Id,
                        Name = _farm.Name,
                        Description = _farm.Description
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditFarmModal>(id == 0 ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _farm = new GetAllFarmsResponse();
            await GetFarmsAsync();
        }

        private bool Search(GetAllFarmsResponse brand)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (brand.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (brand.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}