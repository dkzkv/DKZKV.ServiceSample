using System.Collections;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;

namespace DKZKV.MandatoryOptions;

/// <summary>
///     Register mandatory options
/// </summary>
public static class OptionsRegisterExtension
{
    /// <summary>
    ///     Scan current assembly for properties with attribute cref="MandatoryOptionsAttribute" and register them.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="OptionsRegisterException"></exception>
    public static void ConfigureMandatoryOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.ConfigureMandatoryOptions(configuration, assemblies);
    }

    /// <summary>
    ///     Scan assemblies for properties with attribute cref="MandatoryOptionsAttribute" and register them.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="assemblies"></param>
    /// <exception cref="OptionsRegisterException"></exception>
    public static void ConfigureMandatoryOptions(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        var options = assemblies.SelectMany(o => o.FindOptions()).ToArray();
        ValidateSections(options);
        ValidateMandatoryProperties(options, configuration);

        foreach (var option in options) services.ConfigureOption(configuration, option);
    }

    private static IEnumerable<(Type Type, string Section)> FindOptions(this Assembly assembly)
    {
        var candleStickOptions = assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(MandatoryOptionsAttribute)));
        return candleStickOptions.Select(o => (o, o.GetCustomAttribute<MandatoryOptionsAttribute>()?.Section ?? string.Empty));
    }

    private static void ValidateSections((Type Type, string Section)[] options)
    {
        var emptySectionOption = options.Where(o => string.IsNullOrEmpty(o.Section)).ToArray();
        if (emptySectionOption.Any())
            throw new OptionsRegisterException(
                $"Section name is null or empty for {emptySectionOption.Select(o => o.Type.Name).Aggregate((prev, next) => $"{prev}, {next}")}");

        var similarSections = options.GroupBy(o => o.Section).Where(o => o.Count() > 1).ToArray();
        if (similarSections.Any())
            throw new OptionsRegisterException(
                $"Similar name for sections (duplicates): {similarSections.Select(o => o.Key).Aggregate((prev, next) => $"{prev}, {next}")}");
    }

    private static void ValidateMandatoryProperties((Type Type, string Section)[] options, IConfiguration configuration)
    {
        var mandatoryProperties = options.SelectMany(o => o.Type.GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(OptionalInEnvironmentVariableAttribute))))
            .Select(o => $"{o.ReflectedType?.Name}:{o.Name}".ToUpper()).ToArray();

        var root = configuration as IConfigurationRoot;

        var environmentVariablesProvider = root?.Providers.FirstOrDefault(o => o.GetType() == typeof(EnvironmentVariablesConfigurationProvider))
                                           ?? throw new InvalidOperationException("EnvironmentVariables provider not supported.");

        var missedMandatoryProperties = new List<string>();
        foreach (var mandatoryProperty in mandatoryProperties)
            if (!environmentVariablesProvider.TryGet(mandatoryProperty, out _))
                missedMandatoryProperties.Add(mandatoryProperty);

        if (missedMandatoryProperties.Any())
            throw new MandatoryOptionMissedException(missedMandatoryProperties.ToArray());
    }

    private static void ConfigureOption(this IEnumerable services, IConfiguration configuration, (Type Type, string Section) option)
    {
        var section = configuration.GetSection(option.Section);
        var extensionType = typeof(OptionsConfigurationServiceCollectionExtensions);
        const string methodName = nameof(OptionsConfigurationServiceCollectionExtensions.Configure);

        extensionType.GetMethods()
            .Where(c => c.Name == methodName)
            .First(c =>
            {
                var parameters = c.GetParameters()
                    .ToArray();

                return parameters.Length == 2 &&
                       parameters[0].ParameterType == typeof(IServiceCollection) &&
                       parameters[1].ParameterType == typeof(IConfiguration);
            })
            .MakeGenericMethod(option.Type)
            .Invoke(null, new object[] { services, section });
    }
}