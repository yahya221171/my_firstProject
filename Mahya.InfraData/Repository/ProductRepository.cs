using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.Utils;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.ProductEntity;
using Mahya.Domain.ViewModels.Admin.Products;
using Mahya.Domain.ViewModels.Paging;
using Mahya.Domain.ViewModels.Site.Products;
using Mahya.InfraData.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Mahya.InfraData.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Constructor

        private readonly MahyaDbContext _context;

        public ProductRepository(MahyaDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin-Side

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CreateProductCategoryResult> CreateProductCategory(CreateProductCategoryViewModel create, IFormFile image)
        {
            if (await CheckUrlCategory(create.UrlName))
                return CreateProductCategoryResult.IsExistUrlName;

            var newProductcategory = new ProductCategory()
            {
                UrlName = create.UrlName,
                Title = create.Title,
                ParentId = create.SelectedCategory.First()
            };
            if (image != null && image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                image.AddImageToServer(imageName, PathExtensions.CategoryOrginServer, 150, 150, PathExtensions.CategoryThumbServer);

                newProductcategory.ImageName = imageName;
            }

            create.SelectedCategory = await _context.ProductCategories.Select(c => c.Id).ToListAsync();
            await _context.AddAsync(newProductcategory);
            await SaveChanges();
            return CreateProductCategoryResult.Success;
        }

        public async Task AddAllCategorySelectedCategory(List<long> categoryList, long categoryId)
        {
            if (categoryList != null && categoryList.Any())
            {
                var category = await GetProductCategoryById(categoryId);
                categoryList = await GetAllProductCategoryId();
                category.ParentId = categoryList.First();
                await _context.ProductCategories.AddRangeAsync(category);
                
            }
        }

        public async Task RemoveAllCategorySelectedCategory(long categoryId)
        {
            var categorySelectedCategory = await _context.ProductCategories.AsQueryable().ToListAsync();
            if (categorySelectedCategory.Any())
            {
                _context.RemoveRange(categorySelectedCategory);
            }
        }

        public async Task<List<long>> GetProductCategoryByProductId(long productId)
        {
            return await _context.ProductSelectedCategories
                .AsQueryable()
                .Where(c => c.ProductId == productId)
                .Select(a => a.ProductCategoryId)
                .ToListAsync();
        }

        public async Task<List<long>> GetAllProductCategoryId()
        {
            return await _context.ProductCategories.AsQueryable().Select(c => c.Id).ToListAsync();
        }

     

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<FilterProductsViewModel> FilterProduct(FilterProductsViewModel filter)
        {
            var query = _context.Products
                .Include(c => c.ProductSelectedCategories)
                .ThenInclude(c => c.ProductCategory)
                .AsQueryable();

            #region filter

            if (!string.IsNullOrEmpty(filter.ProductName))
            {
                query = query.Where(s => EF.Functions.Like(s.Name, $"%{filter.ProductName}%"));
            }

            if (!string.IsNullOrEmpty(filter.FilterByCategory))
            {
                query = query.Where(a => a.ProductSelectedCategories.Any(d =>
                         d.ProductCategory.UrlName == filter.FilterByCategory));
            }

            #endregion

            #region State

            switch (filter.ProductOrder)
            {
                case ProductOrder.All:

                    break;
                case ProductOrder.ProductNewss:
                    query = query.Where(c => c.IsActive).OrderByDescending(s => s.CreateDate);
                    break;
                case ProductOrder.ProductExp:
                    query = query.Where(c => c.IsActive).OrderByDescending(s => s.Price);
                    break;
                case ProductOrder.ProductInExprnsive:
                    query = query.Where(c => c.IsActive).OrderBy(s => s.Price);
                    break;
            }

            switch (filter.ProductState)
            {
                case ProductState.All:
                    query = query.Where(q => !q.IsDelete);
                    break;
                case ProductState.IsActice:
                    query = query.Where(q => q.IsActive);
                    break;
                case ProductState.Delete:
                    query = query.Where(q => q.IsDelete);
                    break;
            }

            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            #region ProductBox

            switch (filter.ProductBox)
            {
                case ProductBox.Default:
                    break;
                case ProductBox.ItemBoxInSite:
                    var pagerBox = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
                    var allDataBox = await query.Paging(pagerBox).Select(a => new ProductItemViewModel()
                    {
                        Price = a.Price,
                        NewPrice = (int)a.NewPrice,
                        ProductId = a.Id,
                        ProductImageName1 = a.ProductImageName,
                        ProductImageName2 = a.ProductImageName2,
                        ProductName = a.Name,
                        IsActive = a.IsActive,
                        Discount = a.Discount,
                        ProductCategory = a.ProductSelectedCategories.Select(c => c.ProductCategory).First(),
                    }).ToListAsync();
                    return filter.SetPaging(pagerBox).SetProductsItem(allDataBox);
            }


            #endregion

            return filter.SetPaging(pager).SetProducts(allData);
        }

        public async Task RemoveProductSelectedcategory(long productId)
        {
            var productselectedcat = await _context.ProductSelectedCategories.AsQueryable()
                .Where(c => c.ProductId == productId).ToListAsync();
            if (productselectedcat.Any())
            {
                _context.ProductSelectedCategories.RemoveRange(productselectedcat);
            }

        }

        public async Task CreateProductSelectedcategory(List<long> productSelectedCategory, long productId)
        {
            if (productSelectedCategory != null && productSelectedCategory.Any())
            {
                var newProductselectedCat = new List<ProductSelectedCategories>();
                foreach (var categorId in productSelectedCategory)
                {
                    newProductselectedCat.Add(new ProductSelectedCategories()
                    {
                        ProductId = productId,
                        ProductCategoryId = categorId
                    });
                }

                await _context.AddRangeAsync(newProductselectedCat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await _context.Products.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == productId);
        }

        public async Task<bool> DeleteProduct(long productId)
        {
            var currentproduct = await _context.Products.AsQueryable()
                .Where(c => c.Id == productId)
                .SingleOrDefaultAsync();
            if (currentproduct != null)
            {
                currentproduct.IsDelete = true;
                _context.Products.Update(currentproduct);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RecoverProduct(long productId)
        {
            var currentproduct = await _context.Products.AsQueryable()
                .Where(c => c.Id == productId)
                .SingleOrDefaultAsync();
            if (currentproduct != null)
            {
                currentproduct.IsDelete = false;
                _context.Products.Update(currentproduct);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task AddProductGalleries(List<ProductGalleries> productGalleries)
        {
            await _context.ProductGalleries.AddRangeAsync(productGalleries);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsProduct(long productId)
        {
            return await _context.Products.AsQueryable().AnyAsync(c => c.Id == productId);
        }

        public async Task<List<ProductGalleries>> GetAllProductGallry(long productId)
        {
            return await _context.ProductGalleries.AsQueryable()
                .Where(c => c.ProductId == productId && !c.IsDelete).ToListAsync();
        }

        public async Task<ProductGalleries> GetProductGalleryById(long id)
        {
            return await _context.ProductGalleries.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteProductGallery(long galleryId)
        {
            var image = await _context.ProductGalleries.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == galleryId);
            if (image != null)
            {
                //image.IsDelete = true;
                //_context.ProductGalleries.Update(image);
                _context.ProductGalleries.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddProductFeature(ProductFeature productFeature)
        {
            await _context.ProductFeatures.AddAsync(productFeature);
        }

        public async Task<List<ProductFeature>> GetAllProductFeature(long productId)
        {
            return await _context.ProductFeatures.AsQueryable()
                .Where(c => c.ProductId == productId && !c.IsDelete)
                .ToListAsync();
        }

        public async Task DeleteProductFeature(long id)
        {
            var currentFeature = await _context.ProductFeatures.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (currentFeature != null)
            {
                currentFeature.IsDelete = true;
                _context.ProductFeatures.Update(currentFeature);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductItemViewModel>> GetAllProductInSlider()
        {
            return await _context.Products.Include(s => s.ProductSelectedCategories)
                .ThenInclude(a => a.ProductCategory)
                .Include(c => c.ProductGalleries)
                .AsQueryable()
                .Select(a => new ProductItemViewModel()
                {
                    Price = a.Price,
                    NewPrice = (int)a.NewPrice,
                    ProductId = a.Id,
                    ProductImageName1 = a.ProductImageName,
                    ProductImageName2 = a.ProductImageName2,
                    ProductName = a.Name,
                    IsActive = a.IsActive,
                    Discount = a.Discount,
                    Images = a.ProductGalleries.Select(s => s.ImageName).Take(3).ToList(),

                    ProductCategory = a.ProductSelectedCategories.Select(c => c.ProductCategory).First()


                }).ToListAsync();
        }

        public async Task<List<ProductItemViewModel>> GetAllProductInCategory(string hrefName,long parentId)
        {

            var products = await _context.Products.AsQueryable()
                .Include(c => c.ProductSelectedCategories)
                .ThenInclude(c => c.ProductCategory)
                .Include(c => c.ProductGalleries)
                .Where(c => c.ProductSelectedCategories.Any(c => c.ProductCategory.UrlName == hrefName||c.ProductCategory.ParentId==parentId))
                .ToListAsync();

            var data = products.Select(a => new ProductItemViewModel()
            {
                Price = a.Price,
                NewPrice = (int)a.NewPrice,
                ProductId = a.Id,
                ProductImageName1 = a.ProductImageName,
                ProductImageName2 = a.ProductImageName2,
                ProductName = a.Name,
                IsActive = a.IsActive,
                Discount = a.Discount,
                Images = a.ProductGalleries.Select(s => s.ImageName).Take(3).ToList(),
                ProductCategory = a.ProductSelectedCategories.Select(c => c.ProductCategory).First(),
            }).ToList();
            return data;
        }

        public async Task<List<ProductItemViewModel>> NewProduct()
        {
            var newproduct = await _context.Products.Include(c => c.ProductSelectedCategories)
                .ThenInclude(c => c.ProductCategory)
                .Include(c => c.ProductGalleries)
                .AsQueryable()
                .OrderByDescending(c => c.CreateDate)
                .Select(a => new ProductItemViewModel()
                {
                    Price = a.Price,
                    ProductId = a.Id,
                    ProductImageName1 = a.ProductImageName,
                    ProductImageName2 = a.ProductImageName2,
                    ProductName = a.Name,
                    IsActive = a.IsActive,
                    Discount = a.Discount,
                    ProductCategory = a.ProductSelectedCategories.Select(c => c.ProductCategory).First(),
                    NewPrice = (int)a.NewPrice,
                    Images = a.ProductGalleries.Select(s => s.ImageName).Take(3).ToList(),
                })
                .Take(8)
                .ToListAsync();
            return newproduct;
        }

        public async Task<List<ProductItemViewModel>> RelatedProduct(string hrefName, long productId)
        {
            var products = await _context.Products.AsQueryable()
                .Include(c => c.ProductComments)
                .Include(c => c.ProductSelectedCategories)
                .ThenInclude(c => c.ProductCategory)
                .Include(c => c.ProductGalleries)
                .Where(c => c.ProductSelectedCategories.Any(c => c.ProductCategory.UrlName == hrefName) && c.Id != productId)
                .ToListAsync();
            var data = products.Select(a => new ProductItemViewModel()
            {
                ProductComments = a.ProductComments.Count,
                Price = a.Price,
                NewPrice = (int)a.NewPrice,
                ProductId = a.Id,
                ProductImageName1 = a.ProductImageName,
                ProductImageName2 = a.ProductImageName2,
                ProductName = a.Name,
                IsActive = a.IsActive,
                Discount = a.Discount,
                Images = a.ProductGalleries.Select(s => s.ImageName).Take(3).ToList(),
                ProductCategory = a.ProductSelectedCategories.Select(c => c.ProductCategory).First(),
            })
                .Take(10)
                .ToList();
            return data;
        }

        public async Task<ProductDetailViewModel> ShowProductDetail(long productId)
        {
            return await _context.Products.Include(c => c.ProductSelectedCategories)
                .ThenInclude(c => c.ProductCategory)
                .Include(c => c.ProductFeatures)
                .Include(c => c.ProductGalleries)
                .Include(c => c.ProductComments)
                .AsQueryable()
                .Where(c => c.Id == productId)
                .Select(c => new ProductDetailViewModel()
                {
                    ProductId = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    ProductCategory = c.ProductSelectedCategories.Select(s => s.ProductCategory).First(),
                    Description = c.Description,
                    Discount = c.Discount,
                    NewPrice = (int)c.NewPrice,
                    ProductComment = c.ProductComments.Count,
                    ProductFeatures = c.ProductFeatures.ToList(),
                    ProductImageName1 = c.ProductImageName,
                    ProductImageName2 = c.ProductImageName2,
                    ShortDescription = c.ShortDescription,
                    ProductCount = c.ProductCount,
                    ProductImages = c.ProductGalleries.Select(s => s.ImageName).Take(4).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task AddProductComments(ProductComment productComment)
        {
            await _context.ProductComments.AddAsync(productComment);
        }

        public async Task<List<ProductComment>> GetAllCommentByProductId(long productId)
        {
            return await _context.ProductComments.Include(c => c.User)
                .AsQueryable()
                .Where(c => c.ProductId == productId)
                .OrderByDescending(c => c.CreateDate)
                .Take(3)
                .ToListAsync();
        }

        public async Task<bool> CheckUrlCategory(string url)
        {
            return await _context.ProductCategories
                .AsQueryable()
                .AnyAsync(c => c.UrlName == url);
        }

        public async Task<FilterProductCategoriesViewModel> FilterProductCategory(FilterProductCategoriesViewModel filter)
        {
            var query = _context.ProductCategories.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"%{filter.Title}%"));
            }
            #endregion

            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            #endregion

            return filter.SetPaging(pager).SetProductCategories(allData);
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
        }

        public async Task<ProductCategory> GetProductCategoryById(long id)
        {
            return await _context.ProductCategories
                .AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CheckUrlCategory(string url, long categoryId)
        {
            return await _context.ProductCategories
                .AsQueryable()
                .AnyAsync(c => c.UrlName == url && c.Id != categoryId);
        }

        public async Task AddProductCategory(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
        }

        public async Task<List<ProductCategory>> GetAllProductCategory()
        {
            return await _context.ProductCategories.AsQueryable().ToListAsync();
        }

        #endregion


    }
}
