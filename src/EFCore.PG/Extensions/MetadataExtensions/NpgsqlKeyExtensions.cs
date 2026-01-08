using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Extension methods for <see cref="IKey" /> for Npgsql-specific metadata.
/// </summary>
public static class NpgsqlKeyExtensions
{
    #region Without Overlaps

    /// <summary>
    ///     Returns a value indicating whether the key uses WITHOUT OVERLAPS, which is used for temporal
    ///     primary keys and unique constraints in PostgreSQL 18 and above.
    /// </summary>
    /// <remarks>
    ///     https://www.postgresql.org/docs/18/sql-createtable.html
    /// </remarks>
    public static bool? GetWithoutOverlaps(this IReadOnlyKey key)
        => (bool?)key[NpgsqlAnnotationNames.WithoutOverlaps];

    /// <summary>
    ///     Sets a value indicating whether the key uses WITHOUT OVERLAPS, which is used for temporal
    ///     primary keys and unique constraints in PostgreSQL 18 and above.
    /// </summary>
    /// <remarks>
    ///     https://www.postgresql.org/docs/18/sql-createtable.html
    /// </remarks>
    public static void SetWithoutOverlaps(this IMutableKey key, bool? withoutOverlaps)
        => key.SetOrRemoveAnnotation(NpgsqlAnnotationNames.WithoutOverlaps, withoutOverlaps);

    /// <summary>
    ///     Sets a value indicating whether the key uses WITHOUT OVERLAPS, which is used for temporal
    ///     primary keys and unique constraints in PostgreSQL 18 and above.
    /// </summary>
    /// <remarks>
    ///     https://www.postgresql.org/docs/18/sql-createtable.html
    /// </remarks>
    public static bool? SetWithoutOverlaps(
        this IConventionKey key,
        bool? withoutOverlaps,
        bool fromDataAnnotation = false)
    {
        key.SetOrRemoveAnnotation(NpgsqlAnnotationNames.WithoutOverlaps, withoutOverlaps, fromDataAnnotation);

        return withoutOverlaps;
    }

    /// <summary>
    ///     Returns the <see cref="ConfigurationSource" /> for whether the key uses WITHOUT OVERLAPS.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The <see cref="ConfigurationSource" /> for whether the key uses WITHOUT OVERLAPS.</returns>
    public static ConfigurationSource? GetWithoutOverlapsConfigurationSource(this IConventionKey key)
        => key.FindAnnotation(NpgsqlAnnotationNames.WithoutOverlaps)?.GetConfigurationSource();

    #endregion Without Overlaps
}
