
using ee.Framework.Logging;
using ee.Framework.Processor;
using ee.Framework.Schema;
using ee.SessionFactory;
using System.Reflection;

namespace ee.iLawyer.Ops
{
    public class ServiceProcessor
    {
        public static ApiProcessor<T, TR> CreateProcessor<T, TR>(MethodBase methodBase, dynamic request, bool parameterRequired = true)
             where T : BaseRequest
             where TR : BaseResponse, new()
        {
            var processor = new ApiProcessor<T, TR>(methodBase);

            processor.Input(request, parameterRequired);
            //processor.Inbound(() => { SessionManager.GetConnection(); });
            processor.Outbound(() => { SessionManager.CloseConnection(); });
            return processor;
        }

        public static ApiProcessor<T, TR> CreateProcessor<T, TR>(MethodBase methodBase)
            where T : BaseRequest
            where TR : BaseResponse, new()
        {
            var processor = new ApiProcessor<T, TR>(methodBase);
            return processor;
        }

    }

    public class ApiProcessor<TRequest, TResponse> : ProcessorInternal<TRequest, TResponse>
             where TRequest : BaseRequest
             where TResponse : BaseResponse, new()
    {

        public ApiProcessor(MethodBase method)
        {
            MethodBase = method;
        }
        public override void Debug(object message)
        {
            Logger.Debug(message);
        }

        public override void Error(object message)
        {
            Logger.Error(message);
        }

        public override void Fatal(object message)
        {
            Logger.Fatal(message);
        }

        public override void Info(object message)
        {
            Logger.Info(message);
        }

        public override void Warn(object message)
        {
            Logger.Warn(message);
        }
    }

}
