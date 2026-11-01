using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Common.Responses;

namespace UserManagement.Api.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddCustomValidation(
        this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors
                            .Select(e => e.ErrorMessage)
                            .ToArray()
                    );

                return new BadRequestObjectResult(
                    ValidationResponse.Create(errors)
                );
            };
        });

        return services;
    }
}