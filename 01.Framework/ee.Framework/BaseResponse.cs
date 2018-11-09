using System.Collections.Generic;

namespace ee.Framework
{
    public class BaseResponse
    {
        public virtual int Code { get; set; }
        public virtual string Message { get; set; }
    }
    public class BaseQueryResponse<T> : BaseResponse
    {
        public virtual int Total { get; set; }

        public virtual IList<T> QueryList { get; set; }
    }
}
