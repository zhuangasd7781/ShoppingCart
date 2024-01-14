using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class account
    {
        //pk, id, pwd, name, phone, role, date, status
        public int pk { get; set; } = 0;

        [Required(ErrorMessage = "請輸入帳號")]
        public string id { get; set; } = "";
        [Required(ErrorMessage = "請輸入密碼")]
        public string pwd { get; set; } = "";
        public string name { get; set; } = "";
        public string phone { get; set; } = "";
        public int role { get; set; } = 0;
        public string date { get; set; } = "";
        public string status { get; set; } = "";

    }
}
