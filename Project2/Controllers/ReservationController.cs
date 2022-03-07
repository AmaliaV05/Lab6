using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project2.Services;
using Project2.ViewModels.Reservations;
using System.Threading.Tasks;

namespace Project2.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IMapper _mapper;
        private IReservationService _reservationService;

        public ReservationController(ILogger<ReservationController> logger, IMapper mapper,IReservationService reservationService)
        {
            _logger = logger;
            _mapper = mapper;
            _reservationService = reservationService;
        }

        /// <summary>
        /// Make one or more reservations to films
        /// </summary>
        /// <param name="newReservationRequest"></param>
        /// <returns>returns Ok if reservation is successful, else Bad Request</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MakeReservation(NewReservationRequest newReservationRequest)
        {
            //var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var query = await _reservationService.MakeReservation(newReservationRequest);
            
            if(query == false)
            {
                return BadRequest();
            }
            
            return Ok();
        }

        /// <summary>
        /// Get all reservations made
        /// </summary>
        /// <returns>Returns all revservations</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var result = await _reservationService.GetAll();
            
            var resultViewModel = _mapper.Map<ReservationForUserResponse>(result);

            return Ok(resultViewModel);
        }
    }
}
