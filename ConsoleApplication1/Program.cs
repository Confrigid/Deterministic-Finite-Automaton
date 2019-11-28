using System;
using System.Collections.Generic;

namespace dfa
{
    class MainClass
    {
        //set states to a constant
        private const int q0 = 0;
        private const int q0q1q2 = 1;
        private const int q3 = 2;
        private const int q4 = 3;
        private const int qf = 4;
        private const int nullSet = 5;

        private int state;
        public static void Main(string[] args)
        {
            //Console Output and input setup
            var dfa2 = "c1,c2,c3,d1,n2,S";
            //Give rules of the system.
            Console.WriteLine("This Vending Machine operates in this order: ");
            Console.WriteLine("1. Accepts one or more coins: c1, c2, c3.");
            Console.WriteLine("2. Accepts one drink or snack selection: d1, d2, d3, d4, s1, s2, s3.");
            Console.WriteLine("3. Accepts one selection of amount: n1, n2.");
            Console.WriteLine("4. Accepts pressing start: S.");
            //Give example input
            Console.WriteLine("Example input: {0}", dfa2);
            //Ask for string input by the user.
            Console.Write("Enter your input string with no spaces delimited by commas: ");
            string s = Console.ReadLine();
            //Convert the string s into an array split by the commas.
            string[] dfaArray = s.Split(',');
            //Create a list with the same length as the array.
            List<string> dfaList = new List<string>(dfaArray.Length);
            //Add all elements from the array to a list.
            dfaList.AddRange(dfaArray);
            //Display user's input.
            Console.WriteLine("Input is {0}.", s);

            //Create object of mainclass
            MainClass m = new MainClass();            
            //Reset the state to q0
            m.Reset();
            //Run Process on the user's input.
            m.Process(dfaList);

            //Run Accepted to tell whether the input was accepted or rejected
            if (m.Accepted() == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Input {0} was accepted.", s);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Input {0} was rejected.", s);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }

        }

        public static int Delta(int s, string i)
        {
            //s is the state and i is the input. Default is the nullSet, so if no case can be found for a specific switch, it goes to nullSet.
            switch (s)
            {
                //If the state is q0 it can go to state q0q1q2 only by having the case c1,c2,c3, else it goes to the nullSet.
                case q0: switch (i)
                    {
                        case "c1": return q0q1q2;
                        case "c2": return q0q1q2;
                        case "c3": return q0q1q2;
                        default: return nullSet;
                    }
                //If the state is q0q1q2 it can repeatedly accept c1,c2,c3, or move on to q3 with d1,d2,d3,d4,s1,s2,s3, else go to the nullSet
                case q0q1q2: switch (i)
                    {
                        case "c1": return q0q1q2;
                        case "c2": return q0q1q2;
                        case "c3": return q0q1q2;
                        case "d1": return q3;
                        case "d2": return q3;
                        case "d3": return q3;
                        case "d4": return q3;
                        case "s1": return q3;
                        case "s2": return q3;
                        case "s3": return q3;
                        default: return nullSet;
                    }
                //If the state is q3 it can accept n1,n2 and move on to q4, else go to the nullSet.
                case q3: switch (i)
                    {
                        case "n1": return q4;
                        case "n2": return q4;
                        default: return nullSet;
                    }
                //If the state is q4 it can accept S and move on to qf, else go to the nullSet.
                case q4: switch (i)
                    {
                        case "S": return qf;
                        default: return nullSet;
                    }
                default: return nullSet;
            }
        }

        public void Reset()
        {
            //Resets the DFA to the initial state.
            state = q0;
        }

        public void Process(List<string> input)
        {
            //Iterates through the entire user input list, getting the state returned and checking the next input against the state requirements.
            for (int i = 0; i < input.Count; i++) {
                string c = input[i];
                state = Delta(state, c);
            }
        }

        public Boolean Accepted()
        {
            //If the state returned is qf the string is Accepted, otherwise it is rejected.
            return state == qf;
        }
    }
}
