namespace PipServices.Commons.Log
{
    public static class LogLevelConverter
    {
        public static LogLevel ToLogLevel(object value)
        {
            if (value == null) return LogLevel.Info;

            value = value.ToString().ToUpper();

            if ("0".Equals(value) || "NOTHING".Equals(value) || "NONE".Equals(value))
                return LogLevel.None;
            else if ("1".Equals(value) || "FATAL".Equals(value))
                return LogLevel.Fatal;
            else if ("2".Equals(value) || "ERROR".Equals(value))
                return LogLevel.Error;
            else if ("3".Equals(value) || "WARN".Equals(value) || "WARNING".Equals(value))
                return LogLevel.Warn;
            else if ("4".Equals(value) || "INFO".Equals(value))
                return LogLevel.Info;
            else if ("5".Equals(value) || "DEBUG".Equals(value))
                return LogLevel.Debug;
            else if ("6".Equals(value) || "TRACE".Equals(value))
                return LogLevel.Trace;
            else
                return LogLevel.Info;
        }

        public static string ToString(LogLevel level)
        {
            if (level == LogLevel.Fatal) return "FATAL";
            if (level == LogLevel.Error) return "ERROR";
            if (level == LogLevel.Warn) return "WARN";
            if (level == LogLevel.Info) return "INFO";
            if (level == LogLevel.Debug) return "DEBUG";
            if (level == LogLevel.Trace) return "TRACE";
            return "UNDEF";
        }

        public static int ToInteger(LogLevel level)
        {
            if (level == LogLevel.Fatal) return 1;
            if (level == LogLevel.Error) return 2;
            if (level == LogLevel.Warn) return 3;
            if (level == LogLevel.Info) return 4;
            if (level == LogLevel.Debug) return 5;
            if (level == LogLevel.Trace) return 6;
            return 0;
        }

    }
}
