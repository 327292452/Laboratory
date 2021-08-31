using CharModule.UserModules;
using System;
using System.Collections.Generic;

namespace CharModule.SocketModules
{
    public class RoomChatModule
    {
        /// <summary>
        /// Socket ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// Room Number
        /// </summary>
        public string RoomID { get; set; }
        public List<CharModule> CharMessage { get; set; }

    }
    public class RoomUserModule
    {
        /// <summary>
        /// Socket ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// Room Number
        /// </summary>
        public string RoomID { get; set; }
        public List<UserModule> Users { get; set; }

    }
}
