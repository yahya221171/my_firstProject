using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.App.Utils;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.ProductEntity;
using Mahya.Domain.ViewModels.Admin.Products;
using Mahya.Domain.ViewModels.Site.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.App.Services
{
    public class ProductService:IProductService
    {
        #region Constructor

        private readonly IProductRepository _productRepository;
        private readonly IUserInterface _userInterface;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IViewRenderService _renderService;

        public ProductService(IProductRepository productRepository, IUserInterface userInterface, IPasswordHelper passwordHelper, IViewRenderService renderService)
        {
            _productRepository = productRepository;
            _userInterface = userInterface;
            _passwordHelper = passwordHelper;
            _renderService = renderService;
        }

        #endregion

        #region Admin-Side

        #region Product-Category

        public async Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel create, IFormFile image)
        {
            return await _productRepository.CreateProductCategory(create, image);
        }

        public async Task<EditProductCategoryViewModel> GetEditProductCategoryForShow(long categoryId)
        {
            var pc = await _productRepository.GetProductCategoryById(categoryId);
            if (pc != null)
                return new EditProductCategoryViewModel()
                {
                    Title = pc.Title,
                    UrlName = pc.UrlName,
                    ProductCategoryId = pc.Id,
                    ImageName = pc.ImageName,
                    SelectedCategory = await _productRepository.GetAllProductCategoryId()
                };
            return null;
        }

        public async Task<EditProductCategoryResult> EditProductCategory(EditProductCategoryViewModel edit, IFormFile image)
        {
            var productcategory = await _productRepository.GetProductCategoryById(edit.ProductCategoryId);

            if (productcategory == null) return EditProductCategoryResult.NotFound;

            if (await _productRepository.CheckUrlCategory(edit.UrlName, edit.ProductCategoryId)) return EditProductCategoryResult.IsExistUrlName;

            productcategory.UrlName = edit.UrlName;
            productcategory.Title = edit.Title;

            if (image != null && image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                image.AddImageToServer(imageName, PathExtensions.CategoryOrginServer, 150, 150, PathExtensions.CategoryThumbServer, productcategory.ImageName);

                productcategory.ImageName = imageName;
            }

            _productRepository.UpdateProductCategory(productcategory);
            await _productRepository.RemoveAllCategorySelectedCategory(edit.ProductCategoryId);
            await _productRepository.AddAllCategorySelectedCategory(edit.SelectedCategory, edit.ProductCategoryId);

            await _productRepository.SaveChanges();

            return EditProductCategoryResult.Success;
        
    }

        public async Task<FilterProductCategoriesViewModel> FilterProductCategory(FilterProductCategoriesViewModel filter)
        {
            return await _productRepository.FilterProductCategory(filter);
        }

        public async Task<List<ProductCategory>> GetAllProductCategory()
        {
            return await _productRepository.GetAllProductCategory();
        }

        public async Task<ProductCategory> GetProductCategoryById(long id)
        {
            return await _productRepository.GetProductCategoryById(id);
        }

    

        #endregion

        #region Products

        public async Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter)
        {
            return await _productRepository.FilterProduct(filter);
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductViewModel create, IFormFile image1, IFormFile image2)
        {
            var product = new Product()
            {
                Name = create.Name,
                Description = create.Description,
                ShortDescription = create.ShortDescription,
                Price = create.Price,
                IsActive = create.IsActive,
                NewPrice = create.NewPrice,
                Discount = create.Discount
                
            };
            if (image1 != null && image1.IsImage())
            {
                var imageName1 = Guid.NewGuid().ToString("N") + Path.GetExtension(image1.FileName);
                image1.AddImageToServer(imageName1, PathExtensions.Produc1tOrginServer, 1200, 1200,
                    PathExtensions.Product1ThumbServer);
                product.ProductImageName=imageName1;
            }
            else
            {
                return CreateProductResult.NotImage;
            }
            if (image2 != null && image2.IsImage())
            {
                var imageName2 = Guid.NewGuid().ToString("N") + Path.GetExtension(image2.FileName);
                image2.AddImageToServer(imageName2, PathExtensions.Product2OrginServer, 1200, 1200,
                    PathExtensions.Product2ThumbServer);
                product.ProductImageName2 = imageName2;
            }
            else
            {
                return CreateProductResult.NotImage;
            }

            await _productRepository.AddProduct(product);
            await _productRepository.SaveChanges();
            await _productRepository.CreateProductSelectedcategory(create.ProductSelectedCategory, product.Id);
            return CreateProductResult.Success;
        }

        public async Task RemoveProductSelectedcategory(long productId)
        {
             await _productRepository.RemoveProductSelectedcategory(productId);
        }

        public async Task CreateProductSelectedcategory(List<long> productSelectedCategory, long productId)
        {
            await _productRepository.CreateProductSelectedcategory(productSelectedCategory, productId);
        }

        public async Task<EditProductViewModel> EditProductForShow(long productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null) return null;

            return new EditProductViewModel()
            {
                ProductId = product.Id,
                Name = product.Name,
                IsActive = product.IsActive,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Price = product.Price,
                NewPrice = (int) product.NewPrice,
                ProductImageName1 = product.ProductImageName,
                ProductImageName2 = product.ProductImageName2,
                Discount = product.Discount,
                ProductSelectedCategory = await _productRepository.GetProductCategoryByProductId(productId)
            };

        }

        public async Task<EditProductResult> EditProduct(EditProductViewModel edit)
        {
            var newProduct = await _productRepository.GetProductById(edit.ProductId);
            if(newProduct== null) return EditProductResult.NotFound;
            if (edit.ProductSelectedCategory == null) return EditProductResult.NotProductSelectedCategoryHasNull;
            newProduct.Description = edit.Description;
            newProduct.IsActive = edit.IsActive;
            newProduct.Discount = edit.Discount;
            newProduct.Price = edit.Price;
            newProduct.NewPrice = edit.NewPrice;
            newProduct.ShortDescription = edit.ShortDescription;
            newProduct.Name = edit.Name;
            if (edit.ProductImage1 != null && edit.ProductImage1.IsImage())
            {
                var imageName1 = Guid.NewGuid().ToString("N") + Path.GetExtension(edit.ProductImage1.FileName);
                edit.ProductImage1.AddImageToServer(imageName1, PathExtensions.Produc1tOrginServer, 1200, 1200,
                    PathExtensions.Product1ThumbServer,newProduct.ProductImageName);
                newProduct.ProductImageName = imageName1;
            }
            if (edit.ProductImage2 != null && edit.ProductImage2.IsImage())
            {
                var imageName2 = Guid.NewGuid().ToString("N") + Path.GetExtension(edit.ProductImage2.FileName);
                edit.ProductImage2.AddImageToServer(imageName2, PathExtensions.Product2OrginServer, 1200, 1200,
                    PathExtensions.Product2ThumbServer, newProduct.ProductImageName2);
                newProduct.ProductImageName2= imageName2;
            }

            await _productRepository.UpdateProduct(newProduct);
           
            await _productRepository.RemoveProductSelectedcategory(edit.ProductId);
            await _productRepository.CreateProductSelectedcategory(edit.ProductSelectedCategory, edit.ProductId);
            await _productRepository.SaveChanges();

            return EditProductResult.Success;
        }

        public async Task<bool> DeleteProduct(long productId)
        {
            return await _productRepository.DeleteProduct(productId);
        }

        public async Task<bool> RecoverProduct(long productId)
        {
            return await _productRepository.RecoverProduct(productId);
        }

        public async Task<bool> Addproductgallries(long productId, List<IFormFile> images)
        {
            if (!await _productRepository.IsProduct(productId))
            {
                return false;
            }
            if (images != null && images.Any())
            {
                var productGallery = new List<ProductGalleries>();

                foreach (var image in images)
                {
                    if (image.IsImage())
                    {
                        var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                        image.AddImageToServer(imageName, PathExtensions.ProductOrginServer, 1200, 1200,
                            PathExtensions.ProductThumbServer);
                        productGallery.Add(new ProductGalleries()
                        {
                            ProductId = productId,
                            ImageName = imageName
                        });
                    }
                }
                await _productRepository.AddProductGalleries(productGallery);
                return true;

            }

            return false;
        }

        public async Task<List<ProductGalleries>> GetAllProductGallry(long productId)
        {
            return await _productRepository.GetAllProductGallry(productId);
        }

        public async Task DeleteImageFromGallery(long galleryId)
        {
            var productGallery = await _productRepository.GetProductGalleryById(galleryId);
            if (productGallery != null)
            {
                UploadImageExtension.DeleteImage(productGallery.ImageName, PathExtensions.ProductOrginServer, PathExtensions.ProductThumbServer);
                await _productRepository.DeleteProductGallery(galleryId);
            }
        }

        public async Task<CreateProductFeatuersResult> CreateProductFeatures(CreateProductFeatuersViewModel create)
        {
            if (!await _productRepository.IsProduct(create.ProductId))
            {
                return CreateProductFeatuersResult.Error;
            }

            var productFeaature = new ProductFeature()
            {
                ProductId = create.ProductId,
                FeatuerTitle = create.Title,
                FeatureValue = create.Value
            };
            await _productRepository.AddProductFeature(productFeaature);
            await _productRepository.SaveChanges();
            return CreateProductFeatuersResult.Success;
        }

        public async Task<List<ProductFeature>> GetAllProductFeature(long productId)
        {
            return await _productRepository.GetAllProductFeature(productId);
        }

        public async Task DeleteProductFeature(long id)
        {
            await _productRepository.DeleteProductFeature(id);
        }

        public async Task<List<ProductItemViewModel>> GetAllProductInSlider()
        {
            return await _productRepository.GetAllProductInSlider();
        }

        public async Task<List<ProductItemViewModel>> GetAllProductInCategory(string hrefName, long parentId)
        {
            return await _productRepository.GetAllProductInCategory(hrefName,parentId);
        }

        public async Task<List<ProductItemViewModel>> NewProduct()
        {
            return await _productRepository.NewProduct();
        }

        public async Task<ProductDetailViewModel> ShowProductDetail(long productId)
        {
            return await _productRepository.ShowProductDetail(productId);
        }

        public async Task<CreateProductCommentResult> CreateProductComments(CreateProductCommentViewModel create, long userId)
        {
            var user = await _userInterface.GetUserById(userId);
            if (user == null) return CreateProductCommentResult.CheckUser;
            if (!await _productRepository.IsProduct(create.ProductId))
            {
                return CreateProductCommentResult.CheckProduct;
            }

            var comment = new ProductComment()
            {
                ProductId = create.ProductId,
                UserId = userId,
                Text = create.Text
            };
            await _productRepository.AddProductComments(comment);
            await _productRepository.SaveChanges();
            return CreateProductCommentResult.Suucess;
        }

        public async Task<List<ProductComment>> GetAllCommentByProductId(long productId)
        {
            return await _productRepository.GetAllCommentByProductId(productId);
        }

        public async Task<List<ProductItemViewModel>> RelatedProduct(string hrefName, long productId)
        {
            return await _productRepository.RelatedProduct(hrefName, productId);
        }

        #endregion

        #endregion

    }
}
