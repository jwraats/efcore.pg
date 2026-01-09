using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Npgsql-specific extension methods for <see cref="TableBuilder" />.
/// </summary>
/// <remarks>
///     <para>
///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
///         for more information on temporal constraints.
///     </para>
/// </remarks>
public static class NpgsqlTableBuilderExtensions
{
    #region IsTemporal

    /// <summary>
    ///     Configures the table as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints. This is different from SQL Server's temporal tables which use system-versioning
    ///         and history tables.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <param name="tableBuilder">The builder for the table being configured.</param>
    /// <param name="temporal">A value indicating whether the table is temporal.</param>
    /// <returns>An object that can be used to configure the temporal table.</returns>
    public static NpgsqlTemporalTableBuilder IsTemporal(
        this TableBuilder tableBuilder,
        bool temporal = true)
    {
        tableBuilder.Metadata.SetIsTemporal(temporal);

        return new NpgsqlTemporalTableBuilder(tableBuilder.GetInfrastructure());
    }

    /// <summary>
    ///     Configures the table as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints. This is different from SQL Server's temporal tables which use system-versioning
    ///         and history tables.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <param name="tableBuilder">The builder for the table being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the temporal table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static TableBuilder IsTemporal(
        this TableBuilder tableBuilder,
        Action<NpgsqlTemporalTableBuilder> buildAction)
    {
        tableBuilder.Metadata.SetIsTemporal(true);

        buildAction(new NpgsqlTemporalTableBuilder(tableBuilder.GetInfrastructure()));

        return tableBuilder;
    }

    /// <summary>
    ///     Configures the table as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints. This is different from SQL Server's temporal tables which use system-versioning
    ///         and history tables.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="tableBuilder">The builder for the table being configured.</param>
    /// <param name="temporal">A value indicating whether the table is temporal.</param>
    /// <returns>An object that can be used to configure the temporal table.</returns>
    public static NpgsqlTemporalTableBuilder<TEntity> IsTemporal<TEntity>(
        this TableBuilder<TEntity> tableBuilder,
        bool temporal = true)
        where TEntity : class
    {
        tableBuilder.Metadata.SetIsTemporal(temporal);

        return new NpgsqlTemporalTableBuilder<TEntity>(tableBuilder.GetInfrastructure<EntityTypeBuilder<TEntity>>());
    }

    /// <summary>
    ///     Configures the table as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints. This is different from SQL Server's temporal tables which use system-versioning
    ///         and history tables.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="tableBuilder">The builder for the table being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the temporal table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static TableBuilder<TEntity> IsTemporal<TEntity>(
        this TableBuilder<TEntity> tableBuilder,
        Action<NpgsqlTemporalTableBuilder<TEntity>> buildAction)
        where TEntity : class
    {
        tableBuilder.Metadata.SetIsTemporal(true);
        buildAction(new NpgsqlTemporalTableBuilder<TEntity>(tableBuilder.GetInfrastructure<EntityTypeBuilder<TEntity>>()));

        return tableBuilder;
    }

    #endregion IsTemporal
}
