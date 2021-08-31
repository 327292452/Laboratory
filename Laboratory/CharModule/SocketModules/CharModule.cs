using System;

namespace CharModule.SocketModules
{
    public class CharModule
    {
        /// <summary>
        /// Message Sender
        /// </summary>
        public Guid UserGUID { get; set; }
        /// <summary>
        /// 消息类别（1：文字；2：图片；3：视频）
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }
    public class CharAloneModule
    {
        /// <summary>
        /// Message Recipient 
        /// </summary>
        public Guid TagetrGUID { get; set; }
        public CharModule CharMessage { get; set; }
    }
}
