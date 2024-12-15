using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace OauthPlayground;

public class ReverseMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var result = context.User.Identity.IsAuthenticated;
        IEnumerable<Claim> claims = context.User.Identities.FirstOrDefault()!.Claims;

        foreach (var claim in claims)
        {
            Console.WriteLine(claim.Value);
        }
        
        await next.Invoke(context);
    }
}