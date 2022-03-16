namespace DKZKV.MandatoryOptions;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class OptionalInEnvironmentVariableAttribute : Attribute
{
}