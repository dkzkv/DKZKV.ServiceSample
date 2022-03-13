namespace DKZKV.MandatoryOptions;

public class OptionsRegisterException : Exception
{
    /// <summary>
    ///     Exception with cause of options problems
    /// </summary>
    /// <param name="message"></param>
    public OptionsRegisterException(string message) : base(message)
    {
    }
}