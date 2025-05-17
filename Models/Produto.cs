class Produto {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; } 
    public int Estoque { get; set; }
    public int? QuantidadeNoPedido { get; set; }

}
