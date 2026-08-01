using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System.Linq;
using System.Text.Json;
namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private List<Speciality> _specialities = [];
    private List<Doctor> _doctors = [];

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        string jsonPath = Path.Combine(AppContext.BaseDirectory,
            "Source", "specialities.json");
        var json = File.ReadAllText(jsonPath);
        var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new JsonException("No se pudieron deserializar las especialidades.");

        _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
    }

    public void AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }

    public List<Doctor> GetAllDoctor()
    {
        return _doctors;
    }

    public Doctor? GetDoctor(Guid idDoctor)
    {
        return _doctors.FirstOrDefault(d => d.Id == idDoctor);
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _specialities.FirstOrDefault(s => s.Id == id);
    }

    public void DeleteDoctor(Guid idDoctor)
    {
        var doctor = GetDoctor(idDoctor);
        if(doctor is not null)
        {
            doctor.Deactivate();
        }
    }
}
