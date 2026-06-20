namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; init; }
    public string LicenseNumber { get; init; }
    public bool IsActive { get; private set; }
    public Speciality? Speciality { get; private set; }

    public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        Speciality = speciality;
        IsActive = true;
    }
}