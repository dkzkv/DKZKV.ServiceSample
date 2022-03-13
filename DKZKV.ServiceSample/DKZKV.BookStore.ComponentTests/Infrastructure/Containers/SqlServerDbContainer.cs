using System;
using System.Collections.Generic;

namespace DKZKV.BookStore.ComponentTests.Infrastructure.Containers;

public class SqlServerDbContainer : ContainerBase
{
    public static string User = "sa";
    public static string Password = "yourStrong(!)Password";

    public SqlServerDbContainer()
        : base("sqlserver", "mcr.microsoft.com/mssql/server:2019-latest", 1433,
            "ACCEPT_EULA=Y",
            $"SA_PASSWORD={Password}",
            "MSSQL_PID=Express")
    {
    }

    public override void SetupEnvironmentVariables()
    {
        var port = GetOuterPort().ToString();
        var envToOverride = new Dictionary<string, string>
        {
            ["BOOKSTOREOPTIONS:DATABASECONNECTION"] =
                $"Server=.\\SQLEXPRESS,{port};Database=BookStoreBd;User Id={User};Password={Password};Enlist=False;"
        };

        foreach (var (envName, envValue) in envToOverride)
            Environment.SetEnvironmentVariable(envName, envValue);
    }
}