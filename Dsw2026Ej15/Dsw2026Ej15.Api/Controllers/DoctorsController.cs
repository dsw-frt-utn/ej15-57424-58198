using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if(speciality is null)
        {
            return BadRequest("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.AddDoctor(doctor);
    
        return Created();
    }

    [HttpGet("doctors")]
    public IActionResult GetDoctors()
    {
        var doctors = _persistence
            .GetAllDoctor()
            .Where(d => d.IsActive);

        return Ok(doctors);
    }

    [HttpGet("doctors/{id}")]
    public IActionResult GetDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctor(id);

        if (doctor is null || !doctor.IsActive)
            return NotFound();

        var response = new DoctorModel.Response(
            doctor.Name,
            doctor.LicenseNumber,
            doctor.Speciality!.Name
        );

        return Ok(response);
    }

    [HttpDelete("doctors/{id}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctor(id);

        if (doctor is null || !doctor.IsActive)
            return NotFound();

        _persistence.DeleteDoctor(id);

        return NoContent();
    }
}