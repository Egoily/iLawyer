using ee.Framework.Schema;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace ee.Framework.Processor
{
    public abstract class ProcessorInternal<TRequest, TResponse> : BaseProcessor<TRequest, TResponse>
                    where TRequest : BaseRequest
                    where TResponse : BaseResponse, new()
    {

        public override TResponse Build()
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
                Info($">>>>>>>>>>> {MethodBase.Name} ==========");


                if (DynamicRequest == null && IsParameterRequired)
                {
                    throw new EeException(ErrorCodes.NullParameter, "Parameter is null.");
                }

                try
                {
                    Request = (TRequest)(DynamicRequest);

                }
                catch (Exception)
                {
                    throw new EeException(ErrorCodes.InvalidParameter, "Invalid parameter.Should be Json object format");
                }
                Request?.Validate();

                if (InboundActions != null && InboundActions.Any())
                {
                    foreach (var action in InboundActions)
                    {
                        action?.Invoke();
                    }
                }

                if (Fun == null && ObjReturnedFun == null)
                {
                    throw new DevelopmentException("Null process function.");
                }
                else
                {
                    if (Fun != null)
                    {
                        response = Fun(Request);
                    }
                    else
                    {
                        if (ResponseConverter == null)
                        {
                            throw new DevelopmentException("Need ResponseConverter.");
                        }
                        var result = ObjReturnedFun(Request);

                        response = ResponseConverter(result);

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
                if (OutboundActions != null && OutboundActions.Any())
                {
                    foreach (var action in OutboundActions)
                    {
                        action?.Invoke();
                    }
                }
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




                if (response is BaseDataResponse)
                {
                    string data = string.Empty;
                    if ((response as BaseDataResponse).DataObject != null)
                    {
                        try
                        {
                            data = JsonConvert.SerializeObject((response as BaseDataResponse).DataObject, JsonSetting);
                        }
                        catch (Exception)
                        { }

                    }
                    requestResponseMessage.AppendLine($"\n<<-- Response ----\n --Code={response.Code} \n --Message={response.Message} \n --Data={data}");
                }
                else
                {
                    requestResponseMessage.AppendLine($"\n<<-- Response ----\n --Code={response.Code} \n --Message={response.Message}");


                }


                if (response.Code == ErrorCodes.UnknownError || response.Code == ErrorCodes.DevelopError)
                {
                    Fatal(requestResponseMessage);
                }
                else
                {
                    Info(requestResponseMessage);
                }

                #endregion
            }

            return response;
        }

    }
}
