namespace RefitAbstractions.DemoClient.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
}

public class ProductCreateModel
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
}

public class ProductUpdateModel
{
    public string? Name { get; set; }
}

public class ProductQueryFilterModel
{
    public string? Name { get; set; }
}