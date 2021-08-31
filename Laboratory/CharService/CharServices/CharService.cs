using CharModule.ResultModules;
using CharModule.SocketModules;
using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CharService.CharServices
{
    public class CharWebService
    {
        private Regex rg = new Regex(@"\w{8}(-\w{4}){3}-\w{12}");
        private static ConcurrentDictionary<Guid, IWebSocketConnection> dicSocket = new ConcurrentDictionary<Guid, IWebSocketConnection>();
        private static ConcurrentDictionary<string, Guid> dicUserIP = new ConcurrentDictionary<string, Guid>();
        private static ConcurrentDictionary<string, IWebSocketConnection> dicChatRoom = new ConcurrentDictionary<string, IWebSocketConnection>();
        public void WebSocketInitialize()
        {
            WSResult r = new WSResult();
            try
            {
                List<Guid> list = GetUserList();
                list.ForEach(f =>
                {
                    WebSocketServer server = new WebSocketServer("ws://127.0.0.1:14001/alone/" + f);
                    server.Start(socket =>
                    {
                        socket.OnOpen = () =>
                        {
                            Console.WriteLine("Open: " + socket.ConnectionInfo.Path.Substring(socket.ConnectionInfo.Path.LastIndexOf("/") + 1));
                            try
                            {
                                var strParame = socket.ConnectionInfo.Path;
                                var listParame = strParame.Split('&');
                                if (rg.IsMatch(listParame[0]))
                                {
                                    var UserGUID = Guid.Parse(rg.Match(listParame[0]).Value);
                                    dicSocket.TryAdd(UserGUID, socket);
                                }
                                else
                                {
                                    throw new Exception("未获取人员信息！");
                                }
                            }
                            catch (Exception e)
                            {
                                r.Code = 1;
                                r.Msg = e.Message;
                                Console.WriteLine(r.Msg);
                                socket.Send(JsonConvert.SerializeObject(r));
                            }
                        };
                        socket.OnClose = () =>
                        {
                            Console.WriteLine("Close: " + socket.ConnectionInfo.Path.Substring(socket.ConnectionInfo.Path.LastIndexOf("/") + 1));
                            try
                            {
                                var ip = socket.ConnectionInfo.ClientIpAddress;
                                if (dicUserIP.ContainsKey(ip))
                                {
                                    var UserGUID = dicUserIP[ip];
                                    if (dicUserIP.ContainsKey(ip))
                                    {
                                        dicSocket.TryRemove(UserGUID, out IWebSocketConnection removedSocket);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        };
                        socket.OnMessage = message =>
                        {
                            var result = new WSResult<CharAloneModule>();
                            string msg;
                            Console.WriteLine("Message: " + socket.ConnectionInfo.Path.Substring(socket.ConnectionInfo.Path.LastIndexOf("/") + 1));

                            SocketPackage package = JsonConvert.DeserializeObject<SocketPackage>(message);

                            switch (package.Type)
                            {
                                case 1:
                                    break;
                                case 2:
                                    CharAloneModule alone = JsonConvert.DeserializeObject<CharAloneModule>(package.Data.ToString());
                                    if (dicSocket.ContainsKey(alone.TagetrGUID))
                                    {
                                        result.Result = alone;
                                        msg = JsonConvert.SerializeObject(result);
                                        //dicSocket[alone.TagetrGUID].Send(msg);
                                        socket.Send(msg);
                                    }
                                    else
                                    {
                                        //存入数据库保存历史信息
                                    }
                                    break;
                                case 3:
                                    RoomChatModule room = (RoomChatModule)package.Data;

                                    msg = JsonConvert.SerializeObject(room.CharMessage);
                                    dicChatRoom[room.RoomID].Send(msg);
                                    break;
                                default:
                                    break;
                            }

                        };

                    });

                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Runing()
        {
            var result = new WSResult<CharAloneModule>();
            result.Result = new CharAloneModule();
            result.Result.CharMessage = new CharModule.SocketModules.CharModule();
            while (true)
            {
                var value = Console.ReadLine();

                if ("CLOSE".Equals(value.ToUpper()))
                {

                    Parallel.ForEach(dicSocket.ToList(), f =>
                    {
                        result.Result.CharMessage.Message = "系统关闭！";
                        result.Result.TagetrGUID = f.Key;
                        result.Result.CharMessage.Type = 1;
                        f.Value.Send(JsonConvert.SerializeObject(result));
                        f.Value.Close();
                    });
                }
                else
                {
                    result.Result.CharMessage.Message = value;

                    Parallel.ForEach(dicSocket.ToList(), f =>
                    {
                        result.Result.TagetrGUID = f.Key;
                        result.Result.CharMessage.Type = 1;
                        f.Value.Send(JsonConvert.SerializeObject(result));
                    });
                }

            }
        }

        private List<Guid> GetUserList()
        {
            List<Guid> list = new List<Guid>();
            list.Add(Guid.Parse("15547ED0-43C3-48B1-88A6-331F72E02B32"));
            list.Add(Guid.Parse("25557ED0-43C3-48B1-88A6-331F72E02B32"));
            list.Add(Guid.Parse("35567ED0-43C3-48B1-88A6-331F72E02B32"));

            return list;
        }

    }
}
