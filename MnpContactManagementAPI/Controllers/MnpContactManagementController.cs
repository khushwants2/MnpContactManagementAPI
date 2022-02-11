using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MNPDatabaseRepository.Models;
using MNPInterfaces;
using MNPModels;
using System.Net;
using System.Net.Mime;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompaniesTableDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpGet]
        [Route("GetMNPContanctManagementList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MnpContactManagementDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMNPContanctManagementList()
        {
            try
            {
                var mnpContactManagementList = _iContactManagementBusinessLogic.GetMnpContactManagementTableData();
                if (mnpContactManagementList.Count == 0)
                    return NotFound(HttpStatusCode.NotFound);
                var dto = _mapper.Map<List<MnpContactManagement>, List<MnpContactManagementDTO>>(mnpContactManagementList);
                var mnpContactManagementListDTO = JsonSerializer.Serialize(dto);
                if (!string.IsNullOrEmpty(mnpContactManagementListDTO))
                {
                    return Ok(mnpContactManagementListDTO);
                }
                return NotFound(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetMNPContanctManagementList/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MnpContactManagementDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMNPContanctManagementById(int id)
        {
            try
            {
                var mnpContactManagement = _iContactManagementBusinessLogic.GetMnpContactManagementTableDataById(id);
                if (mnpContactManagement == null)
                    return NotFound(HttpStatusCode.NotFound);
                var dto = _mapper.Map<MnpContactManagement, MnpContactManagementDTO>(mnpContactManagement);
                var mnpContactManagementDTO = JsonSerializer.Serialize(dto);
                if (!string.IsNullOrEmpty(mnpContactManagementDTO))
                {
                    return Ok(mnpContactManagementDTO);
                }
                return NotFound(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("GetMNPContanctManagementList")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveMNPContanctManagement(MnpContactManagementDTO mnpContactManagementDTO)
        {
            try
            {
                var mnpContactManagement = _mapper.Map<MnpContactManagementDTO, MnpContactManagement>(mnpContactManagementDTO);
                if(!ModelState.IsValid)
                    return BadRequest(HttpStatusCode.BadRequest);
                if (mnpContactManagement == null)
                    return BadRequest(HttpStatusCode.ExpectationFailed);

                bool status;
                if (mnpContactManagement.Id == 0)
                    status = _iContactManagementBusinessLogic.CreateNewContact(mnpContactManagement);
                else
                    status = _iContactManagementBusinessLogic.SaveContact(mnpContactManagement);

                if(status)
                    return Ok("Contact Saved Succeessfully");
                else
                    return BadRequest(HttpStatusCode.InternalServerError + "Error: Contact Saved Unsuccessfull");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
