// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     A builder for configuring PostgreSQL temporal table settings on an entity type.
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
public class NpgsqlTemporalTableBuilder
{
    private readonly EntityTypeBuilder _entityTypeBuilder;

    /// <summary>
    ///     Creates a new instance of <see cref="NpgsqlTemporalTableBuilder" />.
    /// </summary>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    public NpgsqlTemporalTableBuilder(EntityTypeBuilder entityTypeBuilder)
    {
        _entityTypeBuilder = entityTypeBuilder;
    }

    /// <summary>
    ///     Gets the entity type builder.
    /// </summary>
    protected virtual EntityTypeBuilder EntityTypeBuilder => _entityTypeBuilder;

    /// <summary>
    ///     Configures the period column name for the temporal table.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PostgreSQL, the period column is a range type column (e.g., tstzrange, daterange)
    ///         that represents the validity period of each row.
    ///     </para>
    /// </remarks>
    /// <param name="periodColumnName">The name of the period column.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public virtual NpgsqlTemporalTableBuilder HasPeriodColumnName(string periodColumnName)
    {
        _entityTypeBuilder.Metadata.SetTemporalPeriodColumnName(periodColumnName);
        return this;
    }
}

/// <summary>
///     A builder for configuring PostgreSQL temporal table settings on an entity type.
/// </summary>
/// <typeparam name="TEntity">The entity type being configured.</typeparam>
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
public class NpgsqlTemporalTableBuilder<TEntity> : NpgsqlTemporalTableBuilder
    where TEntity : class
{
    /// <summary>
    ///     Creates a new instance of <see cref="NpgsqlTemporalTableBuilder{TEntity}" />.
    /// </summary>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    public NpgsqlTemporalTableBuilder(EntityTypeBuilder<TEntity> entityTypeBuilder)
        : base(entityTypeBuilder)
    {
    }

    /// <summary>
    ///     Gets the entity type builder.
    /// </summary>
    protected new virtual EntityTypeBuilder<TEntity> EntityTypeBuilder
        => (EntityTypeBuilder<TEntity>)base.EntityTypeBuilder;

    /// <summary>
    ///     Configures the period column name for the temporal table.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PostgreSQL, the period column is a range type column (e.g., tstzrange, daterange)
    ///         that represents the validity period of each row.
    ///     </para>
    /// </remarks>
    /// <param name="periodColumnName">The name of the period column.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public new virtual NpgsqlTemporalTableBuilder<TEntity> HasPeriodColumnName(string periodColumnName)
    {
        base.HasPeriodColumnName(periodColumnName);
        return this;
    }
}
