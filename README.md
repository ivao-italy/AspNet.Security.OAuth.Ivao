# AspNet.Security.OAuth.Ivao
# IVAO IT Discord Automation
Project made with ❤️ by IVAO Italy division.

![Discord](https://img.shields.io/discord/426318927220441089)

**AspNet.Security.OAuth** is a **security middleware** that you can use in your **ASP.NET Core** application to support **IVAO** authentication provider.

## Getting started

**Adding IVAO authentication to your application is a breeze** and just requires a few lines in your Program.cs file:

```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = IvaoAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddIvao();

...

app.UseAuthentication();
app.UseAuthorization();
```

See the [sample project](https://github.com/ivao-italy/AspNet.Security.OAuth.Ivao/tree/master/src/Ivao.It.Authentication) directory for a complete sample **using ASP.NET Core 6 and supporting ivao login provider**.

## Ivao Login v1 Documentation
Docs can be found on [the international forum](http://it.forum.ivao.aero/index.php?topic=285996.0). IVAO Account is required.

## Support

**Need help or wanna share your thoughts?** Don't hesitate to join us on Discord:
- **[Discord](https://discord.ivao.it)**

## License

This project is licensed under the **Apache License**. This means that you can use, modify and distribute it freely. See [https://www.apache.org/licenses/LICENSE-2.0.html](https://www.apache.org/licenses/LICENSE-2.0.html) for more details.
