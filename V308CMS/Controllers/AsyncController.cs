﻿using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using V308CMS.Data;
using V308CMS.Data.Enum;
using V308CMS.Data.Models;

namespace V308CMS.Controllers
{
    public class AsyncController : BaseController
    {       
        public PartialViewResult LoadListBrandAsync(int categoryId, int limit = 6)
        {
            return PartialView("_ListBrandAsync",  ProductBrandService.GetRandom(categoryId, limit));
        }

        public async Task<PartialViewResult> LoadListProductByCategoryAsync(int categoryId, int limit = 6)
        {
            return PartialView("_ListProductByCategoryAsync", await BoxContentService.GetListProductByCategory(categoryId, limit));

        }
        [ChildActionOnly]
        public async Task<PartialViewResult> LoadBoxItemAsync(ProductType category, int subCategoryLimit = 3, int productLimit = 6)
        {

            var listSubcategory = await ProductTypeService.GetListSubByParentIdAsync(category.ID, subCategoryLimit);
            var boxContent = new BoxContent
            {
                Category = category,
                ListSubCategory = listSubcategory
            };
            for (int k = 0, subTotal = listSubcategory.Count; k < subTotal; k++)
            {
                var subCategory = listSubcategory[k];
                var boxContentItem = new BoxContentItem
                {
                    SubCategory = subCategory,
                    Products = await ProductsService.GetListByCategoryWithImagesAsync(subCategory.ID, productLimit)                   
                };
                boxContent.ListContentItem.Add(boxContentItem);

            }
            return PartialView("_BoxItem", boxContent);

        }

        public async Task<PartialViewResult> LoadProductRelatived(int productId, int categoryId, int limit =12)
        {
            return PartialView("_ListProductRelativedAsync", await ProductsService.GetListRelativedAsync(productId, categoryId,limit,true));
        }


        public async Task<PartialViewResult> LoadProductsBestSellerAsync(int categoryId, int limit =5 )
        {

            return PartialView("_ListProductBestSellerAsync", await ProductsService.GetProductsBestSellerAsync(categoryId));
        }

        public async Task<PartialViewResult> LoadListProductBrandFilterAsync(int categoryId, RouteValueDictionary currentRouteData)
        {
            ViewBag.CurrentRouteData = currentRouteData;
            return PartialView("_ListProductBrandFilterAsync", await ProductBrandService.GetListByCategoryIdAsync(categoryId));

        }

        public async Task<PartialViewResult> LoadListManufacturerFilterAsync(RouteValueDictionary currentRouteData)
        {
            ViewBag.CurrentRouteData = currentRouteData;
            return PartialView("_ListProductManufacturerFilterAsync", await ProductManufacturerService.GetAllAsync());
        }

        public PartialViewResult LoadListPriceFilterAsync(RouteValueDictionary currentRouteData)
        {
            ViewBag.CurrentRouteData = currentRouteData;
            return PartialView("_ListProductPriceFilterAsync");
        }

        public async Task<PartialViewResult> LoadHomeSliderAsync(int limit =5, byte position =(byte)BannerPositionEnum.HomeSlider)
        {
            return PartialView("HomeSlides", await BannerService.GetListByPositionAsync(position));
        }
        public async Task<PartialViewResult> LoadBannerHomeTopAsync(int limit = 5, byte position = (byte)BannerPositionEnum.HomeTop)
        {
            return PartialView("_LeftBannerHomeTopAsyn", await BannerService.GetListByPositionAsync(position, limit));
        }

        public async Task<PartialViewResult> LoadLeftBannerAsync(byte position)
        {
            return PartialView("_LeftBannerAsyn", await BannerService.GetFistByPosition(position));
        }
        public async Task<PartialViewResult> LoadBigSaleTopBannerAsync(byte position)
        {
            return PartialView("_BannerBigSaleAsync", await BannerService.GetFistByPosition(position));
        }

        public async Task<PartialViewResult> LoadListVideoHomeAsync(byte position =(byte)VideoPosition.Home, int limit =3)
        {
            return PartialView("_ListVideoHomeAsync", await VideoService.GetListHomeVideo(position, limit));
        }

        public async Task<PartialViewResult> LoadListVideoRelativedAsync(int id, int limit =10)
        {
            return PartialView("_ListVideoHomeAsync", await VideoService.GetListRelatived(id, limit));
        }

    }
}