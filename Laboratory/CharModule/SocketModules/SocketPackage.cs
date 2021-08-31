
namespace CharModule.SocketModules
{
    public class SocketPackage
    {
        /// <summary>
        /// 1:heartbeat,2:Chat Alone Data,3:Chat Room
        /// </summary>
        public int Type { get; set; }
        public object Data { get; set; }
    }
}
