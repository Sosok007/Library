using Biblioteka.API.Models;
using Biblioteka.BLL;
using Biblioteka.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AvtorController : ControllerBase
{
    private readonly IAvtorService _avtorService;

    public AvtorController(IAvtorService avtorService)
    {
        _avtorService = avtorService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAvtors()
    {
        return Ok(await _avtorService.GetAvtorsAsync());
    }

    [HttpGet("find/{id:guid}")]
    public async Task<IActionResult> GetAvtor(Guid id)
    {
        var avtor = await _avtorService.GetAvtorByIdAsync(id);
        return Ok(avtor);
    }

    [Authorize("admin")]
    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> DeleteAvtor(Guid id)
    {
        await _avtorService.DeleteAvtorAsync(id);
        return NoContent();
    }

    [Authorize("admin")]
    [HttpPost("update/{id:guid}")]
    public async Task<IActionResult> UpdateAvtor([FromRoute] Guid id, [FromBody] CreatAvtor avtor)
    {
        await _avtorService.UpdateAvtorAsync(id, new Avtor
        {
            Firstname = avtor.Firstname, Lastname = avtor.Lastname, Patronymic = avtor.Patronymic
        });
        return Ok();
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateAvtor([FromBody] CreatAvtor avtor)
    {
        var result = await _avtorService.InsertAvtorAsync(new Avtor
        {
            Firstname = avtor.Firstname, Lastname = avtor.Lastname, Patronymic = avtor.Patronymic
        });
        return Ok(result);
    }
}