using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAS.Application.Features.Zones.Commands.AddEdit;
using PAS.Application.Features.Zones.Commands.Delete;
using PAS.Application.Features.Zones.Queries.Export;
using PAS.Application.Features.Zones.Queries.GetAllPaged;
using PAS.Shared.Constants.Permission;
using System.Threading.Tasks;

namespace PAS.Server.Controllers.v1.FarmManagement
{
    public class ZonesController : BaseApiController<ZonesController>
    {
        /// <summary>
        /// Get All Zones
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Zones.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var zones = await _mediator.Send(new GetAllZonesQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(zones);
        }

        /// <summary>
        /// Add/Edit a Zone
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Zones.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditZoneCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Zone
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.Zones.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteZoneCommand { Id = id }));
        }

        /// <summary>
        /// Search Zones and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Zones.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportZonesQuery(searchString)));
        }
    }
}