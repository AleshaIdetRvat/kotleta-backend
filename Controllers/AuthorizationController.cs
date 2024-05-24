using MariyaBackend.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MariyaBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly ILogger<AuthorizationController> _logger;
    private readonly IUserRepository _userRepository;

    public AuthorizationController(ILogger<AuthorizationController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("Register")]
    public async Task<IActionResult> Register(string login, string password)
    {
        var responseId = await _userRepository.RegisterUser(login, password);
        
        if(responseId is not null)
        {
            _logger.LogInformation($"REGISTERED USER: login = {login} password = {password}");
            return Ok(responseId);
        }

        _logger.LogInformation($"INVALID REGISTRATION: login = {login} password = {password}");
        return BadRequest();
    }

    [HttpGet]
    [Route("Authorize")]
    public async Task<IActionResult> Authorize(string login, string password)
    {
        var response = await _userRepository.Authorize(login, password);

        if (response)
        {
            _logger.LogInformation($"AUTHORIZED USER: login = {login} password = {password}");
            return Ok();
        }

        _logger.LogInformation($"INVALID AUTHORIZATION: login = {login} password = {password}");
        return BadRequest();
    }
}
