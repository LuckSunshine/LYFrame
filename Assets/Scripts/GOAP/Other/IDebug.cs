namespace LYGOAP
{
    public abstract class DebugBase
    {
        public static DebugBase Instance { get; set; }

        public abstract void Log(string msg);

        public abstract void LogWarning(string msg);

        public abstract void LogError(string msg);
    }


    public class Debuger
    {
        public static void Log(string msg)
        {
            DebugBase.Instance.Log(msg);
        }

        public static void LogWarning(string msg)
        {
            DebugBase.Instance.LogWarning(msg);
        }

        public static void LogError(string msg)
        {
            DebugBase.Instance.LogError(msg);
        }
    }
}

