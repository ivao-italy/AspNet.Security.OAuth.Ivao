using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AspNet.Security.OAuth.Ivao;

public partial class IvaoAuthenticationHandler : OAuthHandler<IvaoAuthenticationOptions>
{
    public IvaoAuthenticationHandler(
        IOptionsMonitor<IvaoAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticationTicket> CreateTicketAsync(
        [NotNull] ClaimsIdentity identity,
        [NotNull] AuthenticationProperties properties,
        [NotNull] OAuthTokenResponse tokens)
    {
        var principal = new ClaimsPrincipal(identity);
        var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, tokens.Response!.RootElement);
        context.RunClaimActions();

        await Events.CreatingTicket(context);
        return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
    }

    /// <summary>
    /// 0 - Crea l'url per il redirect del challenge
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
    {
        ICollection<string> parameter = properties.GetParameter<ICollection<string>>(OAuthChallengeProperties.ScopeKey)!;
        string value = (parameter != null) ? FormatScope(parameter) : FormatScope();
        string value2 = base.Options.StateDataFormat.Protect(properties);
        Dictionary<string, string> queryString = new Dictionary<string, string>
            {
                {
                    "url",
                    redirectUri
                }
            };
        return QueryHelpers.AddQueryString(base.Options.AuthorizationEndpoint, queryString);
    }

    /// <summary>
    /// 1 - chiama IVAO e ottiene il redirect con il token settato 
    /// </summary>
    /// <returns></returns>
    protected override async Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
    {
        var query = Request.Query;

        //var properties = Options.StateDataFormat.Unprotect("IVAO");
        var properties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            IsPersistent = true,
            AllowRefresh = false,
        };
        
        var token = query["IVAOTOKEN"];
        if (token == "error")
        {
            //Ivao non ci da dettaglio sull'errore
            return HandleRequestResult.Fail("Error authenticating user with IVAO");
        }

        var codeExchangeContext = new OAuthCodeExchangeContext(properties, code: token, BuildRedirectUri(Options.CallbackPath));
        var tokens = await ExchangeCodeAsync(codeExchangeContext);
        if (tokens.Error != null)
        {
            return HandleRequestResult.Fail(tokens.Error);
        }

        var identity = new ClaimsIdentity(ClaimsIssuer);       
        var ticket = await CreateTicketAsync(identity, properties, tokens);
        if (ticket != null)
        {
            return HandleRequestResult.Success(ticket);
        }
        else
        {
            return HandleRequestResult.Fail("Failed to retrieve user information from remote server.", properties);
        }
    }

    /// <summary>
    /// 2 - Scambia il token con l'endpoint delle API per avere i dati dell'utente
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(OAuthCodeExchangeContext context)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Options.TokenEndpoint}{context.Code}");
        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await Backchannel.SendAsync(requestMessage, Context.RequestAborted);
        var body = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode switch
        {
            true => OAuthTokenResponse.Success(JsonDocument.Parse(body)),
            false => OAuthTokenResponse.Failed(new Exception("Error authenticating user with IVAO"))
        };
    }    
}
