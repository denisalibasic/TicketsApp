using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.context;
using TicketsAPI.model;
using TicketsAPI.request;
using TicketsAPI.authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TicketsDbContext>(options => options.UseInMemoryDatabase("TicketsDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Tickets Management API",
        Description = "API for managing tickets"
    });

    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "X-API-Key",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "API key needed to access the endpoints"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new List<string>()
        }
    });
});

var apiKey = builder.Configuration["ApiKey"];
builder.Services.AddSingleton<IApiKeyValidator>(new ApiKeyValidator(apiKey));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TicketsDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapGet("/tickets", async (TicketsDbContext dbContext) =>
    {
        var tickets = await dbContext.Tickets
        .Join(dbContext.Providers,
              ticket => ticket.ProviderId,
              provider => provider.Id,
              (ticket, provider) => new
              {
                  TicketId = ticket.Id,
                  TicketName = ticket.Name,
                  TicketQuantity = ticket.Quantity,
                  ProviderId = provider.Id,
                  ProviderName = provider.Name
              })
        .ToListAsync();

        return Results.Ok(tickets);
    })
    .WithName("GetAllTickets")
    .WithTags("Get all tickets")
    .WithOpenApi();

app.MapGet("/tickets/{id:int}", async (int id, TicketsDbContext dbContext) =>
    await dbContext.Tickets.FindAsync(id)
        is Ticket ticket
            ? Results.Ok(ticket)
            : Results.NotFound())
            .WithName("GetTicketById")
            .WithTags("Get ticket by Id");


app.MapGet("/tickets/provider/{id:int}", async (int id, TicketsDbContext dbContext) =>
    {
        var providers = await dbContext.Tickets
        .Where(ticket => ticket.ProviderId == id)
        .Join(dbContext.Providers,
              ticket => ticket.ProviderId,
              provider => provider.Id,
              (ticket, provider) => new
              {
                  Provider = new
                  {
                      Id = provider.Id,
                      Name = provider.Name
                  },
                  Tickets = new
                  {
                      Id = ticket.Id,
                      Name = ticket.Name,
                      Quantity = ticket.Quantity
                  }
              })
        .GroupBy(x => x.Provider)
        .Select(g => new
        {
            Provider = g.Key,
            Tickets = g.Select(x => x.Tickets)
        })
        .FirstOrDefaultAsync();

        return Results.Ok(providers);
    })
    .WithName("GetTicketsByProviderId")
    .WithTags("Get tickets by provider Id");


app.MapGet("/providers", async (TicketsDbContext dbContext) =>
{
    var providers = await dbContext.Providers.ToListAsync();
    return providers.Any() ? Results.Ok(providers) : Results.NotFound();
})
.WithName("GetAllProviders")
.WithTags("Get all providers");

app.MapGet("/provider/{id:int}", async (int id, TicketsDbContext dbContext) =>
    await dbContext.Providers.FindAsync(id)
        is Provider provider
            ? Results.Ok(provider)
            : Results.NotFound())
        .WithName("GetProviderById")
        .WithTags("Get provider by Id");

app.MapPost("/tickets", async (CreateTicketRequest ticketRequest, TicketsDbContext dbContext) =>
    {
        var provider = await dbContext.Providers
            .Where(provider => provider.Name == ticketRequest.ProviderName)
            .FirstOrDefaultAsync();

        if (provider is null) return Results.BadRequest();

        Ticket ticket = new Ticket(ticketRequest, provider);

        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync();

        return Results.Created($"/tickets/{ticket.Id}", ticket);
    })
    .WithName("CreateTicket")
    .WithTags("Add new ticket to list");

app.MapPut("/tickets/{id:int}", async (int id, UpdateTicketRequest requestTicket, TicketsDbContext dbContext) =>
    {
        var ticket = await dbContext.Tickets.FindAsync(id);

        if (ticket is null) return Results.NotFound();

        ticket.Name = requestTicket.Name;
        ticket.Quantity = requestTicket.Quantity;

        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    })
    .WithName("UpdateTicketById")
    .WithTags("Update ticket by Id");

app.MapDelete("/tickets/{id:int}", async (int id, TicketsDbContext dbContext) =>
    {
        if (await dbContext.Tickets.FindAsync(id) is Ticket ticket)
        {
            dbContext.Tickets.Remove(ticket);
            await dbContext.SaveChangesAsync();
            return Results.Ok(ticket);
        }

        return Results.NotFound();
    })
    .WithName("DeleteTicketById")
    .WithTags("Delete ticket by Id");

app.Run();
