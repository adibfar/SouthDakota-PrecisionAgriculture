using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAS.Application.Features.Farms.Commands.AddEdit;
using PAS.Application.Features.Farms.Commands.Delete;
using PAS.Application.Features.Farms.Queries.Export;
using PAS.Application.Features.Farms.Queries.GetAll;
using PAS.Application.Features.Farms.Queries.GetById;
using PAS.Shared.Constants.Permission;
using System.Threading.Tasks;

namespace PAS.Server.Controllers.v1.FarmManagement
{
    public class FarmsController : BaseApiController<FarmsController>
    {
        /// <summary>
        /// Get All Farms
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Farms.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var farms = await _mediator.Send(new GetAllFarmsQuery());
            return Ok(farms);
        }

        /// <summary>
        /// Get a Farm By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Farms.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var farm = await _mediator.Send(new GetFarmByIdQuery() { Id = id });
            return Ok(farm);
        }

        /// <summary>
        /// Create/Update a Farm
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Farms.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditFarmCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Farm
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Farms.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteFarmCommand { Id = id }));
        }

        /// <summary>
        /// Search Farms and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Farms.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportFarmsQuery(searchString)));
        }
    }
}