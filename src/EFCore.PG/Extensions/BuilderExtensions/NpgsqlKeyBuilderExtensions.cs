using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Npgsql specific extension methods for <see cref="KeyBuilder" />.
/// </summary>
public static class NpgsqlKeyBuilderExtensions
{
    #region Without Overlaps

    /// <summary>
    ///     Configures the key to use WITHOUT OVERLAPS, which is used for temporal primary keys and unique constraints
    ///     in PostgreSQL 18 and above. The last column in the key must be a range type.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         See https://www.postgresql.org/docs/18/sql-createtable.html for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <param name="keyBuilder">The builder for the key being configured.</param>
    /// <param name="withoutOverlaps">A value indicating whether to use WITHOUT OVERLAPS.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static KeyBuilder WithoutOverlaps(
        this KeyBuilder keyBuilder,
        bool withoutOverlaps = true)
    {
        Check.NotNull(keyBuilder, nameof(keyBuilder));

        keyBuilder.Metadata.SetWithoutOverlaps(withoutOverlaps);

        return keyBuilder;
    }

    /// <summary>
    ///     Configures the key to use WITHOUT OVERLAPS, which is used for temporal primary keys and unique constraints
    ///     in PostgreSQL 18 and above. The last column in the key must be a range type.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         See https://www.postgresql.org/docs/18/sql-createtable.html for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <param name="keyBuilder">The builder for the key being configured.</param>
    /// <param name="withoutOverlaps">A value indicating whether to use WITHOUT OVERLAPS.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static KeyBuilder<TEntity> WithoutOverlaps<TEntity>(
        this KeyBuilder<TEntity> keyBuilder,
        bool withoutOverlaps = true)
        => (KeyBuilder<TEntity>)WithoutOverlaps((KeyBuilder)keyBuilder, withoutOverlaps);

    /// <summary>
    ///     Configures the key to use WITHOUT OVERLAPS, which is used for temporal primary keys and unique constraints
    ///     in PostgreSQL 18 and above. The last column in the key must be a range type.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         See https://www.postgresql.org/docs/18/sql-createtable.html for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <param name="keyBuilder">The builder for the key being configured.</param>
    /// <param name="withoutOverlaps">A value indicating whether to use WITHOUT OVERLAPS.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The same builder instance if the configuration was applied,
    ///     <c>null</c> otherwise.
    /// </returns>
    public static IConventionKeyBuilder? WithoutOverlaps(
        this IConventionKeyBuilder keyBuilder,
        bool? withoutOverlaps,
        bool fromDataAnnotation = false)
    {
        if (keyBuilder.CanSetWithoutOverlaps(withoutOverlaps, fromDataAnnotation))
        {
            keyBuilder.Metadata.SetWithoutOverlaps(withoutOverlaps, fromDataAnnotation);

            return keyBuilder;
        }

        return null;
    }

    /// <summary>
    ///     Returns a value indicating whether the key can be configured to use WITHOUT OVERLAPS.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         See https://www.postgresql.org/docs/18/sql-createtable.html for more information on temporal constraints.
    ///     </para>
    /// </remarks>
    /// <param name="keyBuilder">The builder for the key being configured.</param>
    /// <param name="withoutOverlaps">A value indicating whether to use WITHOUT OVERLAPS.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns><c>true</c> if the key can be configured to use WITHOUT OVERLAPS.</returns>
    public static bool CanSetWithoutOverlaps(
        this IConventionKeyBuilder keyBuilder,
        bool? withoutOverlaps,
        bool fromDataAnnotation = false)
    {
        Check.NotNull(keyBuilder, nameof(keyBuilder));

        return keyBuilder.CanSetAnnotation(NpgsqlAnnotationNames.WithoutOverlaps, withoutOverlaps, fromDataAnnotation);
    }

    #endregion Without Overlaps
}
