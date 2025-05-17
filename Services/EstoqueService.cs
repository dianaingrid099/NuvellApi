using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

class EstoqueService{
    //reandonly é apenas leitura
    public readonly string _pathEstoque = "BaseDeDados/Estoque.json";
    
    public List<Produto> getProdutos(){
        //Chama um manipulador de arquivos dotnet para leitura do arquivo como texto
        var produtosJson = File.ReadAllText(_pathEstoque);
        var produtos = new List<Produto>();
        
        if (produtosJson != null && produtosJson != ""){

            //Json serializer é uma biblioteca do dotnet para manipular Json, o deserialize vai transformar o Json em uma lista de objetos - JsonSerializer.Deserialize<ObjetoQueVaiSerCriado>(JsonASerTransformado)
            produtos = JsonSerializer.Deserialize<List<Produto>>(produtosJson);

            if(produtos != null && produtos.Count > 0){
                return produtos;
            }else{
                return new List<Produto>();
            }
        }
        else{
            return new List<Produto>();
        }
        
    }
    [HttpPost]

    async public Task AdicionarProduto(Produto produto)
    {
        var estoque = getProdutos();

        var produtoExistente = estoque.FirstOrDefault(p => p.Nome == produto.Nome);
        var estoqueAtualizado = "";

        //Verifica se existe algum produto cadastrado com o mesmo nome, e se tiver, adiciona apenas a quantidade
        if (produtoExistente != null)
        {
            //Busca o item que já existe para adicionar
            foreach (var item in estoque)
            {
                if (item.Nome == produtoExistente.Nome)
                {//Soma a quantidade no estoque do Produto informado
                    item.Estoque += produto.Estoque;
                }
            }
            estoqueAtualizado = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizado);
            return;
        }

        //Se não existir o produto em estoque, cria um novo
        estoque.Add(produto);

        estoqueAtualizado = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizado);
        return;
    }

    [HttpDelete]

    public async Task RemoverProdutoAsync(string id)
    {
        var estoque = getProdutos();

        var produtoExistente = estoque.FirstOrDefault(p => p.Id.ToString() == id);
        if (produtoExistente != null)
        {
            estoque.Remove(produtoExistente);

            var estoqueAtualizado = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizado);
        }
    }
    
}
