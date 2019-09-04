using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ee.Framework.Schema
{
    public class BaseResponse
    {
        public virtual int Code { get; set; }
        public virtual string Message { get; set; }
    }

    public class BaseDataResponse : BaseResponse
    {
        [JsonProperty("Data")]
        public virtual string Data { get; protected set; }

        private object dataObject;
        [JsonIgnore]
        public virtual object DataObject
        {
            get
            {
                return dataObject;
            }
            set
            {
                dataObject = value;
                if (dataObject != null)
                {
                    Data = JsonConvert.SerializeObject(DataObject);
                }
            }
        }

    }

    public class BaseObjectResponse<T> : BaseResponse
    {
        public virtual T Object { get; set; }


    }
    public class BaseQueryResponse<T> : BaseResponse
    {
        public virtual int? Total { get; set; }

        public virtual IList<T> QueryList { get; set; }



    }

    public static class Converters
    {
        public static BaseDataResponse ToBaseDataResponse<T>(this BaseObjectResponse<T> source)
        {
            return new BaseDataResponse()
            {
                Code = source.Code,
                Message = source.Message,
                DataObject = source.Object,
            };
        }
        public static BaseObjectResponse<T> ToBaseObjectResponse<T>(this BaseDataResponse source)
        {
            return new BaseObjectResponse<T>()
            {
                Code = source.Code,
                Message = source.Message,
                Object = JsonConvert.DeserializeObject<T>(source.Data)
            };
        }
        public static BaseDataResponse ToBaseDataResponse<T>(this BaseQueryResponse<T> source)
        {
            return new BaseDataResponse()
            {
                Code = source.Code,
                Message = source.Message,
                DataObject = (source.Total, source.QueryList),
            };
        }
        public static BaseQueryResponse<T> ToBaseQueryResponse<T>(this BaseDataResponse source)
        {
            var tuple = JsonConvert.DeserializeObject<Tuple<IList<T>, int?>>(source.Data);
            return new BaseQueryResponse<T>()
            {
                Code = source.Code,
                Message = source.Message,
                QueryList = tuple.Item1,
                Total = tuple.Item2,
            };
        }

        public static BaseDataResponse ToBaseDataResponse(this object source)
        {

            var type = source.GetType();
            var underlyingSystemType = type.UnderlyingSystemType;
            if (type.Name == typeof(BaseObjectResponse<>).Name)
            {

                //var sType = typeof(BaseObjectResponse<>);
                //sType.MakeGenericType(underlyingSystemType);
                //object o = Activator.CreateInstance(sType);


                dynamic res = source;
                return new BaseDataResponse()
                {
                    Code = res.Code,
                    Message = res.Message,
                    DataObject = res.Object,
                };
            }
            else if (type.Name == typeof(BaseQueryResponse<>).Name)
            {
                dynamic res = source;
                return new BaseDataResponse()
                {
                    Code = res.Code,
                    Message = res.Message,
                    DataObject = (res.QueryList, res.Total),
                };
            }
            else if (source is BaseResponse)
            {
                var res = source as BaseResponse;
                return new BaseDataResponse()
                {
                    Code = res.Code,
                    Message = res.Message,
                    DataObject = null,
                };
            }
            else
            {
                throw new EeException(ErrorCodes.DevelopError, "Can not convert to BaseDataResponse type");
            }

        }
    }


}
