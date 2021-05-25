using System;

namespace Program
{
    class Program
    {
        
        static double AnnuityPayment(double sum, double rate, double term)
        {
            double i;
            double rez;
            
            i = rate / 12 / 100;
            rez = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
            return (rez);
        }

        static double Percent(double sum, double rate, double period_day)
        {
            double rez;

            rez = (sum * rate * period_day) / (100 * 365);
            return (rez);
        }

        static int CountMonth(double annuityPayment, double sum, double rate)
        {
            double i;
            int rez;
            
            i = rate / 12 / 100;
            rez = (int)Math.Log(annuityPayment/(annuityPayment - i * sum), 1 + i);
            return (rez);
        }

        static void Header(int i)
        {
            int j;
            if (i < 13)
                i = 13;
            string[] words = new string[] {"Номер месяца", "Дата", "Платеж", "ОД", "Проценты", "Остаток долга"};

            foreach (string word in words)
            {
                Console.Write("| " + word);
                j = i - word.Length;
                while (j > 0)
                {
                    Console.Write(" ");
                    j--;
                }
            }

            Console.WriteLine("|");
        }

        static void Tail(int i, double digit)
        {
            double digitSave;
            
            if (i < 13)
                i = 13;
            digitSave = digit;
            Console.Write("| ");
            while (true)
            {
                i--;
                digit /= 10;
                if (digit < 1)
                    break;
                if (digit == 1)
                {
                    i--;
                    break;
                }
            }
            digit = digitSave;
            if (digit % 1 != 0)
                i -= 2;
            digit *= 10;
            if (digit % 1 != 0)
                i -= 1;
            Console.Write(digitSave);
            if (i != 0)
            {
                while (i > 0)
                {
                    Console.Write(" ");
                    i--;
                }
            }
        }

        static void Date(DateTime date, int lenString)
        {
            int lenSpace;

            if (lenString < 13)
                lenSpace = 3;
            else
                lenSpace = lenString - 10;
            Console.Write("| " + date.ToShortDateString());
            while (lenSpace > 0)
            {
                Console.Write(" ");
                lenSpace--;
            }
        }
        
        static double ReducingTheAmount(double annuityPayment, int selectedMonth, int term, double sum, double rate, double payment, int lenString)
        {
            int numberOfMonth;
            double totalDebt;
            double percent;
            double overpayment;
	    int month;
	    month = term;
            numberOfMonth = 0;
            overpayment = 0;
            Console.WriteLine("\nДосрочное погашение с уменьшением суммы.\n");
            Header(lenString);
            while (month != 0)
            {
                
                var pastMonth = DateTime.Now.AddMonths(numberOfMonth);
                var thisMonth = DateTime.Now.AddMonths(numberOfMonth + 1);
                var diffDay = (thisMonth - pastMonth).Duration().Days;
                percent = Percent(sum , rate, (double)diffDay);
                numberOfMonth++;
                totalDebt = annuityPayment - percent;
                sum -= totalDebt;
                if (month == term - selectedMonth + 1)
                {
                    sum -= payment;
                    annuityPayment = AnnuityPayment(sum, rate, (double) (term - selectedMonth));
                }
                if (sum < 0)
                    sum = 0;
                Tail(lenString, numberOfMonth);
                Date(DateTime.Now.AddMonths(numberOfMonth), lenString);
                Tail(lenString, Math.Round(annuityPayment, 2));
                Tail(lenString, Math.Round(totalDebt, 2));
                Tail(lenString, Math.Round(percent, 2));
                Tail(lenString, Math.Round(sum, 2));
                Console.WriteLine("|");
                month--;
                overpayment += percent;
                if (sum == 0)
                    break;
            }
            return (overpayment);
        }

        static double ReductionOfTheTerm(double annuityPayment, int selectedMonth, int term, double sum, double rate, double payment, int lenString)
        {
            int numberOfMonth;
            double totalDebt;
            double percent;
            double overpayment;
	    int month;
	    month = term;
            numberOfMonth = 0;
            overpayment = 0;
            Console.WriteLine("\nДосрочное погашение с уменьшением срока.\n");
            Header(lenString);
            while (month >= 0)
            {
                var pastMonth = DateTime.Now.AddMonths(numberOfMonth);
                var thisMonth = DateTime.Now.AddMonths(numberOfMonth + 1);
                var diffDay = (thisMonth - pastMonth).Duration().Days;
                percent = Percent(sum , rate, (double)diffDay);
                numberOfMonth++;
                totalDebt = annuityPayment - percent;
                sum -= totalDebt;
                if (month == term - selectedMonth + 1)
                {
                    sum -= payment;
                    month = CountMonth(annuityPayment, sum, rate) + 1;
                }
                if (sum < 0)
                    sum = 0;
                Tail(lenString, numberOfMonth);
                Date(DateTime.Now.AddMonths(numberOfMonth), lenString);
                Tail(lenString, Math.Round(annuityPayment, 2));
                Tail(lenString, Math.Round(totalDebt, 2));
                Tail(lenString, Math.Round(percent, 2));
                Tail(lenString, Math.Round(sum, 2));
                Console.WriteLine("|");
                month--;
                if(month != -1)
                    overpayment += percent;
                if (sum == 0)
                    break;
            }
            return (overpayment);
        }
        
        static void Main(string[] args)
        {
            double sum;// Сумма кредита
            double rate;// Годовая процентная ставка
            int term;// Количество месяцев кредита
            int selectedMonth;// Номер месяца кредита, в котором вносится досрочный платеж
            double payment;// Сумма досрочного платежа
            double annuityPayment;
            double overpaymentRTA;
            double overpaymentROTT;
            int lenString;
            
            if (args.Length != 5)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return ;
            }
            Double.TryParse(args[0], out sum);
            Double.TryParse(args[1], out rate);
            Int32.TryParse(args[2], out term);
            Int32.TryParse(args[3], out selectedMonth);
            Double.TryParse(args[4], out payment);
            if (sum <= 0 || rate <= 0 || term <= 0 || selectedMonth <= 0 || payment <= 0 || term < selectedMonth || sum < payment)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return ;
            }
            lenString = args[0].Length + 2;
            annuityPayment = AnnuityPayment(sum, rate, (double) term);
            overpaymentRTA = ReducingTheAmount(annuityPayment, selectedMonth, term, sum, rate, payment, lenString);
            overpaymentROTT = ReductionOfTheTerm(annuityPayment, selectedMonth, term, sum, rate, payment, lenString);
            overpaymentRTA = Math.Round(overpaymentRTA, 2);
            overpaymentROTT = Math.Round(overpaymentROTT, 2);
            Console.WriteLine($"Переплата при уменьшении платежа: {overpaymentRTA}р.");
            Console.WriteLine($"Переплата при уменьшении срока: {overpaymentROTT}р.");
            if (overpaymentRTA > overpaymentROTT)
                Console.WriteLine($"Уменьшение срока выгоднее уменьшения платежа на {Math.Round(overpaymentRTA - overpaymentROTT, 2)}р.");
            else if (overpaymentRTA < overpaymentROTT)
                Console.WriteLine($"Уменьшение платежа выгоднее уменьшения срока на {Math.Round(overpaymentROTT - overpaymentRTA, 2)}р.");
            else
                Console.WriteLine("Переплата одинакова в обоих вариантах.");
        }
    }
}
 
