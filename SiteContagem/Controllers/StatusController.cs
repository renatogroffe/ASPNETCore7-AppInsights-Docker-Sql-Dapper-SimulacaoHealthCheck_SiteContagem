using System.Net;
using Microsoft.AspNetCore.Mvc;
using SiteContagem.Models;

namespace SiteContagem.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    public static bool Healthy { get; set; } = true;
    private readonly ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<StatusApi> Get()
    {
        var status = GetCurrentStatusApplication();
        if (Healthy)
        {
            _logger.LogInformation("Simulacao status = OK");
            return status;
        }
        else
        {
            _logger.LogError("Simulacao status = Unhealthy");
            return new ObjectResult(status)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    [HttpGet("healthy")]
    public ActionResult<StatusApi> SetHealthy()
    {
        Healthy = true;
        _logger.LogInformation("Novo status = Healthy");
        return GetCurrentStatusApplication();
    }

    [HttpGet("unhealthy")]
    public ActionResult<StatusApi> SetUnhealthy()
    {
        Healthy = false;
        _logger.LogWarning("Novo status = Unhealthy");
        return GetCurrentStatusApplication();
    }

    private StatusApi GetCurrentStatusApplication()
    {
        return new StatusApi
        {
            Producer = Environment.MachineName,
            Healthy = Healthy,
            Mensagem = Healthy ? "OK" : "Unhealthy"
        };
    }
}