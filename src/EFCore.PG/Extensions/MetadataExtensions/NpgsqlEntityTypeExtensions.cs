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

    #region Temporal tables

    /// <summary>
    ///     Gets a value indicating whether the entity type is configured as a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns><see langword="true"/> if the entity type is configured as temporal.</returns>
    public static bool IsTemporal(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.IsTemporal] as bool? ?? false;

    /// <summary>
    ///     Sets a value indicating whether the entity type is configured as a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="temporal">A value indicating whether the entity type is temporal.</param>
    public static void SetIsTemporal(this IMutableEntityType entityType, bool temporal)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.IsTemporal, temporal);

    /// <summary>
    ///     Sets a value indicating whether the entity type is configured as a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="temporal">A value indicating whether the entity type is temporal.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    public static bool SetIsTemporal(
        this IConventionEntityType entityType,
        bool temporal,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.IsTemporal, temporal, fromDataAnnotation);
        return temporal;
    }

    /// <summary>
    ///     Gets the configuration source for <see cref="IsTemporal" />.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The configuration source for <see cref="IsTemporal" />.</returns>
    public static ConfigurationSource? GetIsTemporalConfigurationSource(this IConventionEntityType entityType)
        => entityType.FindAnnotation(NpgsqlAnnotationNames.IsTemporal)?.GetConfigurationSource();

    /// <summary>
    ///     Gets the name of the history table used for temporal data.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The name of the history table.</returns>
    public static string? GetTemporalHistoryTableName(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.TemporalHistoryTableName] as string;

    /// <summary>
    ///     Sets the name of the history table used for temporal data.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="historyTableName">The name of the history table.</param>
    public static void SetTemporalHistoryTableName(this IMutableEntityType entityType, string? historyTableName)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalHistoryTableName, historyTableName);

    /// <summary>
    ///     Sets the name of the history table used for temporal data.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="historyTableName">The name of the history table.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    public static string? SetTemporalHistoryTableName(
        this IConventionEntityType entityType,
        string? historyTableName,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalHistoryTableName, historyTableName, fromDataAnnotation);
        return historyTableName;
    }

    /// <summary>
    ///     Gets the schema of the history table used for temporal data.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The schema of the history table.</returns>
    public static string? GetTemporalHistoryTableSchema(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.TemporalHistoryTableSchema] as string;

    /// <summary>
    ///     Sets the schema of the history table used for temporal data.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="historyTableSchema">The schema of the history table.</param>
    public static void SetTemporalHistoryTableSchema(this IMutableEntityType entityType, string? historyTableSchema)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalHistoryTableSchema, historyTableSchema);

    /// <summary>
    ///     Sets the schema of the history table used for temporal data.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="historyTableSchema">The schema of the history table.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    public static string? SetTemporalHistoryTableSchema(
        this IConventionEntityType entityType,
        string? historyTableSchema,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalHistoryTableSchema, historyTableSchema, fromDataAnnotation);
        return historyTableSchema;
    }

    /// <summary>
    ///     Gets the name of the period start property.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The name of the period start property.</returns>
    public static string? GetTemporalPeriodStartPropertyName(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.TemporalPeriodStartPropertyName] as string;

    /// <summary>
    ///     Sets the name of the period start property.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodStartPropertyName">The name of the period start property.</param>
    public static void SetTemporalPeriodStartPropertyName(this IMutableEntityType entityType, string? periodStartPropertyName)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalPeriodStartPropertyName, periodStartPropertyName);

    /// <summary>
    ///     Sets the name of the period start property.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodStartPropertyName">The name of the period start property.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    public static string? SetTemporalPeriodStartPropertyName(
        this IConventionEntityType entityType,
        string? periodStartPropertyName,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalPeriodStartPropertyName, periodStartPropertyName, fromDataAnnotation);
        return periodStartPropertyName;
    }

    /// <summary>
    ///     Gets the name of the period end property.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The name of the period end property.</returns>
    public static string? GetTemporalPeriodEndPropertyName(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.TemporalPeriodEndPropertyName] as string;

    /// <summary>
    ///     Sets the name of the period end property.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodEndPropertyName">The name of the period end property.</param>
    public static void SetTemporalPeriodEndPropertyName(this IMutableEntityType entityType, string? periodEndPropertyName)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalPeriodEndPropertyName, periodEndPropertyName);

    /// <summary>
    ///     Sets the name of the period end property.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodEndPropertyName">The name of the period end property.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    public static string? SetTemporalPeriodEndPropertyName(
        this IConventionEntityType entityType,
        string? periodEndPropertyName,
        bool fromDataAnnotation = false)
    {
        entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.TemporalPeriodEndPropertyName, periodEndPropertyName, fromDataAnnotation);
        return periodEndPropertyName;
    }

    #endregion Temporal tables
}
