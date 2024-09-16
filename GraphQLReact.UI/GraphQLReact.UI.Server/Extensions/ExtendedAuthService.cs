using AuthPlus.Identity.Dtos;
using AuthPlus.Identity.Interfaces;
using AuthPlus.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace GraphQLReact.UI.Server.Extensions;

public class ExtendedAuthService : IAuthService
{
    private readonly AuthService _authService;

    public ExtendedAuthService(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthenticationResult> RegisterAsync(RegisterDto registerDto)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        return await _authService.RegisterAsync(registerDto);
    }

    public async Task<AuthenticationResult> LoginAsync(LoginDto loginDto)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        return await _authService.LoginAsync(loginDto);
    }

    public async Task<AuthenticationResult> ExternalLoginAsync(string provider, string token)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        return await _authService.ExternalLoginAsync(provider, token);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string token, string userId)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        return await _authService.ConfirmEmailAsync(token, userId);
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        return await _authService.RefreshTokenAsync(refreshTokenDto);
    }

    public async Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        return await _authService.ResetPasswordAsync(resetPasswordDto);
    }

    public async Task SendResetPasswordEmailAsync(ForgotPasswordDto forgotPasswordDto)
    {
        // Add custom logic here if needed

        // Delegate to base AuthService
        await _authService.SendResetPasswordEmailAsync(forgotPasswordDto);
    }
}