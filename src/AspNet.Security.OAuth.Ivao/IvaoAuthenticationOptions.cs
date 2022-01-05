using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using static AspNet.Security.OAuth.Ivao.IvaoAuthenticationConstants;

namespace AspNet.Security.OAuth.Ivao;

/// <summary>
/// Defines a set of options used by <see cref="IvaoAuthenticationHandler"/>.
/// </summary>
public class IvaoAuthenticationOptions : OAuthOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IvaoAuthenticationOptions"/> class.
    /// </summary>
    public IvaoAuthenticationOptions()
    {
        AuthorizationEndpoint = IvaoAuthenticationDefaults.AuthorizationEndpoint;
        CallbackPath = IvaoAuthenticationDefaults.CallbackPath;
        TokenEndpoint = IvaoAuthenticationDefaults.TokenEndpoint;
        UserInformationEndpoint = IvaoAuthenticationDefaults.UserInformationEndpoint;
        ClientId = "NONE";
        ClientSecret = "NONE";
        SaveTokens = false;

        // Available scopes: https://docs.gitlab.com/ee/integration/oauth_provider.html
        //Scope.Add("read_user");

        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "vid");
        ClaimActions.MapJsonKey(ClaimTypes.Name, "firstname");
        ClaimActions.MapJsonKey(ClaimTypes.Surname, "lastname");
        ClaimActions.MapJsonKey(Claims.RatingAtc, "ratingatc");
        ClaimActions.MapJsonKey(Claims.RatingPilot, "ratingpilot");
        ClaimActions.MapJsonKey(Claims.Division, "division");
        ClaimActions.MapJsonKey(Claims.HoursAtc, "hours_atc");
        ClaimActions.MapJsonKey(Claims.HoursPilot, "hours_pilot");
        ClaimActions.MapJsonKey(Claims.IsNpoMember, "isNpoMember");
        ClaimActions.MapJsonKey(Claims.StaffPositions, "staff");

        ClaimActions.MapJsonKey(Claims.IsVaStaff, "va_staff");
        ClaimActions.MapJsonKey(Claims.VaIds, "va_staff_ids");
        ClaimActions.MapJsonKey(Claims.VaIcaos, "va_staff_icaos");
        ClaimActions.MapJsonKey(Claims.VaMemberIds, "va_member_ids");
    }
}
