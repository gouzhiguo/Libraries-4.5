using System;

namespace EC.Libraries.Util.UniqueID
{
    /// <summary>
    /// 标识时间错误异常
    /// </summary>
    internal class InvalidSystemClock : Exception
    {
        public InvalidSystemClock(string message) : base(message) { }
    }
}
