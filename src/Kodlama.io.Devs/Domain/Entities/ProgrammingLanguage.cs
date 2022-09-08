using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Technology>? Technologies { get; set; }

    public ProgrammingLanguage() 
    {
    }

    public ProgrammingLanguage(int id, string name, bool isActive) : this()
    {
        Id = id;
        Name = name;
        IsActive = isActive;

    }
}
