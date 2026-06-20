using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
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
    public IActionResult CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("El número de matrícula es requerido.");

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
            throw new ValidationException("La especialidad indicada no existe.");

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.AddDoctor(doctor);

        var response = new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, speciality.Name);

        return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, response);
    }

    [HttpGet("doctors")]
    public IActionResult GetDoctors()
    {
        var doctors = _persistence
            .GetAllDoctor()
            .Where(d => d.IsActive)
            .Select(d => new DoctorModel.Response(d.Name, d.LicenseNumber, d.Speciality!.Name));

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