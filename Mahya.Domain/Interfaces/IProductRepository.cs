using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.ProductEntity;
using Mahya.Domain.ViewModels.Admin.Products;
using Mahya.Domain.ViewModels.Site.Products;
using Microsoft.AspNetCore.Http;

namespace Mahya.Domain.Interfaces
{
    public interface IProductRepository
    {
        #region Admin-Side

        public Task SaveChanges();

        #region ProductCategory

        Task<bool> CheckUrlCategory(string url);
        public Task<FilterProductCategoriesViewModel> FilterProductCategory(FilterProductCategoriesViewModel filter);
        public void UpdateProductCategory(ProductCategory productCategory);
        public Task<ProductCategory> GetProductCategoryById(long id);
        public Task<bool> CheckUrlCategory(string url, long categoryId);
        public Task AddProductCategory(ProductCategory productCategory);
        public Task<List<ProductCategory>> GetAllProductCategory();
        public Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel create, IFormFile image);
        public Task AddAllCategorySelectedCategory(List<long> categoryList, long categoryId);
        public Task RemoveAllCategorySelectedCategory(long categoryId);
        public Task<List<long>> GetProductCategoryByProductId(long productId);
        Task<List<long>> GetAllProductCategoryId();
        #endregion

        #region Product

        public Task AddProduct(Product product);
        public Task UpdateProduct(Product product);
        public Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter);
        public Task RemoveProductSelectedcategory(long productId);
        public Task CreateProductSelectedcategory(List<long> productSelectedCategory, long productId);
        public Task<Product> GetProductById(long productId);
        public Task<bool> DeleteProduct(long productId);
        public Task<bool> RecoverProduct(long productId);
        public Task AddProductGalleries(List<ProductGalleries> productGalleries);
        public Task<bool> IsProduct(long productId);
        public Task<List<ProductGalleries>> GetAllProductGallry(long productId);
        public Task<ProductGalleries> GetProductGalleryById(long id);
        public Task DeleteProductGallery(long galleryId);
        public Task AddProductFeature(ProductFeature productFeature);
        public Task<List<ProductFeature>>GetAllProductFeature(long productId);
        public Task DeleteProductFeature(long id);
        Task<List<ProductItemViewModel>> GetAllProductInSlider();
        Task<List<ProductItemViewModel>> GetAllProductInCategory(string hrefName, long parentId);
        Task<List<ProductItemViewModel>> NewProduct();
        Task<List<ProductItemViewModel>> RelatedProduct(string hrefName,long productId);
        Task<ProductDetailViewModel>ShowProductDetail(long productId);
        Task AddProductComments(ProductComment productComment);
        Task<List<ProductComment>> GetAllCommentByProductId(long productId);

        #endregion

        #endregion
    }
}
