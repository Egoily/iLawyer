
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ee.iLawyer.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 第一个访问网站的用户会触发该方法. 通常会在该方法里定义一些系统变量
        /// 如聊天室的在线总人数统计,历史访问人数统计的初始化等等均可在这里定义.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ee.Framework.Logging.Logger.Configure("iLawyerWebApi");

            ee.Framework.Logging.Logger.Info("------------------------Application Start------------------------");
        }


        /// <summary>
        /// 在应用程序关闭时运行的代码，在最后一个HttpApplication销毁之后执行
        /// 比如IIS重启，文件更新，进程回收导致应用程序转换到另一个应用程序域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
            ee.Framework.Logging.Logger.Info("------------------------Application End------------------------");
        }

        /// <summary>
        /// 每个用户访问网站的第一个页面时触发;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(object sender, EventArgs e)
        {
            //string IP = this.Context.Request.UserHostAddress;
            //Session["IP"] = IP;
        }

        /// <summary>
        /// 使用了session.abandon(),或session超时用户退出后均可触发.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {
            // Session["User"]; 向数据库中记录用户退出时间
        }
        /// <summary>
        /// 在每一个HttpApplication实例初始化的时候执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Init(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 在应用程序被关闭一段时间之后，在.net垃圾回收器准备回收它占用的内存的时候被调用。
        ///在每一个HttpApplication实例被销毁之前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Disposed(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///所有没有处理的错误都会导致这个方法的执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            #region 记录错误日志
            //Exception ex = Server.GetLastError().GetBaseException();
            //StringBuilder str = new StringBuilder();
            //str.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            //str.Append("\r\n.客户信息：");


            //string ip = "";
            //if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            //{
            //    ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            //}
            //else
            //{
            //    ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            //}
            //str.Append("\r\n\tIp:" + ip);
            //str.Append("\r\n\t浏览器:" + Request.Browser.Browser.ToString());
            //str.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
            //str.Append("\r\n\t操作系统:" + Request.Browser.Platform.ToString());
            //str.Append("\r\n.错误信息：");
            //str.Append("\r\n\t页面：" + Request.Url.ToString());
            //str.Append("\r\n\t错误信息：" + ex.Message);
            //str.Append("\r\n\t错误源：" + ex.Source);
            //str.Append("\r\n\t异常方法：" + ex.TargetSite);
            //str.Append("\r\n\t堆栈信息：" + ex.StackTrace);
            //str.Append("\r\n--------------------------------------------------------------------------------------------------");
            ////创建路径 
            //string upLoadPath = Server.MapPath("~/Logs/");
            //if (!System.IO.Directory.Exists(upLoadPath))
            //{
            //    System.IO.Directory.CreateDirectory(upLoadPath);
            //}
            ////创建文件 写入错误 
            //System.IO.File.AppendAllText(upLoadPath + DateTime.Now.ToString("yyyy.MM.dd") + ".log", str.ToString(), System.Text.Encoding.UTF8);
            ////处理完及时清理异常 
            //Server.ClearError();
            ////跳转至出错页面 
            //Response.Redirect("Error.html");
            #endregion
        }


        /// <summary>
        /// //每次请求时第一个出发的事件，这个方法第一个执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //var url = Request.Url.ToString();

        }

        /// <summary>
        ///在执行验证前发生，这是创建验证逻辑的起点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 当安全模块已经验证了当前用户的授权时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 当ASP.NET完成授权事件以使缓存模块从缓存中为请求提供服务时发生，从而跳过处理程序（页面或者是WebService）的执行。
        ///这样做可以改善网站的性能，这个事件还可以用来判断正文是不是从Cache中得到的。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_ResolveRequestCache(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 读取了Session所需的特定信息并且在把这些信息填充到Session之前执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 在合适的处理程序执行请求前调用
        ///这个时候，Session就可以用了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {

        }


        /// <summary>
        ///当处理程序完成对请求的处理后被调用。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 释放请求状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_ReleaseRequestState(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 为了后续的请求，更新响应缓存时被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_UpdateRequestCache(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// EndRequest是在响应Request时最后一个触发的事件
        ///但在对象被释放或者从新建立以前，适合在这个时候清理代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 向客户端发送Http标头之前被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 向客户端发送Http正文之前被调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreSendRequestContent(object sender, EventArgs e)
        {

        }


    }
}
