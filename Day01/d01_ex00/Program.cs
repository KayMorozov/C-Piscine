using System;
using System.Globalization;
using Models;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace d01_ex00
{
    class Program
    {
        static IEnumerable getValet(string mySum, string path)
        {
            List<string> valets;
            string currentDirName;
            FileInfo fi;
            string[] files;

            valets = new List<string>();
            currentDirName = Directory.GetCurrentDirectory();
            files = Directory.GetFiles(currentDirName, $"{path}/*.txt");
            foreach (string s in files)
            {
                fi = null;
                try
                {
                    fi = new FileInfo(s);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                valets.Add(fi.Name.Substring(0, fi.Name.Length - 4));
            }
            foreach (string valet in valets)
            {
                if (mySum != valet)
                    yield return valet;
            }
        }

        static void Main(string[] args)
        {
            string path;
            Exchanger exchanger;
            ExchangerSum mySum;
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            if (args.Length > 2)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            path = args[1];
            mySum.id = args[0].Substring(args[0].Length - 3);
            args[0] = args[0].Substring(0, args[0].Length - 3);
            if (Double.TryParse(args[0].Replace(',', '.'), out mySum.sum) == false)
            { 
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            if (!(mySum.id == "RUB" || mySum.id == "USD" || mySum.id == "EUR") || mySum.sum == 0)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            Console.WriteLine($"Сумма в исходной валюте: {mySum.sum:N2} {mySum.id}");
            foreach(string valet in getValet(mySum.id, path))
            {
                try
                {
                    exchanger = new Exchanger(path, mySum, valet);
                    Console.WriteLine($"Сумма в {valet}: {exchanger.ToString()}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
