class Pedido{
    public string Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<Produto> Produtos { get; set; }
}