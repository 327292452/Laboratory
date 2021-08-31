

namespace CharModule.ResultModules
{
    public class WSResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public int Total { get; set; }
    }

    public class WSResult<T> : WSResult
    {
        public T Result { get; set; }
    }
}
