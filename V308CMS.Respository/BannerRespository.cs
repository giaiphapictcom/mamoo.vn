﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using V308CMS.Data;
using V308CMS.Data.Helpers;
using V308CMS.Data.Models;

namespace V308CMS.Respository
{
    public interface IBannerRespository
    {
        string ChangeStatus(int id);
        List<Banner> GetList(int position = 0, string site = "", bool withImg = false, int limit=1);
    }
    public class BannerRespository: IBaseRespository<Banner>, IBannerRespository
    {

        public async Task<Banner> GetFistByPosition(byte position)
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from banner in entities.Banner
                    where banner.Position == position
                    orderby banner.Order, banner.Id descending
                    select banner).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Banner>> GetListByPositionAsync(byte position, int limit =5)
        {
            using (var entities = new V308CMSEntities())
            {
                return await (from banner in entities.Banner
                        where banner.Position == position
                        orderby  banner.Order, banner.Id  descending 
                        select banner).ToListAsync();
            }

        }

        public Banner Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from banner in entities.Banner
                        where banner.Id == id
                        select banner).FirstOrDefault();
            }
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerDelete = (from banner in entities.Banner
                        where banner.Id == id
                        select banner).FirstOrDefault();
                if (bannerDelete != null)
                {
                    entities.Banner.Remove(bannerDelete);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Update(Banner data)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerUpdate = (from banner in entities.Banner
                                where banner.Id == data.Id
                                select banner).FirstOrDefault();
                if (bannerUpdate != null)
                {
                    bannerUpdate.Name = data.Name;
                    bannerUpdate.Description = data.Description;
                    bannerUpdate.Position = data.Position;
                    bannerUpdate.Width = data.Width;
                    bannerUpdate.Height = data.Height;
                    bannerUpdate.StartDate = data.StartDate;
                    bannerUpdate.EndDate = data.EndDate;
                    bannerUpdate.ImageUrl = data.ImageUrl;
                    bannerUpdate.Status  = data.Status;
                    bannerUpdate.CreatedAt = data.CreatedAt;
                    bannerUpdate.UpdatedAt = data.UpdatedAt;
                    bannerUpdate.Site = data.Site;
                    bannerUpdate.Link = data.Link;
                    bannerUpdate.Target = data.Target;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        }

        public string Insert(Banner data)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerInsert = (from banner in entities.Banner
                    where banner.Name == data.Name
                    select banner
                    ).FirstOrDefault();
                if (bannerInsert == null)
                {
                    try
                    {
                        entities.Banner.Add(data);
                        entities.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Console.Write(dbEx);
                    }
                    
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public List<Banner> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {
                return (from banner in entities.Banner
                    orderby banner.UpdatedAt descending
                    select banner
                    ).ToList();
            }
           
        }

        public List<Banner> GetAll(string site = Site.home)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from banner in entities.Banner
                        where banner.Site == site
                        orderby banner.UpdatedAt descending
                        select banner
                    ).ToList();
            }

        }

        public string ChangeStatus(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var bannerStatus = (from banner in entities.Banner
                                    where banner.Id == id
                                    select banner
               ).FirstOrDefault();
                if (bannerStatus != null)
                {
                    bannerStatus.Status = !bannerStatus.Status;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
        
        }

       

        public List<Banner> GetList(int position = 0,string site= Site.home, bool withImg = false,int limit = 1)
        {
            var banners = new List<Banner>();
            using (var entities = new V308CMSEntities())
            {

                try {
                    var items = entities.Banner.Select(b=>b);
                    if (site == Site.home)
                    {
                        items = items.Where(b => b.Site == site || b.Site == "" || b.Site == null || b.Site =="1");
                    }
                    else {
                        items = items.Where(b => b.Site == site.Trim());
                    }

                    if (position >= 0)
                    {
                        items = items.Where(b => b.Position ==position);
                    }
                    if (withImg)
                    {
                        items = items.Where(b => b.ImageUrl.Length > 0);
                    }

                    //if (items.Any() )
                    //{


                    if (limit > 0)
                        {
                        items = items.Take(limit);
                        }
                    banners = items.OrderBy(b => b.Order).ToList();
                    //}
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Console.Write(dbEx);
                }

            }
            return banners;
        }
    }
}
