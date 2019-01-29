using ee.Framework;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ee.iLawyer.ViewModels
{
    public static class GlobalViewModel
    {
        public static Dictionary<int, string> Appellation
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 0, " " },
                    { 1, "先生" },
                    { 2, "女士" }
                };

            }

        }
        public static Dictionary<int, string> Genders
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 0, " " },
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
                        },
                        new TimeSpan(12, 0, 0));//过期时间
                    return areas;
                }
                catch (Exception)
                {
                    return new List<Area>();
                }
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
                Console.WriteLine(ex.Message);
            }
        }




















        #region PropertyPicker


        public static Category ConvertToCategory(PropertyItemCategory source) => new Category()
        {
            Id = source.Id,
            Code = source.Code,
            Name = source.Name,
            Icon = source.Icon,
        };
        public static PickerProperty ConvertToPickerProperty(PropertyItemCategory source) => new PickerProperty()
        {
            PickerType = (PickerType)source.PickerType,
            PropertyName = source.Name,
            Icon = source.Icon,
            CategorySource = source.Children?.ToList()?.Select(ConvertToCategory)?.ToList()
        };



        public static ObservableCollection<Category> GetPersonPropertyCategories()
        {
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
                    },
                    new TimeSpan(12, 0, 0));//过期时间


                var list = new ObservableCollection<Category>();

                categories.ToList().ForEach(x => list.Add(DtoConverter.Convert(x)));
                //foreach (var category in categories)
                //{
                //    var pickerProperty = ConvertToPickerProperty(category);
                //    list.Add(pickerProperty);
                //}

                return list;
            }
            catch (Exception)
            {
                return new ObservableCollection<Category>();
            }
        }

        private static ObservableCollection<Category> personPropertyCategories;
        public static ObservableCollection<Category> PersonPropertyCategories
        {
            get
            {
                if (personPropertyCategories == null)
                {
                    personPropertyCategories = new ObservableCollection<Category>(GetPersonPropertyCategories());
                }
                return personPropertyCategories;
            }
        }


        public static PickerProperty GetPickerProperty(MainPrpoertyCategory categoryType)
        {
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
                    },
                    new TimeSpan(12, 0, 0));//过期时间
                var category = categories.FirstOrDefault(x => x.Code == categoryType.ToString());
                var propertyItem = ConvertToPickerProperty(category);
                return propertyItem;
            }
            catch (Exception)
            {
                return new PickerProperty();
            }
        }
        public static IList<PickerProperty> GetPickerProperties()
        {
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
                    },
                    new TimeSpan(12, 0, 0));//过期时间


                var list = new List<PickerProperty>();
                foreach (var category in categories)
                {
                    var pickerProperty = ConvertToPickerProperty(category);
                    list.Add(pickerProperty);
                }

                return list;
            }
            catch (Exception)
            {
                return new List<PickerProperty>();
            }
        }

        private static ObservableCollection<PickerProperty> pickerProperties;
        public static ObservableCollection<PickerProperty> PickerProperties
        {
            get
            {
                if (pickerProperties == null)
                {
                    pickerProperties = new ObservableCollection<PickerProperty>(GetPickerProperties());
                }
                return pickerProperties;
            }
        }
        public static ObservableCollection<PropertyListItem> GetPropertyListItems()
        {
            try
            {
                var categories = MemoryCacheHelper.CacheItem<List<PropertyItemCategory>>(CacheKeys.PropertyItemCategories,
                    delegate ()
                    {
                        var server = new CtsService();
                        var response = server.GetPropertyItemCategories(new GetPropertyItemCategoriesRequest());
                        if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                        {
                            return response.QueryList.ToList();
                        }
                        return new List<PropertyItemCategory>();
                    },
                    new TimeSpan(12, 0, 0));//过期时间


                var list = new ObservableCollection<PropertyListItem>();
                foreach (var category in categories)
                {
                    var pickerProperty = ConvertToPickerProperty(category);
                    list.Add(new PropertyListItem()
                    {
                        PickerProperty = pickerProperty,
                        Items = new ObservableCollection<Models.PropertyPickerItem>()
                        {
                            new Models.PropertyPickerItem()
                            {
                                Guid=Guid.NewGuid(),
                                 IsDefault=true,
                            }
                        },
                    }
                    );
                }

                return list;
            }
            catch (Exception)
            {
                return new ObservableCollection<PropertyListItem>();
            }
        }


        private static ObservableCollection<PropertyListItem> propertyListItems;
        public static ObservableCollection<PropertyListItem> PropertyListItems
        {
            get
            {
                if (propertyListItems == null)
                {
                    propertyListItems = new ObservableCollection<PropertyListItem>(GetPropertyListItems());
                }
                return propertyListItems;
            }
        }



        private static PropertyListItem propertyListItemOfPhone;
        public static PropertyListItem PropertyListItemOfPhone
        {
            get
            {
                if (propertyListItemOfPhone == null)
                {
                    propertyListItemOfPhone = GetPropertyListItems().FirstOrDefault(x => x.PickerProperty.PropertyName == "电话");
                }
                propertyListItemOfPhone.Items = new ObservableCollection<Models.PropertyPickerItem>()
                {
                    new Models.PropertyPickerItem()
                    {
                         Guid=Guid.NewGuid(),
                          IsDefault=true,

                    }
                };
                return propertyListItemOfPhone;
            }
        }

        private static PropertyListItem propertyListItemOfEmail;
        public static PropertyListItem PropertyListItemOfEmail
        {
            get
            {
                if (propertyListItemOfEmail == null)
                {
                    propertyListItemOfEmail = GetPropertyListItems().FirstOrDefault(x => x.PickerProperty.PropertyName == "邮箱");
                }
                propertyListItemOfEmail.Items = new ObservableCollection<Models.PropertyPickerItem>()
                {
                    new Models.PropertyPickerItem()
                    {
                         Guid=Guid.NewGuid(),
                          IsDefault=true,

                    }
                };
                return propertyListItemOfEmail;
            }
        }
        private static PropertyListItem propertyListItemOfAddress;
        public static PropertyListItem PropertyListItemOfAddress
        {
            get
            {
                if (propertyListItemOfAddress == null)
                {
                    propertyListItemOfAddress = GetPropertyListItems().FirstOrDefault(x => x.PickerProperty.PropertyName == "地址");
                }
                propertyListItemOfAddress.Items = new ObservableCollection<Models.PropertyPickerItem>()
                {
                    new Models.PropertyPickerItem()
                    {
                         Guid=Guid.NewGuid(),
                          IsDefault=true,

                    }
                };
                return propertyListItemOfAddress;
            }
        }
        private static PropertyListItem propertyListItemOfCertificate;
        public static PropertyListItem PropertyListItemOfCertificate
        {
            get
            {
                if (propertyListItemOfCertificate == null)
                {
                    propertyListItemOfCertificate = GetPropertyListItems().FirstOrDefault(x => x.PickerProperty.PropertyName == "证件");
                }
                propertyListItemOfCertificate.Items = new ObservableCollection<Models.PropertyPickerItem>()
                {
                    new Models.PropertyPickerItem()
                    {
                         Guid=Guid.NewGuid(),
                          IsDefault=true,

                    }
                };
                return propertyListItemOfCertificate;
            }
        }
        private static PropertyListItem propertyListItemOfPerson;
        public static PropertyListItem PropertyListItemOfPerson
        {
            get
            {
                if (propertyListItemOfPerson == null)
                {
                    propertyListItemOfPerson = GetPropertyListItems().FirstOrDefault(x => x.PickerProperty.PropertyName == "重要人物");
                }
                propertyListItemOfPerson.Items = new ObservableCollection<Models.PropertyPickerItem>()
                {
                    new Models.PropertyPickerItem()
                    {
                         Guid=Guid.NewGuid(),
                          IsDefault=true,

                    }
                };
                return propertyListItemOfPerson;
            }
        }
        private static PropertyListItem propertyListItemOfDateTime;
        public static PropertyListItem PropertyListItemOfDateTime
        {
            get
            {
                if (propertyListItemOfDateTime == null)
                {
                    propertyListItemOfDateTime = GetPropertyListItems().FirstOrDefault(x => x.PickerProperty.PropertyName == "重要日期");
                }
                propertyListItemOfDateTime.Items = new ObservableCollection<Models.PropertyPickerItem>()
                {
                    new Models.PropertyPickerItem()
                    {
                         Guid=Guid.NewGuid(),
                          IsDefault=true,

                    }
                };
                return propertyListItemOfDateTime;
            }
        }


        #endregion













    }
}
