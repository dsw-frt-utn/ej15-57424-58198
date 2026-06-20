namespace Dsw2026Ej15.Api.Models
{
    internal record DoctorModel
    {
        internal record Request(string Name, string LicenseNumber, Guid SpecialityId);
    }
}