namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; private set; }
    public string LicenseNumber { get; private set; }
    public bool IsActive { get; private set; }
    public Speciality? Speciality { get; private set; }

    private Doctor()
    {
        Name = string.Empty;
        LicenseNumber = string.Empty;
    }

    public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        Speciality = speciality;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
}