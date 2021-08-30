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

        Dictionary<int, PersonModule> dicDerson = new Dictionary<int, PersonModule>();
        Random rd = new Random();

        private int RoomPersonNumAll = 0;
        private int RoomPersonNumNow = 0;
        private int CapitalAll = 0;

        private int Wheel = 0;
        public DZPockService()
        {
        }

        #region 内执行
        public Queue<DZPockModule> OprationShuffle()
        {

            try
            {
                GetPockInit();
                //
                GetCutOInT();
                GetShuffle();
                //
                GetCutOInT();
                GetShuffle();
                //
                GetCutOInT();
                GetShuffle();
                //
                GetCut();
                GetCut();
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

                    if (dicDerson[i + 1].State)
                    {
                        dicDerson[i + 1].HandPock.Add(new DZPockModule { Col = q.Col, Num = q.Num });
                        //
                    }
                }
                if (handPNum == 2) break;
            }
        }
        public void PlayPock()
        {
            GetPockAssign();

            while (Wheel < RoomPersonNumAll)
            {
                Wheel++;
            }

        }
        public void PlayPockBid(int id, string tallyState, int tally)
        {
            try
            {
                dicDerson[id].TallyState.State = tallyState;
                dicDerson[id].TallyState.Tally = tally;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreatePockRoom(int personNum, int capitalAll)
        {
            RoomPersonNumAll = personNum;
            CapitalAll = capitalAll;
        }
        public void AddPerson(int code, string name)
        {
            RoomPersonNumNow += 1;
            dicDerson.Add(RoomPersonNumNow, new PersonModule { Code = code, Name = name, Capitall = CapitalAll, State = true });
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
        public List<DZPockModule> HandPock { get; set; }
        public int Capitall { get; set; }
        public bool State { get; set; }
        public TallyStateModule TallyState { get; set; }
    }
    public class TallyStateModule
    {
        public string State { get; set; }
        public int Tally { get; set; }
    }
}
