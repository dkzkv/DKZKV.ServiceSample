using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DKZKV.BookStore.ComponentTests.Infrastructure.Containers;
using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Services;

namespace DKZKV.BookStore.ComponentTests.Infrastructure;

public class BookStoreInfrastructureConfigurator
{
    private readonly ICollection<ContainerBase> _containers;

    public BookStoreInfrastructureConfigurator()
    {
        RemoveAllContainers();
        _containers = new List<ContainerBase>
        {
            new SqlServerDbContainer()
        };
        Thread.Sleep(5000);
    }

    public void Configure()
    {
        foreach (var container in _containers)
            container.SetupEnvironmentVariables();
    }

    public void Dispose()
    {
        RemoveAllContainers();
    }

    private void RemoveAllContainers()
    {
        var hosts = new Hosts().Discover();
        var docker = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.First(x => x.Name == "default");
        foreach (var container in docker.Host.InspectContainers().Data.Where(x => x.Name.StartsWith(ConfigurationConstants.NamePrefix)))
            docker.Host.RemoveContainer(container.Id, true, true);
    }
}