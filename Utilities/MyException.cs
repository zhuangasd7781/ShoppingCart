//namespace dekKBS_MVC.Utility
//{
    public class MyException : System.Exception
    {
        public string Remark;
        public MyException(string message, string remark) : base(message)
        {
            Remark = remark;
        }
    }
//}
