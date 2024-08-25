using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SempleApiJwtWithRedis.Application.Domain;
using SempleApiJwtWithRedis.Application.Services;
using System.Net;

namespace SempleApiJwtWithRedis.Controllers
{
    public class UserController(IUserRepository userRepository) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpPost]
        [Route("creating")]
        [ProducesResponseType(typeof(User),(int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody]User model) 
        {
            if (!ModelState.IsValid)
                return NoContent();

            try
            {
                var user = new User(model.Name, model.Username, model.Password);
                var result = await _userRepository.Add(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return NoContent();

            var user = await _userRepository.GetUserByUsername(model.Username);
                if (user == null)
                    return NotFound(new { message = "Usuário não encontrado" });

                if (user.Password != model.Password)
                    return BadRequest(new { message = "Senha inválida" });

                var token = TokenService.GenerateToken(user);

                return Ok(new { Token = token });            
        }
 
        [Authorize]
        [HttpPost]
        [Route("creating-authorize")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> PostAuthorize([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return NoContent();
          
            try
            {
                var user = new User(model.Name, model.Username, model.Password);
                var result = await _userRepository.Add(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
