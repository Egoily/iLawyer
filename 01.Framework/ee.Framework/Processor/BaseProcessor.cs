using ee.Framework.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ee.Framework.Processor
{
    public delegate TR ResponseConverter<TR>(object source);
    public abstract class BaseProcessor<TRequest, TResponse> : Logger
    {

        protected static readonly JsonSerializerSettings JsonSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>() { new JsonMarkBigObjectConverter() }
        };
        protected MethodBase MethodBase;
        protected bool IsParameterRequired;

        protected List<Action> InboundActions;
        protected List<Action> OutboundActions;

        protected dynamic DynamicRequest;
        protected TRequest Request;
        protected object Response;
        protected Func<TRequest, TResponse> Fun;
        protected Func<TRequest, object> ObjReturnedFun;

        protected ResponseConverter<TResponse> ResponseConverter;

        public virtual BaseProcessor<TRequest, TResponse> Inbound(Action inbound)
        {
            if (InboundActions == null)
            {
                InboundActions = new List<Action>();
            }

            InboundActions.Add(inbound);
            return this;
        }
        public virtual BaseProcessor<TRequest, TResponse> Outbound(Action outbound)
        {
            if (OutboundActions == null)
            {
                OutboundActions = new List<Action>();
            }

            OutboundActions.Add(outbound);
            return this;
        }

        public BaseProcessor<TRequest, TResponse> Input(dynamic dynamicRequest, bool parameterRequired = true)
        {
            DynamicRequest = dynamicRequest;
            IsParameterRequired = parameterRequired;
            return this;
        }
        public BaseProcessor<TRequest, TResponse> Process(Func<TRequest, TResponse> fun)
        {
            Fun = fun;
            return this;
        }


        public BaseProcessor<TRequest, TResponse> Process(Func<TRequest, object> fun)
        {
            if (Fun != null)
            {
                throw new DevelopmentException("The processor can not process two functions");
            }
            ObjReturnedFun = fun;
            return this;
        }
        public BaseProcessor<TRequest, TResponse> UsingResponseConverter(ResponseConverter<TResponse> converter)
        {
            ResponseConverter = converter;
            return this;
        }


        public abstract TResponse Build();
    }


}
