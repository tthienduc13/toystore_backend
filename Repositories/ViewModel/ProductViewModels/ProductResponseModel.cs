namespace Repositories.ViewModel;

public class ProductResponseModel{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Img { get; set; }

    public double? Price { get; set; }

    public string? Brand { get; set; }

    public string? CategoryName { get; set; }

    public Guid? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

}