namespace ISTUDIO.Contracts.Features.Products;

public class EditAllPhotosProductVM
{
    public int ProductId { get; set; }
    public List<string> ProductPhotos { get; set; } = new List<string>();
}

