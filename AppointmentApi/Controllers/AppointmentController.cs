using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Data.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AppointmentApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentApi.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentController(IAppointmentRepository userRepository)
        {
            _appointmentRepository = userRepository;
        }
        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _appointmentRepository.GetAppointments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                return Ok(await _appointmentRepository.GetAppointment(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult> GetAppointmentsByUserId(int userId)
        {
            try
            {
                return Ok(await _appointmentRepository.GetAppointmentsByUserID(userId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        // POST api/<AppointmentController>
        [HttpPost]
        public async Task<ActionResult> Post(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                    return BadRequest();

                var newUser = await _appointmentRepository.AddAppointment(appointment);

                return StatusCode(StatusCodes.Status201Created, newUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new user record");
            }
        }

        /*
        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
