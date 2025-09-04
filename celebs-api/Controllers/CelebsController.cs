using CelebApi.Models;
using CelebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CelebApi.Controllers;

[ApiController]
[Route("api/celebs")]
public class CelebsController(CelebService service) : ControllerBase
{
    private readonly CelebService _service = service;

    [HttpGet("v1/celebs")]
    public ActionResult<List<Celeb>> GetAll()
    {
        Console.WriteLine("[Controller: GetAll] 📥 Request received to get all celebs.");
        List<Celeb> result = _service.GetAll();
        Console.WriteLine($"[Controller: GetAll] ✅ Returned {result.Count} celebs.");
        return Ok(result);
    }

    [HttpGet("v1/{id}")]
    public ActionResult<Celeb> GetById(Guid id)
    {
        Console.WriteLine($"[Controller: GetById] Request received to get celeb by ID: {id}");
        Celeb celeb = _service.GetById(id);
        Console.WriteLine($"[Controller: GetById] Celeb retrieved: {celeb.Name}");
        return Ok(celeb);
    }

    [HttpPost("v1/celeb")]
    public ActionResult<Celeb> Add([FromBody] CelebCreateDto celebToAdd)
    {
        Console.WriteLine("[Controller: Add] Request received to add a new celeb.");
        Celeb addedCeleb = _service.Add(celebToAdd);
        Console.WriteLine($"[Controller: Add] Celeb added: {celebToAdd.Name} ({addedCeleb.Id})");

        return Ok(addedCeleb);
    }

    [HttpPut("v1/celeb/{id}")]
    public ActionResult<Celeb> Update(Guid id, [FromBody] CelebUpdateDto updated)
    {
        Console.WriteLine($"[Controller: Update] Request received to update celeb with ID: {id}");
        Celeb updatedCeleb = _service.Update(id, updated);
        Console.WriteLine($"[Controller: Update] Update attempted for celeb ID: {id}");

        return Ok(updatedCeleb);
    }

    [HttpDelete("v1/celeb/{id}")]
    public IActionResult Delete(Guid id)
    {
        Console.WriteLine($"[Controller: Delete] Request received to delete celeb with ID: {id}");
        _service.Delete(id);
        Console.WriteLine($"[Controller: Delete] Delete attempted for celeb ID: {id}");
        return Ok();
    }

    [HttpPost("v1/reset")]
    public IActionResult Reset()
    {
        Console.WriteLine("[Controller: Reset] 📥 Request received to reset data.");
        _service.Reset();
        Console.WriteLine("[Controller: Reset] ✅ Data reset to original state.");
        return Ok();
    }
}
