using System;

namespace EC.Libraries.Util.UniqueID
{
    /// <summary>
    /// 生成唯一ID对象
    /// </summary>
    /// <remarks>2015-06-02 杨合余 添加</remarks>
    public class UniqueID : IUniqueID
    {
        //从2015-06-01  08:30:00 开始计时
        public const long Twepoch = 1433147400000L;
        //机器标识位数
        private const int WorkerIdBits = 5;
        //数据中心标识位数
        private const int DatacenterIdBits = 5;
        //毫秒内自增位
        private const int SequenceBits = 12;
        //机器ID最大值
        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        //数据中心ID最大值
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        //机器ID偏左移12位
        private const int WorkerIdShift = SequenceBits;
        //数据中心ID左移17位
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        //时间毫秒左移22位
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        private string _configPath;

        //配置文件的路径
        private const string ConfigFile = @"App_Data\UID.cfg";

        //配置文件存储的类别名
        private const string IniSectionName = "UID";

        private readonly object _lock = new Object();

        /// <summary>
        /// 配置文件的存放路径
        /// </summary>
        public string ConfigPath
        {
            get { return _configPath; }
        }

        /// <summary>
        /// 默认UID生成器构造函数
        /// </summary>
        /// <param name="sequence">序列</param>
        /// <remarks>2015-06-02 杨军 添加配置参数初始化</remarks>        
        public UniqueID(long sequence = 0L)
        {
            lock (_lock)
            {
                InitConfig(); //配置参数初始化

                _sequence = sequence;

                // sanity check for workerId
                if (WorkerId > MaxWorkerId || WorkerId < 0)
                {
                    throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0",
                        MaxWorkerId));
                }

                if (DatacenterId > MaxDatacenterId || DatacenterId < 0)
                {
                    throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0",
                        MaxDatacenterId));
                }
            }
        }

        /// <summary>
        /// UId
        /// </summary>
        /// <param name="workerId">机器标识</param>
        /// <param name="datacenterId">数据中心标识</param>
        /// <param name="sequence">序列</param>
        public UniqueID(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            // sanity check for workerId
            if (WorkerId > MaxWorkerId || WorkerId < 0)
            {
                throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0",
                    MaxWorkerId));
            }

            if (DatacenterId > MaxDatacenterId || DatacenterId < 0)
            {
                throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0",
                    MaxDatacenterId));
            }	
        }

        public long WorkerId { get; protected set; }
        public long DatacenterId { get; protected set; }

        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                //时间错误不能往后调整
                if (timestamp < _lastTimestamp)
                {
                    //exceptionCounter.incr(1);
                    //log.Error("clock is moving backwards.  Rejecting requests until %d.", _lastTimestamp);
                    throw new InvalidSystemClock(String.Format(
                        "Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                        _lastTimestamp - timestamp));
                }

                if (_lastTimestamp == timestamp)
                {
                    //当前毫秒内，则+1
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        //当前毫秒内计数满了，则等待下一秒
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                //ID偏移组合生成最终的ID，并返回ID 
                var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return id;
            }
        }

        /// <summary>
        /// 等待下一个毫秒的到来 
        /// </summary>
        /// <param name="lastTimestamp">最后一次使用时间戳</param>
        /// <returns></returns>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        protected virtual long TimeGen()
        {
            return IdSystem.CurrentTimeMillis();
        }

        /// <summary>
        /// 读取配置参数
        /// </summary>
        /// <remarks>2015-06-02 杨军 创建</remarks>
        protected void InitConfig()
        {
            _configPath = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, ConfigFile);
            var iniObj = new IniUtil(this.ConfigPath);

            WorkerId = int.Parse(iniObj.Read(IniSectionName, "WorkerId", "1"));
            DatacenterId = int.Parse(iniObj.Read(IniSectionName, "DatacenterId", "-1")) + 1;

            if (DatacenterId > 31)
                DatacenterId = 0;

            if (WorkerId > 31)
                WorkerId = 1;

            iniObj.Write(IniSectionName, "WorkerId", WorkerId.ToString()); //机器标识
            iniObj.Write(IniSectionName, "DatacenterId", DatacenterId.ToString()); //数据中心标识
        }

    }
}
