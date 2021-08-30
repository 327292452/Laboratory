using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.SUPTools.EMail
{
    public interface IEMailBase
    {
        void CreateHost(ConfigHost host);
        void CreateMail(ConfigMail mail);
        void CreateMultiMail(ConfigMail mail);
        void SendMail();
    }
}
