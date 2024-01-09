namespace Models
{
    public class order
    {
        //pk, acc_fk, commodity_fk, status, count
        public int acc_fk { get; set; } = 0;
        public int commodity_fk { get; set; } = 0;
        public int pk { get; set; } = 0;
        public int count { get; set; } = 0;
        public int status { get; set; } = 0;
    }
}
