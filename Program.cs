using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace monthlyBillsConsoleApp
{
    class Program 
    {
        public static List<Option>? options;
        public static List<Bill>? bills;
        public static int sum;

        static void Main(string[] args)
        {

            bills = new List<Bill>();

            options = new List<Option>
            {
                new Option("add a new bill", () => createNewBill()),
                new Option("show list of bills", () => printListOfBills()),
                new Option("calculate the total amount of this months bills", () => calculateTotalAmountForMonth()),
                new Option("Exit", () => Environment.Exit(0))
            };

            int index = 0;
            
            tutorial();
            WriteMenu(options, options[index]);

            ConsoleKeyInfo keyInfo;
            do 
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        WriteMenu(options, options[index]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options, options[index]);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyInfo.Key != ConsoleKey.X);

            Console.ReadKey();

        }

        static void createNewBill()
        {
            Console.Clear();
            Console.WriteLine("enter name of the bill");
            var billName = Console.ReadLine();
            Console.WriteLine("enter value of the bill");
            var billValue = Console.ReadLine();
            bills.Add(new Bill(billName, Int32.Parse(billValue)));
            Console.WriteLine($"you entered {billName} and the bill is ${billValue}");
            Console.WriteLine("\nhit enter to continue");
            Console.Read();
            WriteMenu(options, options.First());
        }

        static void printListOfBills()
        {
            Console.Clear();
            Console.WriteLine(" here is a list of your bills \n");
            foreach( var bill in bills)
            {
                Console.WriteLine($" name:{bill.Name} ${bill.Value}");
            }
            Console.WriteLine("\n hit enter to continue");
            Console.Read();
            WriteMenu(options, options.First());

        }

        static void calculateTotalAmountForMonth()
        {
            Console.Clear();
            Console.WriteLine("here is the total for the month\n");
            foreach (var bill in bills)
            {
                 sum = sum + bill.Value ;
            }
            Console.WriteLine($"the total amount in bills this month is ${sum}");
            Console.WriteLine("\nhit enter to continue");
            Console.Read();
            sum = 0;
            WriteMenu(options, options.First());
        }

        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("* ");
                }
                else 
                {
                    Console.WriteLine(" ");
                }

                Console.WriteLine(option.Name);
            }
        }

        static void tutorial()
        {
            Console.Clear();
            Console.WriteLine("to use this app, move the up and down keys to move through the menu and hit enter to select");
            Console.WriteLine("\nhit enter to continue");
            Console.Read();
        }
    }

        

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected; 
        }
    }

    public class Bill 
    {
        public string Name { get; }
        public int Value { get ;}

        public Bill(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}

