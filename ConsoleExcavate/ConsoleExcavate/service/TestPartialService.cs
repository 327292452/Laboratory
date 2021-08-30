
namespace ConsoleExcavate.service
{
    public partial class TestPartialService
    {
        private int _x;
        private int _y;

        public TestPartialService(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public int GetReuslt()
        {
            return _x * _y;
        }
    }

    public partial class TestPartialService
    {
        public int GetReusltTen()
        {
            return _x * _y * 10;
        }
    }
}
