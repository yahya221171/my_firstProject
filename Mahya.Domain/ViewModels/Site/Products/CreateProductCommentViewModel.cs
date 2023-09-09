namespace Mahya.Domain.ViewModels.Site.Products
{
    public class CreateProductCommentViewModel
    {
        public long ProductId { get; set; }
        public string Text { get; set; }
    }
    public enum CreateProductCommentResult
    {
        CheckUser,
        CheckProduct,
        Suucess
    }
}
