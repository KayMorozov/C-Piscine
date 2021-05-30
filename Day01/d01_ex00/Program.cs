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
        static List<string> ReadValet(string path)
        {
            string currentDirName;
            FileInfo fi;
            string[] files;
            List<string> valets;

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
            return (valets);
        }

        static IEnumerable getValet(string mySum, List<string>valets)
        {
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
            List<string> valets;

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
            valets = ReadValet(path);
            if (!valets.Contains(mySum.id))
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
                return;
            }
            Console.WriteLine($"Сумма в исходной валюте: {mySum.sum:N2} {mySum.id}");
            foreach(string valet in getValet(mySum.id, valets))
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
