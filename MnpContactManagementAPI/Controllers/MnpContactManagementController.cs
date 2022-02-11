using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MNPDatabaseRepository.Models;
using MNPInterfaces;
using MNPModels;
using System.Net;
using System.Text.Json;

namespace MnpContactManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MnpContactManagementController : ControllerBase
    {
        private readonly ILogger<MnpContactManagementController> _logger;
        private readonly IMapper _mapper;
        private readonly IContactManagementBusinessLogic _iContactManagementBusinessLogic;
        public MnpContactManagementController(ILogger<MnpContactManagementController> logger, IMapper mapper, IContactManagementBusinessLogic iContactManagementBusinessLogic)
        {
            _logger = logger;
            _mapper = mapper;
            _iContactManagementBusinessLogic = iContactManagementBusinessLogic;
        }
        /// <summary>
        /// Get the List of Companies for DropDown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCompaniesDD")]
        public IActionResult GetCompaniesDD()
        {
            try
            {                
                var companiesList = _iContactManagementBusinessLogic.GetCompaniesList();                
                if (companiesList.Count == 0)
                    return NotFound(HttpStatusCode.NotFound);
                var dto = _mapper.Map<List<CompaniesTable>, List<CompaniesTableDTO>>(companiesList);
                var companiesListDTO = JsonSerializer.Serialize(dto);
                if (!string.IsNullOrEmpty(companiesListDTO))
                {

                    return Ok(companiesListDTO);
                }
                return NotFound(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
