using System;
using Models;
using System.IO;

namespace d01_ex00
{
    public class Exchanger
    {
        private string path;
        ExchangerSum sum;
        private string to;

        internal Exchanger(string path, ExchangerSum sum, string to)
        {
            this.path = path;
            this.sum = sum;
            this.to = to;
        }

        private ExchangerRate ParsingPath(ExchangerSum sum, string valet)
        {
            string[] valets;
            int i;
            ExchangerRate rate;

            rate.currencyFrom = sum.id;
            rate.currencyIn = valet;
            rate.ratio = 1;
            if (sum.id == valet)
                return (rate);
            valets = File.ReadAllLines(path + "/" + sum.id + ".txt");
            i = 0;
            try
            {
                while (true)
                {
                    if (valets[i].Substring(0, 3) == rate.currencyIn)
                    {
                        valets[i] = valets[i].Substring(4, valets[i].Length - 4).Replace(',', '.');
                        if (Double.TryParse(valets[i], out rate.ratio) == false)
                            throw new Exception("Parsing error.");
                        break;
                    }
                    i++;
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Unknown currency.");
            }
            return (rate);
        }

        internal ExchangerSum Convert()
        {
            ExchangerRate rate;
            ExchangerSum sumRez;

            rate = ParsingPath(sum, to);
            sumRez.id = rate.currencyIn;
            sumRez.sum = Math.Round(sum.sum * rate.ratio, 2);
            return (sumRez);
        }

        public override string ToString()
        {
            ExchangerSum sum;

            sum = this.Convert();
            return (sum.sum.ToString() + " " + sum.id);
        }
    }
}