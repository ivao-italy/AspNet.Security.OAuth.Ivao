using AspNet.Security.OAuth.Ivao;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods to add Ivao authentication capabilities to an HTTP application pipeline.
/// </summary>
public static class IvaoAuthenticationExtensions
{
    /// <summary>
    /// Adds <see cref="IvaoAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Ivao authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddIvao([NotNull] this AuthenticationBuilder builder)
    {
        return builder.AddIvao(IvaoAuthenticationDefaults.AuthenticationScheme, _ => { });
    }

    /// <summary>
    /// Adds <see cref="IvaoAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Ivao authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddIvao(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] Action<IvaoAuthenticationOptions> configuration)
    {
        return builder.AddIvao(IvaoAuthenticationDefaults.AuthenticationScheme, configuration);
    }

    /// <summary>
    /// Adds <see cref="IvaoAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Ivao authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="scheme">The authentication scheme associated with this instance.</param>
    /// <param name="configuration">The delegate used to configure the Ivao options.</param>
    /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
    public static AuthenticationBuilder AddIvao(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] string scheme,
        [NotNull] Action<IvaoAuthenticationOptions> configuration)
    {
        return builder.AddIvao(scheme, IvaoAuthenticationDefaults.DisplayName, configuration);
    }

    /// <summary>
    /// Adds <see cref="IvaoAuthenticationHandler"/> to the specified
    /// <see cref="AuthenticationBuilder"/>, which enables Ivao authentication capabilities.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="scheme">The authentication scheme associated with this instance.</param>
    /// <param name="caption">The optional display name associated with this instance.</param>
    /// <param name="configuration">The delegate used to configure the Ivao options.</param>
    /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
    public static AuthenticationBuilder AddIvao(
        [NotNull] this AuthenticationBuilder builder,
        [NotNull] string scheme,
        [NotNull] string caption,
        [NotNull] Action<IvaoAuthenticationOptions> configuration)
    {
        return builder.AddOAuth<IvaoAuthenticationOptions, IvaoAuthenticationHandler>(scheme, caption, configuration);
    }
}
