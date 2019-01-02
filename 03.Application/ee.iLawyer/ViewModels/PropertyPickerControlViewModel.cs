using ee.Framework;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.UserControls;
using PropertyChanged;

namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public  class PropertyPickerControlViewModel
    {



        public static Category ConvertToCategory(PropertyItemCategory source) => new Category()
        {
            Id = source.Id,
            Code = source.Code,
            Name = source.Name,
            Icon = source.Icon,
        };
        public static PickerProperty ConvertToPickerProperty(PropertyItemCategory source) => new PickerProperty()
        {
            PickerType = source.Name == MainPrpoertyCategory.DateTime.ToString() ? PickerType.DateTime : PickerType.Text,
            PropertyName = source.Name,
            Icon = source.Icon,
            CategorySource = source.Children?.ToList()?.Select(ConvertToCategory)?.ToList()
        };



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

        private static PickerProperty _phonePickerProperty;
        public static PickerProperty PhonePickerProperty
        {
            get
            {
                if (_phonePickerProperty == null)
                {
                    _phonePickerProperty = GetPickerProperty(MainPrpoertyCategory.Phone);
                }
                return _phonePickerProperty;
            }
        }

        private static PickerProperty _emailPickerProperty;
        public static PickerProperty EmailPickerProperty
        {
            get
            {
                if (_emailPickerProperty == null)
                {
                    _emailPickerProperty = GetPickerProperty(MainPrpoertyCategory.Email);
                }
                return _emailPickerProperty;
            }
        }

        private static PickerProperty _addressPickerProperty;
        public static PickerProperty AddressPickerProperty
        {
            get
            {
                if (_addressPickerProperty == null)
                {
                    _addressPickerProperty = GetPickerProperty(MainPrpoertyCategory.Address);
                }
                return _addressPickerProperty;
            }
        }

        private static PickerProperty _certificatePickerProperty;
        public static PickerProperty CertificatePickerProperty
        {
            get
            {
                if (_certificatePickerProperty == null)
                {
                    _certificatePickerProperty = GetPickerProperty(MainPrpoertyCategory.Certificate);
                }
                return _certificatePickerProperty;
            }
        }

        private static PickerProperty _personPickerProperty;
        public static PickerProperty PersonPickerProperty
        {
            get
            {
                if (_personPickerProperty == null)
                {
                    _personPickerProperty = GetPickerProperty(MainPrpoertyCategory.Person);
                }
                return _personPickerProperty;
            }
        }

        private static PickerProperty _dateTimePickerProperty;
        public static PickerProperty DateTimePickerProperty
        {
            get
            {
                if (_dateTimePickerProperty == null)
                {
                    _dateTimePickerProperty = GetPickerProperty(MainPrpoertyCategory.DateTime);
                }
                return _dateTimePickerProperty;
            }
        }




    }
}
