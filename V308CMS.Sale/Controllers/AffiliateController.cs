﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Data;
using V308CMS.Data.Models;
using V308CMS.Data.Helpers;

namespace V308CMS.Sale.Controllers
{
    public class AffiliateController : BaseController
    {

        public ActionResult Home()
        {
            try{
                CreateRepos();
                AffiliateHomePage Model = new AffiliateHomePage();
                Model.VideoCategory = NewsRepos.SearchNewsGroup("affiliate-video",Data.Helpers.Site.affiliate);
                if (Model.VideoCategory != null)
                {
                    Model.Videos = NewsRepos.LayDanhSachTinTheoGroupIdWithPage(5, Model.VideoCategory.ID);
                }

                Model.Banners = BannerRepo.GetList(-1,"affiliate",true,3);
                Model.Testimonial = CommentRepo.GetRandom(4);

                Model.Brands = ProductRepos.getRandomBrands(0, 6);
                Model.Categorys = CategoryRepo.GetItems(20);
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
            
        }
        
        public ActionResult NewsList(string CategoryAlias = "", string PageTitle="")
        {
            try
            {
                CreateRepos();
                NewsIndexPageContainer Model = new NewsIndexPageContainer();
                Model.NewsGroups = NewsRepos.SearchNewsGroupByAlias(CategoryAlias, Site.affiliate);
                if (Model.NewsGroups != null) {
                    Model.ListNews = NewsRepos.LayDanhSachTinTheoGroupId(ProductHelper.ProductShowLimit, Model.NewsGroups.ID);
                    Model.PageTitle = Model.NewsGroups.Name;
                } else {
                    InsertNewsGroupDefault(CategoryAlias);
                    Model.PageTitle = PageTitle; 
                }
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult Articles(string alias = "") {
            return NewsList(alias);
        }

        public ActionResult Videos()
        {
            try
            {
                CreateRepos();
                NewsIndexPageContainer Model = new NewsIndexPageContainer();
                Model.NewsGroups = NewsRepos.SearchNewsGroup("affiliate-video", Data.Helpers.Site.affiliate);
                if (Model.NewsGroups != null)
                {
                    Model.ListNews = NewsRepos.LayDanhSachTinTheoGroupId(ProductHelper.ProductShowLimit, Model.NewsGroups.ID);
                    Model.PageTitle = Model.NewsGroups.Name;
                }
                
                return View("Videos",Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult Video(int id)
        {

            try
            {
                CreateRepos();
                var newsItem = NewsRepos.GetById(id);
                if (newsItem == null)
                {
                    return HttpNotFound("Tin này không tồn tại trên hệ thống");
                }
                var newsDetailViewModel = new NewsDetailPageContainer
                {
                    NewsItem = newsItem,
                    NextNewsItem = NewsRepos.GetNext(id),
                };

                return View(newsDetailViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }


        private void InsertNewsGroupDefault(string NewsGroupAlias="",NewsGroups GroupParent= null) {

            var GroupItem = new NewsGroups() { Link = "", Date = DateTime.Now, Number = 0, Parent=0,Status = true, Level = "1", Alias = NewsGroupAlias, Site= V308CMS.Data.Helpers.Site.affiliate };
            

            switch (GroupItem.Alias)
            {
                case "chuong-trinh-thuc-day":
                    GroupItem.Name = "Chương trình thúc đẩy";break;
                case "huong-dan":
                    GroupItem.Name = "Hướng Dẫn"; break;
                case "quy-dinh":
                    GroupItem.Name = "Quy Định"; break;
                case "chinh-sach":
                    GroupItem.Name = "Chính Sách"; break;
                case "ho-tro":
                    GroupItem.Name = "Hỗ Trợ"; break;
                case "vinh-danh-ca-nhan":
                    GroupItem.Name = "Vinh Danh Cá Nhân"; break;
                case "top-xuat-sac":
                    GroupItem.Name = "Top Xuất Sắc"; break;
                case "he-thong":
                    GroupItem.Name = "Hệ Thống"; break;
                case "affiliate-news":
                    GroupItem.Name = "Tin tức Affiliate"; break;
            }
            if (GroupItem.Name.Length > 0 && GroupItem.Alias.Length > 0) {
                using (var mEntities = new V308CMSEntities()) {
                    mEntities.AddToNewsGroups(GroupItem);
                    mEntities.SaveChanges();

                    News NewsItem = new News() { Date = DateTime.Now, Order = 1, Status = true, Summary = "", Title = GroupItem.Name + " bài viết mẫu", TypeID = GroupItem.ID, Description = "Nội dung của " + GroupItem.Name };
                    mEntities.AddToNews(NewsItem);
                    mEntities.SaveChanges();
                }
                
            }
            
        }
        
        [AffiliateAuthorize]
        public ActionResult News(string NewsAlias = "", string PageTitle="")
        {
            try
            {
                //CreateRepos();
                NewsDetailPageContainer Model = new NewsDetailPageContainer();
                Model.NewsItem = NewsRepos.SearchNews(NewsAlias);
                if (Model.NewsItem ==null || Model.NewsItem.ID < 1)
                {
                    NewsGroups AffiliateGroup = NewsRepos.SearchNewsGroupByAlias("affiliate-news", Site.affiliate);
                    if (AffiliateGroup == null)
                    {
                        InsertNewsGroupDefault("affiliate-news");
                        AffiliateGroup = NewsRepos.SearchNewsGroupByAlias("affiliate-news", Site.affiliate);
                    }
                    string NewsTitle = "";
                    switch (NewsAlias)
                    {
                        case "ve-affiliate":
                            NewsTitle = "Về Affiliate"; break;
                        default:
                            NewsTitle = "News default title "; break;
                    }
                    News NewsItem = new News() { Date = DateTime.Now, Alias = NewsAlias, Order = 1, Status = true, Summary = NewsTitle+" mô tả ngắn", Title = NewsTitle + " bài viết mẫu", TypeID = AffiliateGroup.ID, Description = "Nội dung của " + NewsTitle };
                    using (var mEntities = new V308CMSEntities()) {
                        mEntities.AddToNews(NewsItem);
                        mEntities.SaveChanges();
                    }
                    
                }
                Model.PageTitle = PageTitle;
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        [AffiliateAuthorize]
        public ActionResult NewsItem(int id=0)
        {
            try
            {
                CreateRepos();
                NewsDetailPageContainer Model = new NewsDetailPageContainer();
     
                Model.NewsItem = NewsRepos.GetById(id);
               
                Model.PageTitle = Model.NewsItem.Title;
                return View("News", Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        [AffiliateAuthorize]
        public ActionResult NewsTable(string CategoryAlias = "", string PageTitle = "")
        {
            try
            {
                CreateRepos();
                NewsIndexPageContainer Model = new NewsIndexPageContainer();
                Model.NewsGroups = NewsRepos.SearchNewsGroupByAlias(CategoryAlias);
                if (Model.NewsGroups != null)
                {
                    Model.ListNews = NewsRepos.LayDanhSachTinTheoGroupId(ProductHelper.ProductShowLimit, Model.NewsGroups.ID);
                    Model.PageTitle = Model.NewsGroups.Name; 
                }
                else
                {
                    var GroupItem = new NewsGroups() { Link = "", Date = DateTime.Now, Number = 0, Status = true, Parent = 0, Level = "99", Alias = CategoryAlias };
                    using (var mEntities = new V308CMSEntities())
                    {
                        mEntities.AddToNewsGroups(GroupItem);
                        mEntities.SaveChanges();
                    }
                    

                    Model.PageTitle = PageTitle;
                }
                return View(Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult Article(int id)
        {
            
            try {
                CreateRepos();
                var newsItem = NewsRepos.GetById(id);
                if (newsItem == null)
                {
                    return HttpNotFound("Tin này không tồn tại trên hệ thống");
                }
                var newsDetailViewModel = new NewsDetailPageContainer
                {
                    NewsItem = newsItem,
                    NextNewsItem = NewsRepos.GetNext(id),
                };

                return View(newsDetailViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public ActionResult Code()
        {
            int page = 1;
            var pagerequest = Request.QueryString["page"];
            if (pagerequest != null) {
                page = int.Parse(pagerequest);
            }
            if (page < 1) {
                page = 1;
            }
            try
            {
                var Model = new AffiliateCodesPage();
                Model.Page = page;
                Model.Limit = 42;

                int total = 0;
                AffiliateCodeRepo.limit = Model.Limit;
                Model.Items = AffiliateCodeRepo.items(out total, Model.Page);

                Model.Total = total;
                
                return View("CodesModal", Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Content(ex.InnerException.ToString());
            }
            finally
            {
                DisposeRepos();
            }
        }

        public JsonResult codes()
        {
            var CodeItem = new List<AffilateCode>();
            //int page = 1;
            //int psize = 4 * 20;
            using (var mEntities = new V308CMSEntities())
            {
                var used = mEntities.Account.Where(a => a.affiliate_code.Length > 0 && a.affiliate_code != string.Empty && a.affiliate_code != "").ToList();
                var CodeQuery = mEntities.AffilateCode.Where(c => c.Status == 1);
                //CodeQuery = CodeQuery.Where(c=> !used.Any(u=>u.affiliate_code == c.Code));
                CodeQuery = CodeQuery.Where(c => !mEntities.Account.Any(a => a.affiliate_code == c.Code));
                //!db.Fi.Any(f => f.UserID == user.UserID)
                CodeItem = CodeQuery.OrderBy(c => c.Code).ToList();
                //CodeItem = CodeItem.Skip((page - 1) * psize).Take(psize).ToList();

            }
            return Json(new
            {
                code = 1,
                items = CodeItem
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
