using System;
using System.Data;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.IO;

namespace NStore.Core
{
    //OFF > FATAL > ERROR > WARN > INFO > DEBUG  > ALL
    //公共记录日志类
    public static class Log4NetHelper
    {
        #region 变量定义

        /*方便日志的输出格式的配置 采用配置文件的方式来实现*/

        //定义信息的二次处理
        public static event Action<string> OutputMessage;

        private static ILog log = null;

        private static bool LoadLoggerXml = false;

        static Log4NetHelper()
        {
            if (log == null)
            {
                GetLogger("AskMeLog");
            }
        }

        #endregion

        //初始化Logger实例
        public static bool GetLogger(string loggername = "")
        {
            try
            {
                //强制加载配置文件 Log4Net的配置文件
                if (!LoadLoggerXml)
                {
                    log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Log4Net.config"));
                    LoadLoggerXml = true;
                }

                if (String.IsNullOrEmpty(loggername))
                {
                    log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }
                else
                {
                    //根据配置文件中logger节点的名称来获取日志实例对象
                    log = LogManager.GetLogger(loggername);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        #region 定义信息回调处理方式
        private static void HandMessage(object Msg)
        {
            OutputMessage?.Invoke(Msg.ToString());
        }
        private static void HandMessage(object Msg, Exception ex)
        {
            OutputMessage?.Invoke(string.Format("{0}:{1}", Msg.ToString(), ex.ToString()));
        }
        private static void HandMessage(string format, params object[] args)
        {
            OutputMessage?.Invoke(string.Format(format, args));
        }
        #endregion

        #region 封装Log4net
        //记录日志的级别
        //日志的等级[由高到底顺序]
        //OFF > FATAL > ERROR > WARN > INFO > DEBUG  > ALL

        #region Level - Debug[调试信息] 
        public static void Debug(object message)
        {
            HandMessage(message);
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void Debug(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsDebugEnabled)
            {
                log.Debug(message, ex);
            }
        }
        public static void DebugFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsDebugEnabled)
            {
                log.DebugFormat(format, args);
            }
        }
        #endregion

        #region Level - Error[一般错误]
        public static void Error(object message)
        {
            HandMessage(message);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void Error(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
        public static void ErrorFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsErrorEnabled)
            {
                log.ErrorFormat(format, args);
            }
        }
        #endregion

        #region Level - Fatal[致命错误]
        public static void Fatal(object message)
        {
            HandMessage(message);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Fatal(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message, ex);
            }
        }
        public static void FatalFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsFatalEnabled)
            {
                log.FatalFormat(format, args);
            }
        }
        #endregion

        #region Level - Info[一般信息]
        public static void Info(object message)
        {
            HandMessage(message);
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void Info(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }
        public static void InfoFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsInfoEnabled)
            {
                log.InfoFormat(format, args);
            }
        }
        #endregion

        #region Level - Warn[警告信息]
        public static void Warn(object message)
        {
            HandMessage(message);
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        public static void Warn(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
        }
        public static void WarnFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsWarnEnabled)
            {
                log.WarnFormat(format, args);
            }
        }
        #endregion

        #endregion

        #region 定义常规应用程序中未处理的异常信息记录方式
        public static void LoadUnhandledException()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler
             ((sender, e) =>
             {
                 log.Fatal("未处理的异常", e.ExceptionObject as Exception);
             });
        }
        #endregion
    }
}
