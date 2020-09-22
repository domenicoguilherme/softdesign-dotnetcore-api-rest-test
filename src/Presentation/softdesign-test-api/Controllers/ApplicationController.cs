using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using softdesign_test_domain.Interfaces.Services;
using softdesign_test_domain.Models.DTOs;
using softdesign_test_domain.Models.Entity;
using softdesign_test_domain.Models.Response;
using softdesign_test_domain.Support;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace softdesign_test_api.Controllers
{
    [ApiController]
    [Route("api/application")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Get all stored applications
        /// </summary>
        /// <returns>List of all applications</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                var result = _applicationService.Get();
                var response = new SuccessResponse<List<ApplicationEntity>>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel(ex));
            }
        }

        /// <summary>
        /// Get an specific application by id
        /// </summary>
        /// <param name="id">Application id to be filtered</param>
        /// <returns>Return the filtered application</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var result = await _applicationService.Get(id);

                if (result != null)
                {
                    var response = new SuccessResponse<ApplicationEntity>(result);

                    return Ok(response);
                }
                else
                {
                    return NotFound(Constants.NOT_FOUND_REGISTRY);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel(ex));
            }
        }

        /// <summary>
        /// Insert a new application
        /// </summary>
        /// <param name="applicationDTO">Body of application to be inserted</param>
        /// <returns>Success/error message</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(ApplicationDTO applicationDTO)
        {
            try
            {
                var applicationEntity = new ApplicationEntity();

                applicationEntity.Map(applicationDTO);

                await _applicationService.InsertAsync(applicationEntity);

                return StatusCode((int)HttpStatusCode.Created, Constants.INSERTED_REGISTRY);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel(ex));
            }
        }

        /// <summary>
        /// Update an already stored application 
        /// </summary>
        /// <param name="id">Application Id</param>
        /// <param name="applicationDTO">New body values</param>
        /// <returns>Success/error message</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch([FromRoute] string id, ApplicationDTO applicationDTO)
        {
            try
            {
                var applicationEntity = await _applicationService.Get(id);

                if (applicationEntity == null) return NotFound(Constants.NOT_FOUND_REGISTRY);

                applicationEntity.Map(applicationDTO);

                await _applicationService.UpdateAsync(id, applicationEntity);

                return StatusCode((int)HttpStatusCode.OK, Constants.UPDATED_REGISTRY);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel(ex));
            }
        }

        /// <summary>
        /// Delete an application by id
        /// </summary>
        /// <param name="id">Application Id</param>
        /// <returns>Success/error message</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _applicationService.DeleteAsync(id);

                return StatusCode((int)HttpStatusCode.OK, Constants.DELETED_REGISTRY);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseModel(ex));
            }
        }
    }
}
