using Microsoft.AspNetCore.Components.Forms;

namespace ISTUDIO.Web.UI.Features.Products.Models;

public class PhotoUploadEventArgs
{
    public IBrowserFile PhotoProduct { get; set; }
    public int ProductId { get; set; }
}
