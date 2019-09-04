
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
    /*
        public class BaseProcessor
        {
            protected static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            protected static readonly JsonSerializerSettings JsonSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>() { new JsonMarkBigObjectConverter() }
            };
            public MethodBase MethodBase { get; set; }
            public bool IsParameterRequired { get; set; }
            public Action InboundAction { get; set; }

        }
        public class Processor<TRequest, TResponse> : BaseProcessor
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {

            public TRequest Request { get; set; }
            public Func<TRequest, TResponse> Fun { get; private set; }

            public Processor<TRequest, TResponse> Inbound(Action inbound)
            {
                InboundAction = inbound;
                return this;
            }
            public Processor<TRequest, TResponse> Process(Func<TRequest, TResponse> fun)
            {
                Fun = fun;
                return this;
            }
            public TResponse Build()
            {
                var response = new TResponse()
                {
                    Code = ErrorCodes.UnknownError,
                    Message = "Unknown Error",
                };
                try
                {
                    if (MethodBase == null)
                    {
                        throw new EeException(ErrorCodes.DevelopError, "Null Method.");
                    }
                    Logger.Info($">>>>>>>>>>> {MethodBase.Name} ==========");
                    if (Request == null)
                    {
                        throw new EeException(ErrorCodes.NullParameter, "Parameter is null.");
                    }
                    Request.Validate();
                    InboundAction?.Invoke();
                    using (var session = SessionManager.GetConnection())
                    {
                        if (Fun != null)
                        {
                            response = Fun(Request);
                        }
                        else
                        {
                            throw new EeException(ErrorCodes.DevelopError, "Null Fun.");
                        }
                    }

                }
                catch (EeException ex)
                {
                    response.Code = ex.ErrorCode;
                    response.Message = ex.Message;
                }
                catch (Exception ex)
                {
                    response.Code = ErrorCodes.UnknownError;
                    response.Message = ex.Message + " " + ex.InnerException?.Message ?? "";
                }
                finally
                {
                    #region * Log Request&Response here
                    var requestResponseMessage = new StringBuilder();
                    requestResponseMessage.Append($"------ {MethodBase?.Name ?? ""} Request & Response ------");

                    try
                    {
                        var request = JsonConvert.SerializeObject(Request ?? null, JsonSetting);
                        requestResponseMessage.Append($"\n>>-- Request  ----\n{request ?? ""}");
                    }
                    catch (Exception)
                    { }

                    requestResponseMessage.AppendLine($"\n<<-- Response ----\n --Code={response.Code} \n --Message={response.Message}");

                    if (response.Code == ErrorCodes.UnknownError || response.Code == ErrorCodes.DevelopError)
                    {
                        Logger.Fatal(requestResponseMessage);
                    }
                    else
                    {
                        Logger.Info(requestResponseMessage);
                    }

                    #endregion
                }

                return response;
            }

        }
        public class DataProcessor<TRequest, TResponse> : BaseProcessor
        where TRequest : BaseRequest
        where TResponse : BaseDataResponse, new()
        {

            public dynamic RequestJson { get; set; }
            public Func<TRequest, TResponse> Fun { get; private set; }


            public DataProcessor<TRequest, TResponse> Inbound(Action inbound)
            {
                InboundAction = inbound;
                return this;
            }
            public DataProcessor<TRequest, TResponse> Process(Func<TRequest, TResponse> fun)
            {
                Fun = fun;
                return this;
            }
            public TResponse Build()
            {
                var response = new TResponse()
                {
                    Code = ErrorCodes.UnknownError,
                    Message = "Unknown Error",
                };

                dynamic para = null;
                try
                {
                    if (MethodBase == null)
                    {
                        throw new EeException(ErrorCodes.DevelopError, "Null Method.");
                    }
                    Logger.Info($">>>>>>>>>>> {MethodBase.Name} ==========");
                    //TODO: Do auth here
                    //throw new EeException(ErrorCodes.Unauthorized, "Unauthorized");


                    if (IsParameterRequired)
                    {
                        if (RequestJson == null)
                        {
                            throw new EeException(ErrorCodes.NullParameter, "Parameter is null.");
                        }

                        try
                        {
                            para = JsonConvert.DeserializeObject<TRequest>(RequestJson.ToString());

                        }
                        catch (Exception)
                        {
                            throw new EeException(ErrorCodes.InvalidParameter, "Invalid parameter.Should be Json object format");
                        }
                    }
                    if (Fun != null)
                    {
                        response = Fun(para);
                    }
                    else
                    {
                        throw new EeException(ErrorCodes.DevelopError, "Null Fun.");
                    }
                }
                catch (EeException ex)
                {
                    response.Code = ex.ErrorCode;
                    response.Message = ex.Message;
                }
                catch (Exception ex)
                {
                    response.Code = ErrorCodes.UnknownError;
                    response.Message = ex.Message + " " + ex.InnerException?.Message ?? "";
                }
                finally
                {
                    #region * Log Request&Response here
                    var requestResponseMessage = new StringBuilder();
                    requestResponseMessage.Append($"------ {MethodBase?.Name ?? ""} Request & Response ------");

                    try
                    {
                        var request = JsonConvert.SerializeObject(para ?? null, JsonSetting);
                        requestResponseMessage.Append($"\n>>-- Request  ----\n{request ?? ""}");
                    }
                    catch (Exception)
                    { }


                    string data = string.Empty;
                    if (response.DataObject != null)
                    {
                        try
                        {
                            data = JsonConvert.SerializeObject(response.DataObject, JsonSetting);
                        }
                        catch (Exception)
                        { }

                    }
                    requestResponseMessage.AppendLine($"\n<<-- Response ----\n --Code={response.Code} \n --Message={response.Message} \n --Data={data}");

                    if (response.Code == ErrorCodes.UnknownError || response.Code == ErrorCodes.DevelopError)
                    {
                        Logger.Fatal(requestResponseMessage);
                    }
                    else
                    {
                        Logger.Info(requestResponseMessage);
                    }

                    #endregion
                }

                return response;
            }

        }
    */

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
