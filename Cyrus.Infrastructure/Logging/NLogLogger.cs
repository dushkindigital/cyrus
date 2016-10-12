﻿using System;
using Cyrus.Core.Logging;

namespace Cyrus.Infrastructure.Logging
{
    public class NLogLogger : ILogger
    {
        private static readonly Lazy<NLogLogger> LazyLogger = new Lazy<NLogLogger>(() => new NLogLogger());
        private static readonly Lazy<NLog.Logger> LazyNLogger = new Lazy<NLog.Logger>(NLog.LogManager.GetCurrentClassLogger);

        public static ILogger Instance
        {
            get
            {
                return LazyLogger.Value;
            }
        }

        private NLogLogger()
        {
        }

        public void Log(string message)
        {
            LazyNLogger.Value.Info(message);
        }

        public void Log(Exception ex)
        {
            LazyNLogger.Value.Error(ex);
        }
    }
}