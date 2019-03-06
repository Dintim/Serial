using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    [Serializable]
    public class PC
    {
        public string Mark { get; set; }
        public string Model { get; set; }
        public string SerialNum { get; set; }
        public int Price { get; set; }
        public PC()
        {

        }
        public PC(string Mark, string SerialNum, string Model)
        {
            this.Mark = Mark;
            this.SerialNum = SerialNum;
            this.Model = Model;
        }

        public void TurnOn()
        {
            Console.WriteLine("PC {0} turn on", SerialNum);
        }
        public void TurnOff()
        {
            Console.WriteLine("PC {0} turn off", SerialNum);
        }
        public void Refresh()
        {
            Console.WriteLine("PC {0} refresh", SerialNum);
        }
    }
}
