using AutoMapper;
using GalponIndustrial_API.Models;
using GalponIndustrial_API.Models.Dtos;
using GalponIndustrial_API.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using System.Net;
using System.Timers;

namespace GalponIndustrial_API.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroGalponController : ControllerBase
    {
        private readonly ILogger<NumeroGalponController> _logger;
        private readonly IGalponRepository _galponRepo;
        private readonly INumeroGalponRepository _nroGalponRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroGalponController (ILogger<NumeroGalponController> logger, IGalponRepository galponRepo,
                                            INumeroGalponRepository nroGalponRepo, IMapper mapper)
        {
            _logger = logger;
            _nroGalponRepo = nroGalponRepo;
            _mapper = mapper;
            _response = new();
            _galponRepo = galponRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroGalpones()
        {
            try
            {
                _logger.LogInformation("Obteniendo lista de NumeroGalpones");
                var listaNroGalpon = await _nroGalponRepo.ObtenerTodos();
                List<NumeroGalponDto> listaNroGalponDto = _mapper.Map<List<NumeroGalponDto>>(listaNroGalpon);
                _response.Resultado = listaNroGalponDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsExitoso = false;
                _response.ExceptionMessages = new List<string>{ ex.ToString() };
            }
            return _response;
            
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetNroGalponById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error en peticion de NumeroGalpon con Id=0");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = _response.ErrorIdCero();
                    return BadRequest(_response);
                }

                var nroGalpon = await _nroGalponRepo.Obtener(n => n.NroGalpon == id, Tracked: false);
                if (nroGalpon == null)
                {
                    _logger.LogError("Id de NumeroGalpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = _response.ErrorNotFound(id);
                    return NotFound(_response);
                }

                NumeroGalponDto nroGalponDto = _mapper.Map<NumeroGalponDto>(nroGalpon);
                _response.Resultado = nroGalponDto;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
            
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Post([FromBody] NumeroGalponCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState Inválido");
                    return BadRequest(ModelState);
                }
                if (await _nroGalponRepo.Obtener(n=>n.NroGalpon == createDto.NroGalpon) != null)
                {
                    ModelState.AddModelError("Exist", "El numero de Galpón ya existe en la base de datos");
                    return BadRequest(ModelState);
                }
                if (await _galponRepo.Obtener(g => g.Id == createDto.GalponId) == null)
                {
                    ModelState.AddModelError("ForeingKey", "El Id del Galpon no existe");
                    return BadRequest(ModelState);
                }

                NumeroGalpon nroGalpon = _mapper.Map<NumeroGalpon>(createDto);
                nroGalpon.FechaCreacion = DateTime.Now;
                nroGalpon.FechaActualizacion = DateTime.Now;

                await _nroGalponRepo.Crear(nroGalpon);
                _response.Resultado = nroGalpon;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtAction(nameof(GetNroGalponById), new { nroGalpon.NroGalpon }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
            
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGalpon(int id, [FromBody] NumeroGalponUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState Inválido");
                    return BadRequest(ModelState);
                }
                if (id != updateDto.NroGalpon)
                {
                    _logger.LogError("Error en peticion de Id");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = _response.ErrorDifId(id, updateDto.NroGalpon);
                    return BadRequest(_response);
                }
                
                var existingNroGalpon = await _nroGalponRepo.Obtener(n => n.NroGalpon == id, Tracked: false);
                if (existingNroGalpon == null)
                {
                    _logger.LogError("Id de NumeroGalpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = _response.ErrorNotFound(id);
                    return NotFound(_response);
                }

                if (await _galponRepo.Obtener(g => g.Id == updateDto.GalponId) == null)
                {
                    ModelState.AddModelError("ForeingHey", "El Id del Galpon no existe");
                    return BadRequest(ModelState);
                }

                NumeroGalpon nroGalpon = _mapper.Map<NumeroGalpon>(updateDto);
                nroGalpon.FechaCreacion = existingNroGalpon.FechaCreacion;

                await _nroGalponRepo.Actualizar(nroGalpon);
                _response.Resultado = _response.Editado();
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return BadRequest(_response);
            
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error en peticion de NumeroGalpon con Id=0");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = _response.ErrorIdCero();
                    return BadRequest(_response);
                }
                var nroGalponToDelete = await _nroGalponRepo.Obtener(n => n.NroGalpon == id);
                if (nroGalponToDelete == null)
                {
                    _logger.LogError("Id de NumeroGalpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = _response.ErrorNotFound(id);
                    return NotFound(_response);
                }
                await _nroGalponRepo.Borrar(nroGalponToDelete);
                _response.Resultado = _response.Eliminado();
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePartialNumeroGalpon (int id, JsonPatchDocument<NumeroGalponUpdateDto> jsonPatch)
        {
            try
            {
                var nroGalpon = await _nroGalponRepo.Obtener(n => n.NroGalpon == id, Tracked: false);
                if (nroGalpon == null)
                {
                    _logger.LogError("Id de NumeroGalpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage = _response.ErrorNotFound(id);
                    return NotFound(_response);
                }
                if (jsonPatch == null || jsonPatch.Operations.Count == 0 || jsonPatch.Operations.Any(o => o.op != "replace"))
                {
                    _logger.LogError("Error al Actualizar, Json Nulo");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessage = _response.ErrorJson();
                    return BadRequest(_response);
                }

                NumeroGalponUpdateDto nroGalponDto = _mapper.Map<NumeroGalponUpdateDto>(nroGalpon);

                jsonPatch.ApplyTo(nroGalponDto);
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState Inválido");
                    return BadRequest(ModelState);
                }
                NumeroGalpon nroGalponActualizado = _mapper.Map<NumeroGalpon>(nroGalponDto);
                nroGalponActualizado.FechaCreacion = nroGalpon.FechaCreacion;

                await _nroGalponRepo.Actualizar(nroGalponActualizado);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Resultado = _response.Editado();
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return BadRequest(_response);
            
        }
    }
}
