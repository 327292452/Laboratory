using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyLibrary.Utile.PLog;

namespace ConsoleExcavate.service
{
    public class DZPockService
    {
        private ProcessLog log = new ProcessLog("pockLog\\");
        private readonly int allNum = 52;
        private Queue<DZPockModule> queuePock;
        private Queue<DZPockModule> queueCutL;
        private Queue<DZPockModule> queueCutC;
        private Queue<DZPockModule> queueCutR;
        private List<int> listShuffleL = new List<int>();
        private List<int> listShuffleR = new List<int>();
        private List<string> listPersonName = new List<string>();

        Dictionary<int, PersonModule> dicDerson = new Dictionary<int, PersonModule>();
        private List<DZPockModule> listCom = new List<DZPockModule>();
        Random rd = new Random();

        private int RoomPersonNumAll = 0;
        private int RoomPersonNumNow = 0;
        private int CapitalAll = 0;

        private int Wheel = 0;
        public DZPockService()
        {
            GetPockInit();
        }

        #region 内执行
        public Queue<DZPockModule> OprationShuffle()
        {

            try
            {
                //
                GetCutOInT();
                GetShuffle();
                //
                GetCut();
                //
                GetCutOInT();
                GetShuffle();
                GetCut();
                //
                GetCutOInT();
                GetShuffle();
                GetCut();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return queuePock;
        }
        public void GetShuffle()
        {

            var successively = rd.Next(1, 3);
            try
            {
                if (successively % 2 > 0)
                {
                    var f = listShuffleL.Count > listShuffleR.Count ? listShuffleL.Count : listShuffleR.Count;
                    for (int i = 0; i < f; i++)
                    {
                        if (listShuffleL.Count - 1 > i)
                        {
                            for (int iL = 0; iL < listShuffleL[i]; iL++)
                            {
                                var shuffleL = queueCutL.Dequeue();
                                queuePock.Enqueue(shuffleL);
                            }
                        }
                        if (listShuffleR.Count - 1 > i)
                        {
                            for (int iR = 0; iR < listShuffleR[i]; iR++)
                            {
                                var shuffleR = queueCutR.Dequeue();
                                queuePock.Enqueue(shuffleR);
                            }
                        }
                    }
                }
                else
                {
                    var f = listShuffleL.Count > listShuffleR.Count ? listShuffleL.Count : listShuffleR.Count;
                    for (int i = 0; i < f; i++)
                    {
                        if (listShuffleR.Count - 1 >= i)
                        {
                            for (int iR = 0; iR < listShuffleR[i]; iR++)
                            {
                                var shuffleR = queueCutR.Dequeue();
                                queuePock.Enqueue(shuffleR);
                            }
                        }
                        if (listShuffleL.Count - 1 >= i)
                        {
                            for (int iL = 0; iL < listShuffleL[i]; iL++)
                            {
                                var shuffleL = queueCutL.Dequeue();
                                queuePock.Enqueue(shuffleL);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void GetCutOInT()
        {
            listShuffleL.Clear();
            listShuffleR.Clear();
            var cut = rd.Next(1, 10);
            var cutL = 0;
            var cutR = 0;
            var cutNum = 0;

            cutR = (allNum - cut) / 2;
            cutL = allNum - cutR;

            try
            {
                while (queueCutR.Count < cutR)
                {
                    queueCutR.Enqueue(queuePock.Dequeue());
                }
                while (queueCutL.Count < cutL)
                {
                    queueCutL.Enqueue(queuePock.Dequeue());
                }
                while (queueCutL.Count > cutNum)
                {
                    var random = rd.Next(1, 5);
                    cutNum += random;

                    random = cutL - listShuffleL.Sum() < random ? cutL - listShuffleL.Sum() : random;
                    listShuffleL.Add(random);
                }

                cutNum = 0;
                while (queueCutR.Count > cutNum)
                {
                    var random = rd.Next(1, 5);
                    cutNum += random;

                    random = cutR - listShuffleR.Sum() < random ? cutR - listShuffleR.Sum() : random;
                    listShuffleR.Add(random);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCut()
        {
            var cutR = rd.Next(1, allNum);
            var cutC = rd.Next(1, allNum - cutR);
            var cutL = rd.Next(1, allNum - cutR - cutC);
            var cutNum = 0;

            try
            {
                while (queuePock.Count > 0)
                {
                    cutNum++;
                    if (cutNum <= cutR)
                    {
                        queueCutR.Enqueue(queuePock.Dequeue());
                    }
                    else if (cutNum <= cutR + cutC)
                    {
                        queueCutC.Enqueue(queuePock.Dequeue());
                    }
                    else
                    {
                        queueCutL.Enqueue(queuePock.Dequeue());
                    }
                }
                while (queueCutL.Count > 0)
                {
                    queuePock.Enqueue(queueCutL.Dequeue());
                }
                while (queueCutC.Count > 0)
                {
                    queuePock.Enqueue(queueCutC.Dequeue());
                }
                while (queueCutR.Count > 0)
                {
                    queuePock.Enqueue(queueCutR.Dequeue());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetPockInit()
        {
            queuePock = new Queue<DZPockModule>(allNum);
            queueCutL = new Queue<DZPockModule>(allNum);
            queueCutC = new Queue<DZPockModule>(allNum);
            queueCutR = new Queue<DZPockModule>(allNum);
            for (int i = 13; i > 0; i--)
            {
                queuePock.Enqueue(new DZPockModule { Col = 4, Num = i });
                queuePock.Enqueue(new DZPockModule { Col = 3, Num = i });
                queuePock.Enqueue(new DZPockModule { Col = 2, Num = i });
                queuePock.Enqueue(new DZPockModule { Col = 1, Num = i });
            }
            GetPersonName();
        }

        private void GetComPock()
        {
            try
            {
                var q = queuePock.Dequeue();
                listCom.Add(q);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetPersonName()
        {
            listPersonName.Add("赵一");
            listPersonName.Add("李二");
            listPersonName.Add("张三");
            listPersonName.Add("钱四");
            listPersonName.Add("王五");
            listPersonName.Add("李六");
            listPersonName.Add("朱七");
            listPersonName.Add("陈八");
            listPersonName.Add("郑九");
            listPersonName.Add("贾十");
        }


        private void AddPerson(int code, string name)
        {
            dicDerson.Add(RoomPersonNumNow, new PersonModule { Code = code, Name = name, Capitall = CapitalAll, RoundNum = 1, State = true });
            RoomPersonNumNow += 1;
        }
        private void AddCapitall(int id)
        {
            dicDerson[id].Capitall += CapitalAll;
            dicDerson[id].RoundNum += 1;
        }
        #endregion

        #region 外执行
        public void GetPockAssign()
        {
            var handPNum = 0;

            while (true)
            {
                handPNum++;
                for (int i = 0; i < RoomPersonNumAll; i++)
                {
                    var q = queuePock.Dequeue();

                    if (dicDerson[i].State)
                    {
                        dicDerson[i].HandPock.Add(new DZPockModule { Col = q.Col, Num = q.Num });
                        //
                    }
                }
                if (handPNum == 2) break;
            }
        }

        public void showHandPock()
        {
            dicDerson.ToList().ForEach(f =>
            {
                string handPock = string.Empty;
                f.Value.HandPock.ForEach(hp =>
                {
                    handPock += string.Format("{0}-{1}  ", hp.Num, hp.Col);
                });
                Console.WriteLine(string.Format("{0}:{1}", f.Value.Name, handPock));
            });
        }
        public void showPock()
        {
            List<DZPockModule> list = new List<DZPockModule>();
            string handPock = string.Empty;

            queuePock.ToList().ForEach(f =>
            {
                list.Add(f);
            });

            for (int i = 0; i < list.Count; i++)
            {
                handPock += string.Format("{0}-{1}  ", list[i].Num, list[i].Col);

                if ((i + 1) % 13 == 0)
                {
                    Console.WriteLine(string.Format("{0}    ", handPock));
                    handPock = string.Empty;
                }
            }
        }
        public void showComPock()
        {
            listCom.ForEach(f =>
            {
                string handPock = string.Empty;

                handPock += string.Format("{0}-{1}  ", f.Num, f.Col);

                Console.Write(string.Format("{0}", handPock));
            });
        }

        public void PlayPock()
        {
            GetPockAssign();

            while (Wheel < RoomPersonNumAll)
            {
                Wheel++;
            }

        }
        public void CreatePockRoom(int personNum, int capitalAll)
        {
            RoomPersonNumAll = personNum;
            CapitalAll = capitalAll;

            for (int i = 0; i < personNum; i++)
            {
                AddPerson(i, listPersonName[i]);
            }
        }
        public void BidPock(int id, int type, int tally)
        {
            try
            {
                if (dicDerson[id].Capitall - tally < 0)
                {
                    Console.Write("Whether to append?(Y/N):");
                    var check = Console.ReadLine();
                    if (check.ToUpper().Equals("Y"))
                    {
                        AddCapitall(id);
                        dicDerson[id].Capitall -= tally;
                        dicDerson[id].TallyState.State = type;
                        dicDerson[id].TallyState.Tally = tally;
                    }
                    else
                    {
                        dicDerson[id].TallyState.State = 0;
                        dicDerson[id].TallyState.Tally = dicDerson[id].Capitall;
                        dicDerson[id].Capitall -= 0;
                    }
                }
                else
                {
                    dicDerson[id].Capitall -= tally;
                    dicDerson[id].TallyState.State = type;
                    dicDerson[id].TallyState.Tally = tally;
                }

                if (id == RoomPersonNumAll - 1)
                {
                    GetComPock();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetCapitall()
        {
            string handPock = string.Empty;
            dicDerson.ToList().ForEach(f =>
            {
                handPock += string.Format("{0}[{2}]:{1}  ", f.Value.Name, f.Value.Capitall, f.Value.RoundNum);

            });
            Console.WriteLine("");
            Console.WriteLine(string.Format("{0}", handPock));
        }
        public void RecyclePock()
        {
            dicDerson.Values.ToList().ForEach(f =>
            {
                f.HandPock.ForEach(hp =>
                {
                    queuePock.Enqueue(hp);
                });
                f.HandPock.Clear();
            });
            listCom.ForEach(f =>
            {
                queuePock.Enqueue(f);
            });
            listCom.Clear();
        }
        #endregion
    }

    public class DZPockModule
    {
        public int Col { get; set; }
        public int Num { get; set; }
    }
    public class ShuffleModule
    {
        public int Cut { get; set; }
    }
    public class PersonModule
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public List<DZPockModule> HandPock { get; set; } = new List<DZPockModule>();
        public int Capitall { get; set; }
        public int RoundNum { get; set; }
        public bool State { get; set; }
        public TallyStateModule TallyState { get; set; } = new TallyStateModule();
    }
    public class TallyStateModule
    {
        public int State { get; set; }
        public int Tally { get; set; }
    }
}
