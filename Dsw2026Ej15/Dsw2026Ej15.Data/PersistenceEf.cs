using Dsw2026Ej15.Data.Context;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly ApplicationDbContext _context;

    public PersistenceEf(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public void DeleteDoctor(Guid idDoctor)
    {
        var doctor = GetDoctor(idDoctor);

        if (doctor is null)
            return;

        doctor.Deactivate();

        _context.Doctors.Update(doctor);
        _context.SaveChanges();
    }

    public Doctor? GetDoctor(Guid idDoctor)
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == idDoctor);
    }

    public List<Doctor> GetAllDoctor()
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .ToList();
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _context.Specialities
            .FirstOrDefault(s => s.Id == id);
    }
}