using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data.EFCore;
using Microsoft.AspNetCore.Http;
using UserApi.Model;
using UserApi.Nlog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/user")]
    //[Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILog logger;
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository _userRepository, ILog _log)
        {
            userRepository = _userRepository;
            logger = _log;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            logger.Informatiom("Try getting users list");
            try
            {
                return Ok(await userRepository.GetUsers());
            }
            catch (Exception ex)
            {
                logger.Error("Error getting users list : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            logger.Informatiom("Try getting user by id");
            try
            {
                return Ok(await userRepository.GetUser(id));
            }
            catch (Exception ex)
            {
                logger.Error("Error getting user by id : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            logger.Informatiom("Try addming new user");
            try
            {
                if (user == null)
                    return BadRequest();

                var newUser = await userRepository.AddUser(user);

                logger.Informatiom("User added successfully");
                return StatusCode(StatusCodes.Status201Created, newUser);
            }
            catch (Exception ex)
            {
                logger.Error("Calling GetUsers : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new user record");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, User user)
        {
            logger.Informatiom("Try updating user");
            try
            {
                if (id != user.Id)
                    return BadRequest("Id mismatch");

                var userToUpdate = await userRepository.GetUser(id);

                logger.Informatiom("User updated successfully");
                // Throw expression
                return userToUpdate == null ? throw new NullReferenceException() : StatusCode(StatusCodes.Status202Accepted, (await userRepository.UpdateUser(user)));
            }
            catch (Exception ex)
            {
                logger.Error("Error updating the user record : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the user record");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
