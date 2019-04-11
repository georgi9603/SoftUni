using System.Dynamic;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace ToupleTask
{
    public class SpecialTuple<T,T2,T3>
    {

        public SpecialTuple(T item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public T Item1 { get;  set; }
        public T2 Item2 { get;  set; }

        public T3 Item3 { get; set; }   

        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append(this.Item1);
            //sb.Append($" -> {this.Item2}");
            //return sb.ToString().TrimEnd();
            return $"{this.Item1} -> {this.Item2} -> {this.Item3}";
        }
    }
}