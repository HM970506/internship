using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SI.NE.CMM.Network;
using System.Diagnostics;
using System.IO;
using SI.NE.CMM.DEF;
using SI.NE.CMM.Util;

namespace QuartzTest.Scheduler
{
    public enum NETimeType
    {
        UseLocalTime,
        UseLocalTestTime,
        UseNetworkTestTime,
    }

    [XmlRoot("TimeConfig")]
    public class NETimeConfig
    {
        public NETimeType Type { get; set; }
        public DateTime TestStartTime { get; set; }
        public string NETimeServerIP { get; set; }
        public int NETimeServerPort { get; set; }
    }

    /// <summary>
    /// 테스트를 위한 시간 컨트롤 Class
    /// </summary>
    /// <author>jcyoo</author>
    /// <date>2020-09-23</date> 
    public static class NETime
    {
        private static NETimeType timeType { get; set; }
        private static DateTime currentTime { get; set; }
        private static Timer NETimer;
        private static TimeClient client;
        public static DateTime UtcNow
        {
            get { return timeType == NETimeType.UseLocalTime ? DateTime.UtcNow : currentTime; }
        }

        public static DateTime Now
        {
            get { return timeType == NETimeType.UseLocalTime ? DateTime.UtcNow.AddHours(9) : currentTime.AddHours(9); }
        }

        /// <summary>
        /// 로컬 시간 사용. DateTime.Utc
        /// </summary>
        public static void InitTimer(NETimeConfig config)
        {
            timeType = config.Type;

            switch (config.Type)
            {
                case NETimeType.UseLocalTime:
                    {
                        client?.Stop();

                        currentTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

                        Trace.WriteLine("Init NETime : Use Local");
                    }
                    break;

                case NETimeType.UseLocalTestTime:
                    {
                        client?.Stop();

                        currentTime = DateTime.SpecifyKind(config.TestStartTime, DateTimeKind.Utc);

                        NETimer = new Timer(UpdateLocalTestTime);
                        NETimer.Change(0, 100);

                        Trace.WriteLine(string.Format("Init NETime : Use Local Test Time - {0}", config.TestStartTime.ToString(ConstDef.DATETIME_FORMAT1)));
                    }
                    break;

                case NETimeType.UseNetworkTestTime:
                    {
                        currentTime = DateTime.UtcNow;

                        // 클라이언트 객체 생성
                        NETimeClientRequestHandler clientRequestHandler = new NETimeClientRequestHandler();
                        clientRequestHandler.OnUpdateReceivedTime += ClientTimeService;

                        client = new TimeClient(clientRequestHandler, new NETImeFileTransferProgress());

                        // 연결/종료/파일전송과 관련된 이벤트 연결
                        client.ConnectionResultEventHandler += Client_ConnectionResultEventHandler;

                        // 서버 연결
                        client.Start(config.NETimeServerIP, config.NETimeServerPort);

                        Trace.WriteLine(string.Format("Init NETime : Use Network Test Time - {0}:{1}", config.NETimeServerIP, config.NETimeServerPort));
                    }
                    break;
            }
        }

        /// <summary>
        /// 내부 타이머의 시간을임의로 변경
        /// </summary>
        /// <param name="time"></param>
        public static void SetInternalTestTime(DateTime time)
        {
            currentTime = time;
            Trace.WriteLine(string.Format("Internal Test Time is changed : {0}", time.ToString(ConstDef.DATETIME_FORMAT1)));
        }

        /// <summary>
        /// Client Time Sync Service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ClientTimeService(object sender, ReceivedPacketData receivedData)
        {
            TimeSyncData timeInfo = receivedData.netData.msgData as TimeSyncData;
            DateTime time = DateTime.Parse(Encoding.Default.GetString(timeInfo.payloadData, 0, timeInfo.payloadSize));
            currentTime = DateTime.SpecifyKind(time, DateTimeKind.Utc);
        }

        /// <summary>
        /// 서버 접속여부에 대한 이벤트 핸들러
        /// </summary>
        /// <param name="obj"></param>
        private static void Client_ConnectionResultEventHandler(object sender, bool isConnected)
        {
            if (isConnected)
            {
                Trace.WriteLine("Time Client Connected!!");
            }
            else
            {
                Trace.WriteLine("Time Client Disconnected");
            }
        }

        /// <summary>
        /// 초기 설정 시간을 증가
        /// </summary>
        /// <param name="_state"></param>
        private static void UpdateLocalTestTime(object _state)
        {
            currentTime = currentTime.AddMilliseconds(100);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public static void Close()
        {
            client?.Stop();
            NETimer?.Dispose();
        }
    }


    public class TimeClient : NetClient
    {
        public TimeClient(IRequestHandler requestHandler, IFileTransferProgress fileTransferProgress) : base(requestHandler, fileTransferProgress)
        {

        }
    }


    /// <summary>
    /// 프로토타이핑 클라이언트 reqeust handler class
    /// </summary>
    /// <author>jcyoo</author>
    /// <date>2019-07-03</date> 
    public class NETimeClientRequestHandler : IRequestHandler
    {
        public event EventHandler<ReceivedPacketData> OnUpdateReceivedTime = null;


        /// <summary>
        /// request handler
        /// </summary>
        /// <author>jcyoo</author>
        /// <date>2019-07-03</date> 
        /// <param name="receivedPacketData"></param>
        /// <remarks>서버로 부터 받은 데이터에 대한 핸들링을 PTClient내에서 할 수도 있고 외부의 다른 Class에서 수행할 수도 있음</remarks>
        public void ProcessRequest(ReceivedPacketData receivedPacketData)
        {
            switch ((TimeSyncCommand)receivedPacketData.netData.commandType)
            {
                // 로그인 응답 처리
                case TimeSyncCommand.TIME_REPLY:
                    {
                        OnUpdateReceivedTime?.Invoke(this, receivedPacketData);
                    }
                    break;

            }
        }
    }

    /// <summary>
    /// 파일 전송관련 상태 정보확인을 위한 class
    /// </summary>
    public class NETImeFileTransferProgress : IFileTransferProgress
    {
        public event EventHandler<FileReceivingCompleteInfo> ReceivingCompltedEventHandler;
        public event EventHandler<FileTransferProgressInfo> ReceivingStatusEventHandler;
        public event EventHandler<FileTransferCompleteInfo> TransferCompletedEvnetHandler;

        public void FileReceivingCompltedEventHandler(object sender, FileReceivingCompleteInfo fileReceivingCompleteInfo)
        {
            ReceivingCompltedEventHandler?.Invoke(sender, fileReceivingCompleteInfo);
        }

        public void FileReceivingStatusEventHandler(object sender, FileTransferProgressInfo fileTransferProgressInfo)
        {
            ReceivingStatusEventHandler?.Invoke(sender, fileTransferProgressInfo);
        }

        public void FileTransferCompletedEvnetHandler(object sender, FileTransferCompleteInfo fileTransferCompleteInfo)
        {
            TransferCompletedEvnetHandler?.Invoke(sender, fileTransferCompleteInfo);
        }
    }


    public enum TimeSyncCommand
    {
        TIME_REQUEST = 0x01,
        TIME_REPLY
    }

    
}
