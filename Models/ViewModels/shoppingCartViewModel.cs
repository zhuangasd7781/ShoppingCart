namespace Models.ViewModels
{
    public class shoppingCartViewModel
    {
        //pk, name, price, pic, count
        public int pk { get; set; } = 0;
        public string name { get; set; } = "";
        public int price { get; set; } = 0;
        public string pic { get; set; } = "";
        public int count { get; set; } = 0;
    }
}
