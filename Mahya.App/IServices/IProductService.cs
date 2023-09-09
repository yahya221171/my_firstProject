using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.ProductEntity;
using Mahya.Domain.ViewModels.Admin.Products;
using Mahya.Domain.ViewModels.Site.Products;
using Microsoft.AspNetCore.Http;

namespace Mahya.App.IServices
{
    public interface IProductService
    {
        #region Admin-Side
        #region Product-Category

        public Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel create, IFormFile image);
        public Task<EditProductCategoryViewModel> GetEditProductCategoryForShow(long categoryId);
        public Task<EditProductCategoryResult> EditProductCategory(EditProductCategoryViewModel edit, IFormFile image);
        public Task<FilterProductCategoriesViewModel> FilterProductCategory(FilterProductCategoriesViewModel filter);
        public Task<List<ProductCategory>> GetAllProductCategory();
        public Task<ProductCategory> GetProductCategoryById(long id);


        #endregion

        #region Product

        public Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter);
        Task<CreateProductResult> CreateProduct(CreateProductViewModel create, IFormFile image1, IFormFile image2);
        public Task RemoveProductSelectedcategory(long productId);
        public Task CreateProductSelectedcategory(List<long> productSelectedCategory, long productId);
        public Task<EditProductViewModel> EditProductForShow(long productId);
        public Task<EditProductResult> EditProduct(EditProductViewModel edit);
        public Task<bool> DeleteProduct(long productId);
        public Task<bool> RecoverProduct(long productId);
        public Task<bool> Addproductgallries(long productId, List<IFormFile> images);
        public Task<List<ProductGalleries>> GetAllProductGallry(long productId);
        public Task DeleteImageFromGallery(long galleryId);
        public Task<CreateProductFeatuersResult> CreateProductFeatures(CreateProductFeatuersViewModel create);
        public Task<List<ProductFeature>> GetAllProductFeature(long productId);
        public Task DeleteProductFeature(long id);
        Task<List<ProductItemViewModel>> GetAllProductInSlider();
        Task<List<ProductItemViewModel>> GetAllProductInCategory(string hrefName, long parentId);
        Task<List<ProductItemViewModel>> NewProduct();
        Task<ProductDetailViewModel> ShowProductDetail(long productId);
        Task<CreateProductCommentResult> CreateProductComments(CreateProductCommentViewModel create,long userId);
        Task<List<ProductComment>> GetAllCommentByProductId(long productId);
        Task<List<ProductItemViewModel>> RelatedProduct(string hrefName, long productId);



        #endregion

        #endregion
    }
}
