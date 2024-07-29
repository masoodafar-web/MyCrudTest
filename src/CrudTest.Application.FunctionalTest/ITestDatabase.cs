using System.Data.Common;

namespace CrudTest.Application.FunctionalTest;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
