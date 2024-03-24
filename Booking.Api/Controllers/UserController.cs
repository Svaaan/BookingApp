using Microsoft.AspNetCore.Mvc;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using System.Reflection;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>The User user.</returns>
        /// <response code="200">Returns the newly created user.</response>
        /// <response code="400">If the user is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> PostUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User object is null.");
            }

            PropertyInfo[] properties = typeof(UserDTO).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(userDTO) == null)
                {
                    return BadRequest($"Property {property.Name} is null.");
                }
            }
            var user = new User { Id = userDTO .Id, CompanyId = userDTO .CompanyId, Email = userDTO .Email, Name = userDTO.Name, LastName = userDTO.LastName, Password = userDTO.Password };
            var createUser = await _userRepository.CreateUserAsync(user);
            return Ok(createUser);
        }
        /// <summary>
        /// Lists all user.
        /// </summary>
        /// <returns>A list of user.</returns>
        /// <response code="200">Returns the list of user.</response>
        /// <response code="500">If an error occurs while retrieving the user.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<User>>> ListAllUser()
        {
            var userList = await _userRepository.GetAllUsersAsync();
            if (userList == null)
            {
                return NotFound();
            }
            return Ok(userList);
        }

        /// <summary>
        /// Delete a user ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which user that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> DeleteUserById(int id)
        {
            var deleteUser = await this._userRepository.DeleteUserByIdAsync(id);
            if (deleteUser == null)
            {
                return NotFound();
            }
            return Ok(deleteUser);
        }

        /// <summary>
        /// Retrieve a specific User from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Update a user by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUser"></param>
        /// <returns>Returns the updated user.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> UpdateUSer(int id, [FromBody] User updateUser)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateUser
                if (id != updateUser.Id)
                {
                    return BadRequest("Mismatched user ID in the request.");
                }

                var updatedUser= await _userRepository.UpdateUserById(id, updateUser);
                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user. userId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
            }
        }
    }
}
