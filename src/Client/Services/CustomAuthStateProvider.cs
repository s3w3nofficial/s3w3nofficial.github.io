using Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public User User { get; set; }

        public async Task LogIn(IJSRuntime jsRuntime)
        {
            this.User = await jsRuntime.InvokeAsync<User>("logIn");
            NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
            await Task.CompletedTask;
        }

        public async Task LogOut(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("logOut");
            this.User = null;
            NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
            await Task.CompletedTask;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (User != null)
            {
                var identity =
                    new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.Name, User.Name),
                            new Claim(ClaimTypes.Email, User.Email)
                        },
                        "Custom Auth");

                var user = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(user));
            }
            else
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
    }
}
