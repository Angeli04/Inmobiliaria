using System;
using Microsoft.Extensions.Configuration;

public abstract class RepositorioBase
{
    protected readonly IConfiguration Configuration;
    protected readonly string connectionString;

    protected RepositorioBase(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        connectionString = Configuration["ConnectionStrings:DefaultConnection"]
            ?? throw new InvalidOperationException("No se encontró ConnectionStrings:DefaultConnection en IConfiguration.");
    }
}
