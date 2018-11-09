using ee.Framework;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact;

namespace ee.iLawyer.ViewModels
{
    public static class GlobalViewModel
    {

        public static Dictionary<int, string> Genders
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 0, "未定义" },
                    { 1, "男" },
                    { 2, "女" }
                };

            }

        }
        public static List<string> CourtRanks
        {
            get
            {
                return new List<string>()
                {
                    "最高法院",
                    "高级法院",
                    "中级法院",
                    "基层法院",
                };
            }

        }

        public static IList<Area> GetProvince()
        {
            try
            {
                var areas = MemoryCacheHelper.CacheItem<IList<Area>>(CacheKeys.AreaInfo,
                    delegate ()
                    {
                        var server = new CtsService();
                        var response = server.GetAreas(new GetAreasRequest());
                        if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any()??false))
                        {
                            return response.QueryList.ToList();
                        }
                        return new List<Area>();
                    },
                    new TimeSpan(12, 0, 0));//过期时间
                return areas;
            }
            catch (Exception)
            {
                return new List<Area>();
            }

        }

        public static ObservableCollection<Court> _courts;
        public static ObservableCollection<Court> Courts
        {
            get
            {
                if (_courts == null || !_courts.Any())
                {
                    try
                    {
                        _courts = MemoryCacheHelper.CacheItem(CacheKeys.Courts,
                        delegate ()
                        {
                            var server = new CtsService();
                            var response = server.QueryCourt(new QueryCourtRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any()??false))
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
                        new TimeSpan(12, 0, 0));//过期时间
                    }
                    catch (Exception)
                    {
                    }
                }
                return _courts;

            }
        }


        public static void UpdateCourts()
        {
            try
            {
                _courts = MemoryCacheHelper.CacheItem(CacheKeys.Courts,
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

            }
        }
    }
}
