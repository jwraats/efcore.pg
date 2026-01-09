using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Npgsql-specific extension methods for <see cref="EntityTypeBuilder" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see>.
/// </remarks>
public static class NpgsqlEntityTypeBuilderExtensions
{
    #region Generated tsvector column

    // Note: actual configuration for generated TsVector properties is on the property

    /// <summary>
    ///     Configures a property on this entity to be a full-text search tsvector column over other given properties.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity being configured.</param>
    /// <param name="tsVectorPropertyExpression">
    ///     A lambda expression representing the property to be configured as a tsvector column
    ///     (<c>blog => blog.Url</c>).
    /// </param>
    /// <param name="config">
    ///     <para>
    ///         The text search configuration for this generated tsvector property, or <c>null</c> if this is not a
    ///         generated tsvector property.
    ///     </para>
    ///     <para>
    ///         See https://www.postgresql.org/docs/current/textsearch-controls.html for more information.
    ///     </para>
    /// </param>
    /// <param name="includeExpression">
    ///     <para>
    ///         A lambda expression representing the property(s) to be included in the tsvector column
    ///         (<c>blog => blog.Url</c>).
    ///     </para>
    ///     <para>
    ///         If multiple properties are to be included then specify an anonymous type including the
    ///         properties (<c>post => new { post.Title, post.BlogId }</c>).
    ///     </para>
    /// </param>
    /// <returns>A builder to further configure the property.</returns>
    public static EntityTypeBuilder<TEntity> HasGeneratedTsVectorColumn<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        Expression<Func<TEntity, NpgsqlTsVector>> tsVectorPropertyExpression,
        string config,
        Expression<Func<TEntity, object>> includeExpression)
        where TEntity : class
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
        Check.NotNull(tsVectorPropertyExpression, nameof(tsVectorPropertyExpression));
        Check.NotNull(config, nameof(config));
        Check.NotNull(includeExpression, nameof(includeExpression));

        entityTypeBuilder.Property(tsVectorPropertyExpression).IsGeneratedTsVectorColumn(
            config,
            includeExpression.GetPropertyAccessList().Select(EntityFrameworkMemberInfoExtensions.GetSimpleMemberName).ToArray());

        return entityTypeBuilder;
    }

    #endregion Generated tsvector column

    #region Storage parameters

    /// <summary>
    ///     Sets a PostgreSQL storage parameter on the table created for this entity.
    /// </summary>
    /// <remarks>
    ///     See https://www.postgresql.org/docs/current/static/sql-createtable.html#SQL-CREATETABLE-STORAGE-PARAMETERS
    /// </remarks>
    /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
    /// <param name="parameterName"> The name of the storage parameter. </param>
    /// <param name="parameterValue"> The value of the storage parameter. </param>
    /// <returns> The same builder instance so that multiple calls can be chained. </returns>
    public static EntityTypeBuilder HasStorageParameter(
        this EntityTypeBuilder entityTypeBuilder,
        string parameterName,
        object? parameterValue)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

        entityTypeBuilder.Metadata.SetStorageParameter(parameterName, parameterValue);

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Sets a PostgreSQL storage parameter on the table created for this entity.
    /// </summary>
    /// <remarks>
    ///     See https://www.postgresql.org/docs/current/static/sql-createtable.html#SQL-CREATETABLE-STORAGE-PARAMETERS
    /// </remarks>
    /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
    /// <param name="parameterName"> The name of the storage parameter. </param>
    /// <param name="parameterValue"> The value of the storage parameter. </param>
    /// <returns> The same builder instance so that multiple calls can be chained. </returns>
    public static EntityTypeBuilder<TEntity> HasStorageParameter<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        string parameterName,
        object? parameterValue)
        where TEntity : class
        => (EntityTypeBuilder<TEntity>)HasStorageParameter((EntityTypeBuilder)entityTypeBuilder, parameterName, parameterValue);

    /// <summary>
    ///     Sets a PostgreSQL storage parameter on the table created for this entity.
    /// </summary>
    /// <remarks>
    ///     See https://www.postgresql.org/docs/current/static/sql-createtable.html#SQL-CREATETABLE-STORAGE-PARAMETERS
    /// </remarks>
    /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
    /// <param name="parameterName"> The name of the storage parameter. </param>
    /// <param name="parameterValue"> The value of the storage parameter. </param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns> The same builder instance so that multiple calls can be chained. </returns>
    public static IConventionEntityTypeBuilder? HasStorageParameter(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        string parameterName,
        object? parameterValue,
        bool fromDataAnnotation = false)
    {
        if (entityTypeBuilder.CanSetStorageParameter(parameterName, parameterValue, fromDataAnnotation))
        {
            entityTypeBuilder.Metadata.SetStorageParameter(parameterName, parameterValue);

            return entityTypeBuilder;
        }

        return null;
    }

    /// <summary>
    ///     Returns a value indicating whether the PostgreSQL storage parameter is set on the table created for this entity.
    /// </summary>
    /// <remarks>
    ///     See https://www.postgresql.org/docs/current/static/sql-createtable.html#SQL-CREATETABLE-STORAGE-PARAMETERS
    /// </remarks>
    /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
    /// <param name="parameterName"> The name of the storage parameter. </param>
    /// <param name="parameterValue"> The value of the storage parameter. </param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns><c>true</c> if the mapped table can be configured as with the storage parameter.</returns>
    public static bool CanSetStorageParameter(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        string parameterName,
        object? parameterValue,
        bool fromDataAnnotation = false)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

        return entityTypeBuilder.CanSetAnnotation(
            NpgsqlAnnotationNames.StorageParameterPrefix + parameterName, parameterValue, fromDataAnnotation);
    }

    #endregion Storage parameters

    #region Unlogged Table

    /// <summary>
    ///     Configures the entity to use an unlogged table when targeting Npgsql.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="unlogged">True to configure the entity to use an unlogged table; otherwise, false.</param>
    /// <returns>
    ///     The same builder instance so that multiple calls can be chained.
    /// </returns>
    /// <remarks>
    ///     See: https://www.postgresql.org/docs/current/sql-createtable.html#SQL-CREATETABLE-UNLOGGED
    /// </remarks>
    public static EntityTypeBuilder IsUnlogged(
        this EntityTypeBuilder entityTypeBuilder,
        bool unlogged = true)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

        entityTypeBuilder.Metadata.SetIsUnlogged(unlogged);

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the mapped table to use an unlogged table when targeting Npgsql.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="unlogged">True to configure the entity to use an unlogged table; otherwise, false.</param>
    /// <returns>
    ///     The same builder instance so that multiple calls can be chained.
    /// </returns>
    /// <remarks>
    ///     See: https://www.postgresql.org/docs/current/sql-createtable.html#SQL-CREATETABLE-UNLOGGED
    /// </remarks>
    public static EntityTypeBuilder<TEntity> IsUnlogged<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        bool unlogged = true)
        where TEntity : class
        => (EntityTypeBuilder<TEntity>)IsUnlogged((EntityTypeBuilder)entityTypeBuilder, unlogged);

    /// <summary>
    ///     Configures the mapped table to use an unlogged table when targeting Npgsql.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="unlogged">True to configure the entity to use an unlogged table; otherwise, false.</param>
    /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
    /// <returns>
    ///     The same builder instance so that multiple calls can be chained.
    /// </returns>
    /// <remarks>
    ///     See: https://www.postgresql.org/docs/current/sql-createtable.html#SQL-CREATETABLE-UNLOGGED
    /// </remarks>
    public static IConventionEntityTypeBuilder? IsUnlogged(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool unlogged = true,
        bool fromDataAnnotation = false)
    {
        if (entityTypeBuilder.CanSetIsUnlogged(unlogged, fromDataAnnotation))
        {
            entityTypeBuilder.Metadata.SetIsUnlogged(unlogged, fromDataAnnotation);

            return entityTypeBuilder;
        }

        return null;
    }

    /// <summary>
    ///     Returns a value indicating whether the mapped table can be configured to use an unlogged table when targeting Npgsql.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="unlogged">True to configure the entity to use an unlogged table; otherwise, false.</param>
    /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
    /// <returns>
    ///     The same builder instance so that multiple calls can be chained.
    /// </returns>
    /// <remarks>
    ///     See: https://www.postgresql.org/docs/current/sql-createtable.html#SQL-CREATETABLE-UNLOGGED
    /// </remarks>
    public static bool CanSetIsUnlogged(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool unlogged = true,
        bool fromDataAnnotation = false)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

        return entityTypeBuilder.CanSetAnnotation(NpgsqlAnnotationNames.UnloggedTable, unlogged, fromDataAnnotation);
    }

    #endregion

    #region CockroachDB Interleave-in-parent

    /// <summary>
    ///     Specifies that the CockroachDB-specific "interleave in parent" feature should be used.
    /// </summary>
    public static EntityTypeBuilder UseCockroachDbInterleaveInParent(
        this EntityTypeBuilder entityTypeBuilder,
        Type parentTableType,
        List<string> interleavePrefix)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
        Check.NotNull(parentTableType, nameof(parentTableType));
        Check.NotNull(interleavePrefix, nameof(interleavePrefix));

        var parentEntity = entityTypeBuilder.Metadata.Model.FindEntityType(parentTableType);
        if (parentEntity is null)
        {
            throw new ArgumentException($"Entity not found in model for type: {parentEntity}", nameof(parentTableType));
        }

        if (StoreObjectIdentifier.Create(parentEntity, StoreObjectType.Table) is not { } tableIdentifier)
        {
            throw new ArgumentException($"Entity {parentEntity.DisplayName()} is not mapped to a database table");
        }

        var interleaveInParent = entityTypeBuilder.Metadata.GetCockroachDbInterleaveInParent();
        interleaveInParent.ParentTableSchema = tableIdentifier.Schema;
        interleaveInParent.ParentTableName = tableIdentifier.Name;
        interleaveInParent.InterleavePrefix = interleavePrefix;

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Specifies that the CockroachDB-specific "interleave in parent" feature should be used.
    /// </summary>
    public static EntityTypeBuilder<TEntity> UseCockroachDbInterleaveInParent<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        Type parentTableType,
        List<string> interleavePrefix)
        where TEntity : class
        => (EntityTypeBuilder<TEntity>)UseCockroachDbInterleaveInParent(
            (EntityTypeBuilder)entityTypeBuilder, parentTableType, interleavePrefix);

    #endregion CockroachDB Interleave-in-parent

    #region Temporal tables

    /// <summary>
    ///     Configures the entity type to use a temporal table when targeting PostgreSQL.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="temporal">A value indicating whether the entity type is mapped to a temporal table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    /// <remarks>
    ///     See <see href="https://www.postgresql.org/docs/current/sql-createtable.html">PostgreSQL temporal tables</see> for more information.
    /// </remarks>
    public static EntityTypeBuilder IsTemporal(
        this EntityTypeBuilder entityTypeBuilder,
        bool temporal = true)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

        entityTypeBuilder.Metadata.SetIsTemporal(temporal);

        if (temporal)
        {
            // Set default period property names if not already set
            if (entityTypeBuilder.Metadata.GetTemporalPeriodStartPropertyName() == null)
            {
                entityTypeBuilder.Metadata.SetTemporalPeriodStartPropertyName("PeriodStart");
            }

            if (entityTypeBuilder.Metadata.GetTemporalPeriodEndPropertyName() == null)
            {
                entityTypeBuilder.Metadata.SetTemporalPeriodEndPropertyName("PeriodEnd");
            }

            // Set default history table name if not already set
            if (entityTypeBuilder.Metadata.GetTemporalHistoryTableName() == null)
            {
                var tableName = entityTypeBuilder.Metadata.GetTableName();
                entityTypeBuilder.Metadata.SetTemporalHistoryTableName(tableName + "History");
            }
        }

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the entity type to use a temporal table when targeting PostgreSQL.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="temporal">A value indicating whether the entity type is mapped to a temporal table.</param>
    /// <param name="buildAction">An action that performs configuration of the temporal table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    /// <remarks>
    ///     See <see href="https://www.postgresql.org/docs/current/sql-createtable.html">PostgreSQL temporal tables</see> for more information.
    /// </remarks>
    public static EntityTypeBuilder IsTemporal(
        this EntityTypeBuilder entityTypeBuilder,
        bool temporal,
        Action<TemporalTableBuilder> buildAction)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
        Check.NotNull(buildAction, nameof(buildAction));

        entityTypeBuilder.IsTemporal(temporal);

        if (temporal)
        {
            buildAction(new TemporalTableBuilder(entityTypeBuilder));
        }

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the entity type to use a temporal table when targeting PostgreSQL.
    /// </summary>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="temporal">A value indicating whether the entity type is mapped to a temporal table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    /// <remarks>
    ///     See <see href="https://www.postgresql.org/docs/current/sql-createtable.html">PostgreSQL temporal tables</see> for more information.
    /// </remarks>
    public static EntityTypeBuilder<TEntity> IsTemporal<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        bool temporal = true)
        where TEntity : class
        => (EntityTypeBuilder<TEntity>)IsTemporal((EntityTypeBuilder)entityTypeBuilder, temporal);

    /// <summary>
    ///     Configures the entity type to use a temporal table when targeting PostgreSQL.
    /// </summary>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="temporal">A value indicating whether the entity type is mapped to a temporal table.</param>
    /// <param name="buildAction">An action that performs configuration of the temporal table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    /// <remarks>
    ///     See <see href="https://www.postgresql.org/docs/current/sql-createtable.html">PostgreSQL temporal tables</see> for more information.
    /// </remarks>
    public static EntityTypeBuilder<TEntity> IsTemporal<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        bool temporal,
        Action<TemporalTableBuilder> buildAction)
        where TEntity : class
        => (EntityTypeBuilder<TEntity>)IsTemporal((EntityTypeBuilder)entityTypeBuilder, temporal, buildAction);

    /// <summary>
    ///     Configures the entity type to use a temporal table when targeting PostgreSQL.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="temporal">A value indicating whether the entity type is mapped to a temporal table.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The same builder instance if the configuration was applied; <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionEntityTypeBuilder? IsTemporal(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool temporal = true,
        bool fromDataAnnotation = false)
    {
        if (entityTypeBuilder.CanSetIsTemporal(temporal, fromDataAnnotation))
        {
            entityTypeBuilder.Metadata.SetIsTemporal(temporal, fromDataAnnotation);

            if (temporal)
            {
                // Set default period property names if not already set
                if (entityTypeBuilder.Metadata.GetTemporalPeriodStartPropertyName() == null)
                {
                    entityTypeBuilder.Metadata.SetTemporalPeriodStartPropertyName("PeriodStart", fromDataAnnotation);
                }

                if (entityTypeBuilder.Metadata.GetTemporalPeriodEndPropertyName() == null)
                {
                    entityTypeBuilder.Metadata.SetTemporalPeriodEndPropertyName("PeriodEnd", fromDataAnnotation);
                }

                // Set default history table name if not already set
                if (entityTypeBuilder.Metadata.GetTemporalHistoryTableName() == null)
                {
                    var tableName = entityTypeBuilder.Metadata.GetTableName();
                    entityTypeBuilder.Metadata.SetTemporalHistoryTableName(tableName + "History", fromDataAnnotation);
                }
            }

            return entityTypeBuilder;
        }

        return null;
    }

    /// <summary>
    ///     Returns a value indicating whether the entity type can be configured as a temporal table.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="temporal">A value indicating whether the entity type is mapped to a temporal table.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns><see langword="true"/> if the entity type can be configured as temporal.</returns>
    public static bool CanSetIsTemporal(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool temporal = true,
        bool fromDataAnnotation = false)
    {
        Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));

        return entityTypeBuilder.CanSetAnnotation(NpgsqlAnnotationNames.IsTemporal, temporal, fromDataAnnotation);
    }

    #endregion Temporal tables
}

/// <summary>
///     Allows configuration of a temporal table.
/// </summary>
public class TemporalTableBuilder
{
    private readonly EntityTypeBuilder _entityTypeBuilder;

    /// <summary>
    ///     Creates a new instance of <see cref="TemporalTableBuilder"/>.
    /// </summary>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    public TemporalTableBuilder(EntityTypeBuilder entityTypeBuilder)
    {
        _entityTypeBuilder = entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the history table name and schema.
    /// </summary>
    /// <param name="name">The name of the history table.</param>
    /// <param name="schema">The schema of the history table.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public TemporalTableBuilder UseHistoryTable(string name, string? schema = null)
    {
        Check.NotNull(name, nameof(name));

        _entityTypeBuilder.Metadata.SetTemporalHistoryTableName(name);
        _entityTypeBuilder.Metadata.SetTemporalHistoryTableSchema(schema);

        return this;
    }

    /// <summary>
    ///     Configures the period start and end column names.
    /// </summary>
    /// <param name="periodStartPropertyName">The name of the period start property.</param>
    /// <param name="periodEndPropertyName">The name of the period end property.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public TemporalTableBuilder HasPeriod(string periodStartPropertyName, string periodEndPropertyName)
    {
        Check.NotNull(periodStartPropertyName, nameof(periodStartPropertyName));
        Check.NotNull(periodEndPropertyName, nameof(periodEndPropertyName));

        _entityTypeBuilder.Metadata.SetTemporalPeriodStartPropertyName(periodStartPropertyName);
        _entityTypeBuilder.Metadata.SetTemporalPeriodEndPropertyName(periodEndPropertyName);

        return this;
    }
}
