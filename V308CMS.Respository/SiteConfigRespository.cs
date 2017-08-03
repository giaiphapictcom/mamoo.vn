﻿using System.Collections.Generic;
using System.Linq;
using V308CMS.Data;
using V308CMS.Data.Helpers;

namespace V308CMS.Respository
{
    public interface ISiteConfigRespository
    {
    }
    public class SiteConfigRespository: IBaseRespository<SiteConfig>, ISiteConfigRespository
    {
       
        public SiteConfigRespository()
        {
           
        }
     
        public SiteConfig Find(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from config in entities.SiteConfig
                        where config.Id == id
                        select config).FirstOrDefault();
            }
            
        }

        public string Delete(int id)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.Id == id
                                  select config).FirstOrDefault();
                if (configItem != null)
                {
                    entities.SiteConfig.Remove(configItem);
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";

            }
          
        }

        public string Update(SiteConfig data)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.Id == data.Id
                                  select config).FirstOrDefault();
                if (configItem != null)
                {
                    configItem.Name = data.Name;
                    configItem.Content = data.Content;
                    entities.SaveChanges();
                    return "ok";
                }
                return "not_exists";
            }
           
        }

        public string Insert(SiteConfig data)
        {
            using (var entities = new V308CMSEntities())
            {
                var configItem = (from config in entities.SiteConfig
                                  where config.Name == data.Name
                                  select config).FirstOrDefault();
                if (configItem == null)
                {
                    entities.SiteConfig.Add(data);
                    entities.SaveChanges();
                    return "ok";
                }
                return "exists";
            }
           
        }

        public List<SiteConfig> GetAll()
        {
            using (var entities = new V308CMSEntities())
            {

                return (from config in entities.SiteConfig
                        orderby config.Id descending
                        select config
                ).ToList();
            }
        }
        public List<SiteConfig> GetAll(string site=Site.home)
        {
            using (var entities = new V308CMSEntities())
            {
                var items = entities.SiteConfig.Select(c=>c);
                //return (from config in entities.SiteConfig
                //        orderby config.Id descending
                //        select config
                //).ToList();
                if (site == Site.home)
                {
                    items = items.Where(c => c.Site == site || c.Site == "" || c.Site == "1" || c.Site == null);
                }
                else {
                    items = items.Where(c=>c.Site==site);
                }
                return items.OrderByDescending(c => c.Id).ToList();
            }
            
        }
    }
}