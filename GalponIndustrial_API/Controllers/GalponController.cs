using AutoMapper;
using GalponIndustrial_API.Models;
using GalponIndustrial_API.Repository.IRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using System.Net;

namespace GalponIndustrial_API.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class GalponController : ControllerBase
    {
        private readonly ILogger<GalponController> _logger;
        private readonly IGalponRepository _galponRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public GalponController (ILogger<GalponController> logger,
                                    IGalponRepository galponRepo,
                                        IMapper mapper)
        {
            _logger = logger;
            _galponRepo = galponRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetGalpones()
        {
            try
            {
                _logger.LogInformation("Obteniendo lista de Galpones");
                var listaGalpon = await _galponRepo.ObtenerTodos();
                List<GalponDto> listaGalponDto = _mapper.Map<List<GalponDto>>(listaGalpon);
                _response.Resultado = listaGalponDto;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetGalponById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error en peticion de Galpon con Id=0");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorIdCero();
                    return BadRequest(_response);
                }

                var galpon = await _galponRepo.Obtener(g => g.Id == id, Tracked: false);
                if (galpon == null)
                {
                    _logger.LogError("Id de Galpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorNotFound(id);
                    return NotFound(_response);
                }

                GalponDto galponDto = _mapper.Map<GalponDto>(galpon);
                _response.Resultado = galponDto;
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
        public async Task<ActionResult<APIResponse>> Post([FromBody] GalponCreateDto galponCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState Inválido");
                    return BadRequest(ModelState);
                }

                Galpon galpon = _mapper.Map<Galpon>(galponCreateDto);
                galpon.FechaCreacion = DateTime.Now;
                galpon.FechaActualizacion = DateTime.Now;

                await _galponRepo.Crear(galpon);
                _response.Resultado = galpon;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtAction(nameof(GetGalponById), new { galpon.Id }, _response);
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
        public async Task<IActionResult> UpdateGalpon(int id, [FromBody] GalponUpdateDto galponUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState Inválido");
                    return BadRequest(ModelState);
                }
                if (id != galponUpdateDto.Id)
                {
                    _logger.LogError("Error en peticion de Id");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorDifId(id, galponUpdateDto.Id);
                    return BadRequest(_response);
                }

                var existingGalpon = await _galponRepo.Obtener(g => g.Id == id, Tracked: false);
                if (existingGalpon == null)
                {
                    _logger.LogError("Id de Galpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorNotFound(id);
                    return NotFound(_response);
                }

                Galpon galpon = _mapper.Map<Galpon>(galponUpdateDto);
                galpon.FechaCreacion = existingGalpon.FechaCreacion;

                await _galponRepo.Actualizar(galpon);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Editado();

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
                    _logger.LogError("Error en peticion de Galpon con Id=0");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorIdCero();
                    return BadRequest(_response);
                }
                var galponToDelete = await _galponRepo.Obtener(g => g.Id == id);
                if (galponToDelete == null)
                {
                    _logger.LogError("Id de Galpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorNotFound(id);
                    return NotFound(_response);
                }
                await _galponRepo.Borrar(galponToDelete);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Eliminado();

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
        public async Task<IActionResult> UpdatePartialGalpon (int id, JsonPatchDocument<GalponUpdateDto> jsonPatch)
        {
            try
            {
                var galpon = await _galponRepo.Obtener(g => g.Id == id, Tracked: false);
                if (galpon == null)
                {
                    _logger.LogError("Id de Galpon No encontrado");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorNotFound(id);
                    return NotFound(_response);
                }
                if (jsonPatch == null || jsonPatch.Operations.Count == 0 || jsonPatch.Operations.Any(o => o.op != "replace"))
                {
                    _logger.LogError("Error al Actualizar, Json Nulo");
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorJson();
                    return BadRequest(_response);
                }

                GalponUpdateDto galponDto = _mapper.Map<GalponUpdateDto>(galpon);

                jsonPatch.ApplyTo(galponDto);
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState Inválido");
                    return BadRequest(ModelState);
                }
                Galpon galponActualizado = _mapper.Map<Galpon>(galponDto);
                galponActualizado.FechaCreacion = galpon.FechaCreacion;

                await _galponRepo.Actualizar(galponActualizado);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Editado();
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
