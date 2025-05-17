using Microsoft.AspNetCore.Mvc;

//Essa parte define a aplicação da API e faz o mapeamento das rotas, indicando qual metódo irá ser chamado por qual rota.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<EstoqueService>();
var app = builder.Build();

app.MapGet("/", () => "API Disponível!");

app.MapPost("/addProdutoEstoque", async (EstoqueService service, Produto produto) => {
    service.AdicionarProduto(produto);
    return Results.Ok("Produto adicionado.");
    });

app.MapDelete("/removerProdutoEstoque/{id}", async (EstoqueService service, string id) =>
{
    await service.RemoverProdutoAsync(id);
    return Results.Ok("Produto removido.");
});

app.Run();
