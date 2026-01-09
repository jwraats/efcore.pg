using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Extension methods for <see cref="IEntityType" /> for Npgsql-specific metadata.
/// </summary>
public static class NpgsqlEntityTypeExtensions
{
    #region Storage parameters

    /// <summary>
    ///     Gets all storage parameters for the table mapped to the entity type.
    /// </summary>
    public static Dictionary<string, object?> GetStorageParameters(this IReadOnlyEntityType entityType)
        => entityType.GetAnnotations()
            .Where(a => a.Name.StartsWith(NpgsqlAnnotationNames.StorageParameterPrefix, StringComparison.Ordinal))
            .ToDictionary(
                a => a.Name.Substring(NpgsqlAnnotationNames.StorageParameterPrefix.Length),
                a => a.Value);

    /// <summary>
    ///     Gets a storage parameter for the table mapped to the entity type.
    /// </summary>
    public static string? GetStorageParameter(this IEntityType entityType, string parameterName)
    {
        Check.NotEmpty(parameterName, nameof(parameterName));

        return (string?)entityType[NpgsqlAnnotationNames.StorageParameterPrefix + parameterName];
    }

    /// <summary>
    ///     Sets a storage parameter on the table mapped to the entity type.
    /// </summary>
    public static void SetStorageParameter(this IMutableEntityType entityType, string parameterName, object? parameterValue)
    {
        Check.NotEmpty(parameterName, nameof(parameterName));

        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.StorageParameterPrefix + parameterName, parameterValue);
    }

    /// <summary>
    ///     Sets a storage parameter on the table mapped to the entity type.
    /// </summary>
    public static object SetStorageParameter(
        this IConventionEntityType entityType,
        string parameterName,
        object? parameterValue,
        bool fromDataAnnotation = false)
    {
        Check.NotEmpty(parameterName, nameof(parameterName));

        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.StorageParameterPrefix + parameterName, parameterValue, fromDataAnnotation);

        return parameterName;
    }

    /// <summary>
    ///     Gets the configuration source for a storage parameter for the table mapped to the entity type.
    /// </summary>
    public static ConfigurationSource? GetStorageParameterConfigurationSource(
        this IConventionEntityType index,
        string parameterName)
    {
        Check.NotEmpty(parameterName, nameof(parameterName));

        return index.FindAnnotation(NpgsqlAnnotationNames.StorageParameterPrefix + parameterName)?.GetConfigurationSource();
    }

    #endregion Storage parameters

    #region Unlogged

    /// <summary>
    ///     Gets whether the table to which the entity is mapped is unlogged.
    /// </summary>
    public static bool GetIsUnlogged(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.UnloggedTable] as bool? ?? false;

    /// <summary>
    ///     Sets whether the table to which the entity is mapped is unlogged.
    /// </summary>
    public static void SetIsUnlogged(this IMutableEntityType entityType, bool unlogged)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.UnloggedTable, unlogged);

    /// <summary>
    ///     Sets whether the table to which the entity is mapped is unlogged.
    /// </summary>
    public static bool SetIsUnlogged(
        this IConventionEntityType entityType,
        bool unlogged,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.UnloggedTable, unlogged, fromDataAnnotation);

        return unlogged;
    }

    /// <summary>
    ///     Gets the configuration source for whether the table to which the entity is mapped is unlogged.
    /// </summary>
    public static ConfigurationSource? GetIsUnloggedConfigurationSource(this IConventionEntityType index)
        => index.FindAnnotation(NpgsqlAnnotationNames.UnloggedTable)?.GetConfigurationSource();

    #endregion Unlogged

    #region CockroachDb interleave in parent

    /// <summary>
    ///     Gets the CockroachDB-specific interleave-in-parent setting for the table to which the entity is mapped.
    /// </summary>
    public static CockroachDbInterleaveInParent GetCockroachDbInterleaveInParent(this IReadOnlyEntityType entityType)
        => new(entityType);

    #endregion CockroachDb interleave in parent

    #region Temporal

    /// <summary>
    ///     Gets whether the table to which the entity is mapped is configured as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    public static bool GetIsTemporal(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.IsTemporal] as bool? ?? false;

    /// <summary>
    ///     Sets whether the table to which the entity is mapped is configured as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    public static void SetIsTemporal(this IMutableEntityType entityType, bool temporal)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.IsTemporal, temporal);

    /// <summary>
    ///     Sets whether the table to which the entity is mapped is configured as temporal.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         PostgreSQL 18+ supports temporal constraints using the WITHOUT OVERLAPS clause on primary keys
    ///         and unique constraints.
    ///     </para>
    ///     <para>
    ///         See <see href="https://www.postgresql.org/docs/18/sql-createtable.html">PostgreSQL 18 documentation</see>
    ///         for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    public static bool SetIsTemporal(
        this IConventionEntityType entityType,
        bool temporal,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.IsTemporal, temporal, fromDataAnnotation);
        return temporal;
    }

    /// <summary>
    ///     Gets the configuration source for whether the table to which the entity is mapped is temporal.
    /// </summary>
    public static ConfigurationSource? GetIsTemporalConfigurationSource(this IConventionEntityType entityType)
        => entityType.FindAnnotation(NpgsqlAnnotationNames.IsTemporal)?.GetConfigurationSource();

    /// <summary>
    ///     Gets the name of the temporal period column for the table to which the entity is mapped.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PostgreSQL, the period column is a range type column (e.g., tstzrange, daterange)
    ///         that represents the validity period of each row.
    ///     </para>
    /// </remarks>
    public static string? GetTemporalPeriodColumnName(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.TemporalPeriodColumnName] as string;

    /// <summary>
    ///     Sets the name of the temporal period column for the table to which the entity is mapped.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PostgreSQL, the period column is a range type column (e.g., tstzrange, daterange)
    ///         that represents the validity period of each row.
    ///     </para>
    /// </remarks>
    public static void SetTemporalPeriodColumnName(this IMutableEntityType entityType, string? periodColumnName)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalPeriodColumnName, periodColumnName);

    /// <summary>
    ///     Sets the name of the temporal period column for the table to which the entity is mapped.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PostgreSQL, the period column is a range type column (e.g., tstzrange, daterange)
    ///         that represents the validity period of each row.
    ///     </para>
    /// </remarks>
    public static string? SetTemporalPeriodColumnName(
        this IConventionEntityType entityType,
        string? periodColumnName,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalPeriodColumnName, periodColumnName, fromDataAnnotation);
        return periodColumnName;
    }

    /// <summary>
    ///     Gets the configuration source for the temporal period column name.
    /// </summary>
    public static ConfigurationSource? GetTemporalPeriodColumnNameConfigurationSource(this IConventionEntityType entityType)
        => entityType.FindAnnotation(NpgsqlAnnotationNames.TemporalPeriodColumnName)?.GetConfigurationSource();

    #endregion Temporal
}
