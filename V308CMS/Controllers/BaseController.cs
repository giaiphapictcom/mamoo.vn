﻿using System.Linq;
using System.Web.Mvc;
using V308CMS.Data;
using V308CMS.Helpers;
using V308CMS.Helpers.Url;
using V308CMS.Models;
using V308CMS.Respository;
using V308CMS.Social;

namespace V308CMS.Controllers
{
    public abstract class BaseController : Controller
    {
        public int PageSize = 10;
        /// <summary>
        /// Gio hang
        /// </summary>
        protected ShoppingCart MyCart => ShoppingCart.Instance;

        /// <summary>
        /// Xoa gio hang, duoc su dung khi qua trinh mua ban hoan tat
        /// </summary>
        protected void ClearCart()
        {
            MyCart.Clear();
        }
        /// <summary>
        /// Kiem tra gio hang trong
        /// </summary>
        /// <returns></returns>
        protected bool IsEmptyCart()
        {
            return MyCart.Items.Count == 0;

        }

        /// <summary>
        /// Xoa san pham trong gio hang
        /// </summary>
        /// <param name="itemId"></param>
        protected void RemoveItemInCart(int itemId)
        {
            MyCart.RemoveItem(new ProductModels { Id = itemId });
        }
        protected string TransactionId => GetTempSession("TransactionId");
        /// <summary>
        /// Callback Url
        /// </summary>

        protected string ReturnUrl
        {
            get { return GetTempSession("ReturnUrl"); }
            set { if (!string.IsNullOrEmpty(value)) { SetTempSession("ReturnUrl", value); } }
        }
        protected ActionResult RedirectToUrl(string url, string defaultUrl = "/")
        {
            if (url.IsLocalUrl())
            {
                return Redirect(url);
            }
            return Redirect(defaultUrl);

        }
        /// <summary>
        /// Xac nhan bat dau mot giao dich
        /// </summary>
        /// <param name="transactionId"></param>
        protected void BeginTransaction(string transactionId)
        {
            SetTempSession("TransactionId", transactionId);
        }

        /// <summary>
        /// Ghi nhan da hoan tat giao dich
        /// </summary>
        protected void EndTransaction()
        {
            ResetTempSession("TransactionId");
        }
        /// <summary>
        /// Thong tin User dang nhap
        /// </summary>
        protected virtual new CustomPrincipal User => HttpContext.User as CustomPrincipal;
        /// <summary>
        /// Load cac config can su dung
        /// </summary>
        private void LoadSiteConfig()
        {
            var siteConfigs = SiteConfig.ConfigTable;
            if (siteConfigs.Any())
            {
                ViewBag.domain = Theme.domain;
                ViewBag.ThemesPath = "/Content/themes/" + Theme.domain;
                ViewBag.MoneyShort = "đ";
                ViewBag.SiteName = SiteConfigService.ReadSiteConfig(siteConfigs, "site-name");
                ViewBag.Hotline = SiteConfigService.ReadSiteConfig(siteConfigs, "hotline");
                ViewBag.CompanyFullname = SiteConfigService.ReadSiteConfig(siteConfigs, "company-fullname");
                ViewBag.CompanyEmail = SiteConfigService.ReadSiteConfig(siteConfigs, "company-email");
                ViewBag.CompanyPosition = SiteConfigService.ReadSiteConfig(siteConfigs, "company-position");
                ViewBag.FooterCompanyContact = SiteConfigService.ReadSiteConfig(siteConfigs, "company-footer-contact");
                ViewBag.CompanyHeaderAddress = SiteConfigService.ReadSiteConfig(siteConfigs, "company-header-address");
                ViewBag.FacebookPage = SiteConfigService.ReadSiteConfig(siteConfigs, "facebook-page");
                ViewBag.GPlus = SiteConfigService.ReadSiteConfig(siteConfigs, "gplus");
                ViewBag.Zalo = SiteConfigService.ReadSiteConfig(siteConfigs, "zalo");
                ViewBag.Youtube = SiteConfigService.ReadSiteConfig(siteConfigs, "youtube-channel");
                ViewBag.ProductViewText = SiteConfigService.ReadSiteConfig(siteConfigs, "product-text-view");
                ViewBag.HomeAliasText = SiteConfigService.ReadSiteConfig(siteConfigs, "home-text-alias");
                ViewBag.SubscribeNews = SiteConfigService.ReadSiteConfig(siteConfigs, "subscribe-news");
                
            }

        }
        /// <summary>
        /// Khoi tao cac service can su dung
        /// </summary>
        protected BaseController()
        {
            ProductsService = new ProductRepository();
            NewsService = new NewsRepository();
            AccountService = new AccountRepository();
            FileService = new FileRepository();
            ProductWishlistService = new ProductWishlistRepositry();
            ImagesService = new ImagesRepository();
            MarketService = new MarketRepository();
            ContactService = new ContactRepository();
            CartService = new CartRepository();
            CartItemService = new CartItemRepository();
            MenuConfigService = new MenuConfigRespository();
            ProductTypeService = new Data.ProductTypeRepository();
            SiteConfigService = new Data.SiteConfigRespository();
            BoxContentService = new BoxContentRespository();
            RegionService = new RegionRespository();
            ShippingService = new ShippingAddressRespository();
            OrderTransactionService = new OrderTransactionRespository();
            BannerService = new BannerRespository();
            ProductBrandService = new ProductBrandRespository();
            ProductManufacturerService = new ProductManufacturerRespository();
            OrderService = new ProductOrderRespository();
            LinkRepo = new LinkRepository();
            TestimonialService = new TestimonialRepository();
            AffiliateCategoryRepo = new AffiliateCategoryRespository();
            VoucherRepo = new CouponRepository();
            RevenueGainRepo = new RevenueGainRepository();
            WebsiteRequestRepo = new WebsiteRequestRepository();
            VoucherService = new VoucherRespository();
            AffilateUserService = new AffilateUserRespository();
            VoucherLogService = new VoucherLogRespository();
            VideoService = new VideoRespository();
            GoogleplusService = new GoogleplusService(ConfigHelper.GoogleAppId, ConfigHelper.GoogleAppSecret);

            //FacebookService = new FacebookService(ConfigHelper.FacebookAppId, ConfigHelper.FacebookAppSecret);
            SiteConfigRepo = new Data.SiteConfigRespository();
            SubscribeRepo = new SubscribeRepository();
            //LoadSiteConfig();
            LinkRepo = new LinkRepository();
            VisisterRepo = new VisisterRepository();


            FacebookService = new FacebookService(ConfigHelper.FacebookAppId,ConfigHelper.FacebookAppSecret);
            LoadSiteConfig();

            if (HttpContext != null && HttpContext.User.Identity.IsAuthenticated == true && User != null)
            {
                //lay thong tin chi tiet user
                ViewBag.Account = AccountService.LayTinTheoId(System.Int32.Parse(User.UserId.ToString()));
                ViewBag.IsAuthenticated = true;
            }


        }
        public VideoRespository VideoService { get; set; }
        public VoucherLogRespository VoucherLogService { get; set; }
  
        public AffilateUserRespository AffilateUserService { get; set; }
        public VoucherRespository VoucherService { get; set; }
        public RevenueGainRepository RevenueGainRepo { get; set; }
        public CouponRepository VoucherRepo { get; set; }
        public AffiliateCategoryRespository AffiliateCategoryRepo { get; set; }
        public TestimonialRepository TestimonialService { get; set; }
        public  ProductOrderRespository OrderService { get; set; }
        public WebsiteRequestRepository WebsiteRequestRepo { get; set; }
        public FacebookService FacebookService { get; }
        public GoogleplusService GoogleplusService { get; }
        public ProductManufacturerRespository ProductManufacturerService;
        public ProductBrandRespository ProductBrandService;
        public OrderTransactionRespository OrderTransactionService;
        public ShippingAddressRespository ShippingService { get; }
        public RegionRespository RegionService { get; }
        public BoxContentRespository BoxContentService { get; }
        public Data.SiteConfigRespository SiteConfigService { get; }
        public Data.ProductTypeRepository ProductTypeService { get; }
        public BannerRespository BannerService { get; }
        public VisisterRepository VisisterRepo { get; set; }
        public MenuConfigRespository MenuConfigService { get; }
        protected IContactRepository ContactService { get; }
        protected ImagesRepository ImagesService { get; }
        protected MarketRepository MarketService { get; }
        protected NewsRepository NewsService { get; }
        protected IProductWishlistRepositry ProductWishlistService { get; }
        protected ProductRepository ProductsService { get; }

        public SubscribeRepository SubscribeRepo { get; set; }
        protected AccountRepository AccountService { get; }

        protected FileRepository FileService { get; }


        public CartRepository CartService { get; }

        public CartItemRepository CartItemService { get; }

        public Data.SiteConfigRespository SiteConfigRepo { get; set; }
        public LinkRepository LinkRepo { get; set; }

        private void SetTempSession(string name, string value ="")
        {
            Session[name] = value;
        }

        private string GetTempSession(string name, string defaultValue = "")
        {
            return Session[name]?.ToString() ?? defaultValue;
        }

        private void ResetTempSession(string name,string defaultValue="")
        {
            Session[name] = defaultValue;
        }
    }
}
