using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using AppInvWebSharedLibrary.DTOs;
//using AppInvWebSharedLibrary.State;
using System.IdentityModel.Tokens.Jwt;
using AppInvWebSharedLibrary.State;
using Microsoft.JSInterop;


namespace AppInvWebClient.ClientStates
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var jwtToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwtToken");
                if (string.IsNullOrEmpty(jwtToken))
                    return new AuthenticationState(anonymous);

                var getUserClaims = DecryptToken(jwtToken);
                if (getUserClaims == null)
                    return new AuthenticationState(anonymous);

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return new AuthenticationState(claimsPrincipal);
                //if (string.IsNullOrEmpty(ConstantsJWT.JWTToken))
                //{
                //    Console.WriteLine("JWT Token is null or empty.");
                //    return await Task.FromResult(new AuthenticationState(anonymous));
                //}


                //var getUserClaims = DecryptToken(ConstantsJWT.JWTToken);
                //if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymous));

                //var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                //return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return new AuthenticationState(anonymous);
                //return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async void UpdateAuthenticationState(string jwtToken)
        {
            if (!string.IsNullOrEmpty(jwtToken))
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwtToken", jwtToken);
                var getUserClaims = DecryptToken(jwtToken);
                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwtToken");
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
            }
        }

        //public async void UpdateAuthenticationState(string jwtToken)
        //{
        //    var claimsPrincipal = new ClaimsPrincipal();
        //    if (!string.IsNullOrEmpty(jwtToken))
        //    {
        //        ConstantsJWT.JWTToken = jwtToken;
        //        var getUserClaims = DecryptToken(jwtToken);
        //        claimsPrincipal = SetClaimPrincipal(getUserClaims);
        //    }
        //    else
        //    {
        //        ConstantsJWT.JWTToken = null!;
        //    }
        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        //}

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, claims.Name!),
                    new(ClaimTypes.Email, claims.Email!),
                    new(ClaimTypes.Role, claims.role!),
                }, "JwtAuth"));
        }

        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
            var role = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Role);
            return new CustomUserClaims(name!.Value, email!.Value, role!.Value);
        }

        public void UpdateNotAuthenticationState()
        {
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
    }
}
