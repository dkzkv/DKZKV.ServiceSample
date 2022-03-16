namespace DKZKV.MandatoryOptions;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MandatoryOptionsAttribute : Attribute
{
    public MandatoryOptionsAttribute(string section)
    {
        Section = section ?? throw new ArgumentNullException(nameof(section));
    }

    public string Section { get; }
}