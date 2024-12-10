using LearningPlatform.Models;
using Microsoft.Data.Sqlite;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Interfaces;

namespace LearningPlatform.Repositories;

internal class Repo : BaseRepository<User, SqliteConnection>
{
    public Repo(string connectionString) : base(connectionString)
    {
    }

    public Repo(string connectionString, int? commandTimeout) : base(connectionString, commandTimeout)
    {
    }

    public Repo(string connectionString, ICache cache) : base(connectionString, cache)
    {
    }

    public Repo(string connectionString, ITrace trace) : base(connectionString, trace)
    {
    }

    public Repo(string connectionString, IStatementBuilder statementBuilder) : base(connectionString, statementBuilder)
    {
    }

    public Repo(string connectionString, ConnectionPersistency connectionPersistency) : base(connectionString, connectionPersistency)
    {
    }

    public Repo(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration) : base(connectionString, commandTimeout, cache, cacheItemExpiration)
    {
    }

    public Repo(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace)
    {
    }

    public Repo(string connectionString, int? commandTimeout, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, cache, cacheItemExpiration, trace, statementBuilder)
    {
    }

    public Repo(string connectionString, int? commandTimeout, ConnectionPersistency connectionPersistency, ICache cache, int? cacheItemExpiration, ITrace trace = null, IStatementBuilder statementBuilder = null) : base(connectionString, commandTimeout, connectionPersistency, cache, cacheItemExpiration, trace, statementBuilder)
    {
    }
}