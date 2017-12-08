using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace AdventOfCodeDay8
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            List<string> instructions = new List<string>();
            int highestEver = 0;
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(_assembly.GetManifestResourceStream("AdventOfCodeDay8.data.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    instructions.Add(line);
                    string regName = line.Split(' ')[0];
                    if (!registers.Keys.Contains(regName))
                        registers.Add(regName, 0);
                }
            }
            foreach (string instruction in instructions)
            {
                List<string> instructionParts = instruction.Split(' ').ToList();
                // instruction is built up as follows: registername1, inc/dec, amount, "if", registername2, relation, value
                string registerToModify = instructionParts[0];
                string direction = instructionParts[1];
                int modificationAmount = Int32.Parse(instructionParts[2]);
                string conditionalRegister = instructionParts[4];
                string relation = instructionParts[5];
                int conditionalAmount = Int32.Parse(instructionParts[6]);
                bool condition = false;
                switch (relation)
                {
                    case "==":
                        condition = (registers[conditionalRegister] == conditionalAmount);
                        break;
                    case "!=":
                        condition = (registers[conditionalRegister] != conditionalAmount);
                        break;
                    case ">":
                        condition = (registers[conditionalRegister] > conditionalAmount);
                        break;
                    case "<":
                        condition = (registers[conditionalRegister] < conditionalAmount);
                        break;
                    case "<=":
                        condition = (registers[conditionalRegister] <= conditionalAmount);
                        break;
                    case ">=":
                        condition = (registers[conditionalRegister] >= conditionalAmount);
                        break;
                    default:
                        break;
                }
                if (condition)
                {
                    registers[registerToModify] = (direction == "inc") ? registers[registerToModify] + modificationAmount : registers[registerToModify] - modificationAmount;
                }
                if (registers.Values.Max() > highestEver)
                    highestEver = registers.Values.Max();
            }
            int highestFinally = registers.Values.Max();
            Console.WriteLine($"The highest value after completing the instructions is {highestFinally}.");
            Console.WriteLine($"The highest value during the process was {highestEver}.");
            Console.ReadKey();
        }
    }
}
