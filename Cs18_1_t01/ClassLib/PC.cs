using System;

namespace ClassLib
{
    [Serializable]
    public class PC
    {
        public string Brand { get; set; }
        public string SN { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int State { get; set; }
        public PC() : this(null, null, DateTime.Now, 0) { }
        public PC(string Brand, string SN, DateTime PurchaseDate, int State)
        {
            this.Brand = Brand;
            this.SN = SN;
            this.PurchaseDate = PurchaseDate;
            this.State = State;
        }
        public override string ToString()
        {
            return $"{Brand,-10} {SN,-10} {PurchaseDate.ToShortDateString(),-15} {State,-3}";
        }
        public void PCOn()
        {
            State = 1;
            Console.WriteLine($"PC {SN} is ON");
        }
        public void PCOff()
        {
            State = 0;
            Console.WriteLine($"PC {SN} is OFF");
        }
        public void PCRestart()
        {
            State = 0;
            State = 1;
            Console.WriteLine($"PC {SN} is Restarted");
        }
    }
}
