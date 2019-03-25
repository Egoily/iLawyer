using ee.Framework;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ee.iLawyer.ViewModels
{
    public static class Cacher
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public static TimeSpan ExpiredTimeSpan = new TimeSpan(12, 0, 0);
        public static void Load()
        {
            var provinces = Provinces;
            var courts = Courts;
            var projectCauses = ProjectCauses;
            var projectCategories = ProjectCategories;
        }

        /// <summary>
        /// 省市
        /// </summary>
        public static IList<Area> Provinces
        {
            get
            {
                try
                {
                    var areas = MemoryCacheHelper.CacheItem<IList<Area>>(CacheKeys.AreaInfo,
                        delegate ()
                        {
                            var server = new CtsService();
                            var response = server.GetAreas(new GetAreasRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            return new List<Area>();
                        }, ExpiredTimeSpan);
                    return areas;
                }
                catch (Exception)
                {
                    return new List<Area>();
                }
            }
        }
        public static IList<ProjectCategory> ProjectCategories
        {
            get
            {
                try
                {
                    var areas = MemoryCacheHelper.CacheItem<IList<ProjectCategory>>(CacheKeys.ProjectCategories,
                        delegate ()
                        {
                            var server = new CtsService();
                            var response = server.GetProjectCategories(new GetProjectCategoriesRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            return new List<ProjectCategory>();
                        }, ExpiredTimeSpan);
                    return areas;
                }
                catch (Exception)
                {
                    return new List<ProjectCategory>();
                }
            }
        }

        public static IList<ProjectCause> ProjectCauses
        {
            get
            {
                try
                {
                    var areas = MemoryCacheHelper.CacheItem<IList<ProjectCause>>(CacheKeys.ProjectCauses,
                        delegate ()
                        {
                            var server = new CtsService();
                            var response = server.GetProjectCauses(new GetProjectCausesRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            return new List<ProjectCause>();
                        }, ExpiredTimeSpan);
                    return areas;
                }
                catch (Exception)
                {
                    return new List<ProjectCause>();
                }
            }
        }


        #region Courts

        public static ObservableCollection<Court> courts;
        public static ObservableCollection<Court> Courts
        {
            get
            {
                if (courts == null || !courts.Any())
                {
                    try
                    {
                        courts = MemoryCacheHelper.CacheItem(CacheKeys.Courts,
                        delegate ()
                        {
                            var server = new CtsService();
                            var response = server.QueryCourt(new QueryCourtRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                            {
                                var list = new ObservableCollection<Court>();
                                response.QueryList.ToList().ForEach(x => list.Add(new Court()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Rank = x.Rank,
                                    Province = x.Province,
                                    City = x.City,
                                    County = x.County,
                                    Address = x.Address,
                                    ContactNo = x.ContactNo,
                                }));
                                return list;
                            }
                            return new ObservableCollection<Court>();
                        }, ExpiredTimeSpan);
                    }
                    catch (Exception)
                    {
                    }
                }
                return courts;
            }
        }
        public static void UpdateCourts()
        {
            try
            {
                courts = MemoryCacheHelper.CacheItem(CacheKeys.Courts,
                delegate ()
                {
                    var server = new CtsService();
                    var response = server.QueryCourt(new QueryCourtRequest());
                    if (response.Code == ErrorCodes.Ok && response.QueryList.Any())
                    {
                        var list = new ObservableCollection<Court>();
                        response.QueryList.ToList().ForEach(x => list.Add(new Court()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Rank = x.Rank,
                            Province = x.Province,
                            City = x.City,
                            County = x.County,
                            Address = x.Address,
                            ContactNo = x.ContactNo,
                        }));
                        return list;
                    }
                    return new ObservableCollection<Court>();
                },
                new TimeSpan(12, 0, 0),//过期时间
                null,
                true//立即更新
                );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion




        #region PropertyPicker

        private static ObservableCollection<Category> personPropertyCategories;
        public static ObservableCollection<Category> PersonPropertyCategories
        {
            get
            {
                if (personPropertyCategories == null)
                {
                    personPropertyCategories = new ObservableCollection<Category>();
                    try
                    {
                        var categories = MemoryCacheHelper.CacheItem<IList<PropertyItemCategory>>(CacheKeys.PropertyItemCategories,
                            delegate ()
                            {
                                var server = new CtsService();
                                var response = server.GetPropertyItemCategories(new GetPropertyItemCategoriesRequest());
                                if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                                {
                                    return response.QueryList.ToList();
                                }
                                return new List<PropertyItemCategory>();
                            }, ExpiredTimeSpan);
                        if (categories != null && categories.Any())
                        {
                            categories.ToList().ForEach(x => personPropertyCategories.Add(DtoConverter.Convert(x)));
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                return personPropertyCategories;
            }
        }
        #endregion













    }
}
