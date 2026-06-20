using Dsw2026Ej15.Domain.Entities;
namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    void AddDoctor(Doctor doctor);
    void DeleteDoctor(Guid idDoctor);
    Doctor? GetDoctor(Guid idDoctor);
    List<Doctor> GetAllDoctor();
    Speciality? GetSpecialityById(Guid id);
}