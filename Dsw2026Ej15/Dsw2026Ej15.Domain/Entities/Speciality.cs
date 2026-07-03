namespace Dsw2026Ej15.Domain.Entities;

public class Speciality : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Speciality()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public Speciality(string name, string description, Guid? id = null) : base(id)
    {
        Name = name;
        Description = description;
    }
}