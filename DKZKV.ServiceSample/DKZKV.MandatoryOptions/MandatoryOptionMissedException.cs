namespace DKZKV.MandatoryOptions;

public class MandatoryOptionMissedException : Exception
{
    /// <summary>
    /// </summary>
    /// <param name="mandatoryProperties"></param>
    public MandatoryOptionMissedException(string[] mandatoryProperties)
        : base(string.Concat("Missed mandatory properties: ", mandatoryProperties.Aggregate((prev, next) => $"{prev}, {next}")))
    {
    }
}