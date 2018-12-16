using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D16
{
    class Solution2 : BaseSolution
    {
        private List<string> _lines;
        private List<Test> _tests = new List<Test>();
        private const string _pattern1 = @"(\d), (\d), (\d), (\d)";
        private const string _pattern2 = @"(\d+) (\d) (\d) (\d)";

        public override void FindSolution()
        {
            _lines = _stream.ReadStringDocument();
            var nextIndex = -1;
            for (int i = 0; i < _lines.Count; i += 4)
            {
                var valid = ReadTest(i);
                if (!valid)
                {
                    nextIndex = i;
                    break;
                }
            }            

            var opcodeDict = new Dictionary<int, List<Operation>>(16);
            foreach (var test in _tests)
            {
                var validOperations = new HashSet<Operation>();
                var opcode = test.Instruction.Opcode;

                foreach (var operation in _operations)
                {
                    var tempRegister = new Register
                    {
                        Values = new int[] { test.Before[0], test.Before[1], test.Before[2], test.Before[3] }
                    };
                    operation.Apply(test.Instruction, tempRegister);
                    if (tempRegister.Equals(test.After))
                        validOperations.Add(operation);
                }

                if (!opcodeDict.ContainsKey(opcode))
                {
                    opcodeDict[opcode] = validOperations.ToList();
                }
                else
                {
                    var current = opcodeDict[opcode];
                    var temp = current.Intersect(validOperations).ToList();
                    if (temp.Count < current.Count)
                    {
                    }
                    opcodeDict[opcode] = temp;
                }
            }

            while (opcodeDict.Count(t => t.Value.Count > 1) > 0)
            {
                var singleOps = opcodeDict.Where(t => t.Value.Count == 1).Select(t => t.Value[0]).ToArray();
                foreach (var item in opcodeDict.Where(t => t.Value.Count > 1).ToArray())
                    item.Value.RemoveAll(t => singleOps.Contains(t));
            }

            var register = new Register { Values = new int[4] };
            nextIndex += 2;
            for (int i = nextIndex; i < _lines.Count; i++)
            {
                var line = _lines[i];
                var instruction = ReadInstruction(i);
                var operation = opcodeDict[instruction.Opcode][0];
                operation.Apply(instruction, register);
            }

            Console.WriteLine(register[0]);
        }

        private bool ReadTest(int i)
        {
            var before = ReadRegister(i);
            if (before == null)
                return false;
            var apply = ReadInstruction(i + 1);
            if (apply == null)
                return false;
            var after = ReadRegister(i + 2);
            if (after == null)
                return false;

            var test = new Test
            {
                Before = before,
                Instruction = apply,
                After = after
            };
            _tests.Add(test);
            return true;
        }

        private Register ReadRegister(int i)
        {
            var match = Regex.Match(_lines[i], _pattern1);
            if (!match.Success)
                return null;
            var groups = match.Groups;
            return new Register
            {
                Values = new int[] { int.Parse(groups[1].Value), int.Parse(groups[2].Value),
                    int.Parse(groups[3].Value) , int.Parse(groups[4].Value) }
            };
        }

        private Instruction ReadInstruction(int i)
        {
            var match = Regex.Match(_lines[i], _pattern2);
            if (!match.Success)
                return null;
            var groups = match.Groups;
            return new Instruction
            {
                Opcode = int.Parse(groups[1].Value),
                A = int.Parse(groups[2].Value),
                B = int.Parse(groups[3].Value),
                C = int.Parse(groups[4].Value)
            };
        }

        private Operation[] _operations = new Operation[]
        {
            new Addr(),
            new Addi(),
            new Mulr(),
            new Muli(),
            new Banr(),
            new Bani(),
            new Borr(),
            new Bori(),
            new Setr(),
            new Seti(),
            new Gtir(),
            new Gtri(),
            new Gtrr(),
            new Etir(),
            new Etri(),
            new Etrr(),
        };


        class Test
        {
            public Register Before { get; set; }
            public Register After { get; set; }
            public Instruction Instruction { get; set; }
        }

        class Register
        {
            public int[] Values { get; set; }

            public override bool Equals(object obj)
            {
                var other = obj as Register;
                if (other == null)
                    return false;
                for (int i = 0; i < Values.Length; i++)
                    if (Values[i] != other.Values[i])
                        return false;
                return true;
            }

            public int this[int index]
            {
                get { return Values[index]; }
                set { Values[index] = value; }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        class Instruction
        {
            public int Opcode { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
        }

        abstract class Operation
        {
            public abstract void Apply(Instruction inst, Register register);
        }

        class Addi : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] + inst.B;
            }
        }

        class Addr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] + register[inst.B];
            }
        }

        class Muli : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] * inst.B;
            }
        }

        class Mulr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] * register[inst.B];
            }
        }

        class Bani : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] & inst.B;
            }
        }

        class Banr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] & register[inst.B];
            }
        }

        class Bori : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] | inst.B;
            }
        }

        class Borr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A] | register[inst.B];
            }
        }

        class Seti : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = inst.A;
            }
        }

        class Setr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = register[inst.A];
            }
        }

        class Gtir : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = (inst.A > register[inst.B]) ? 1 : 0;
            }
        }

        class Gtri : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = (register[inst.A] > inst.B) ? 1 : 0;
            }
        }

        class Gtrr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = (register[inst.A] > register[inst.B]) ? 1 : 0;
            }
        }

        class Etir : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = (inst.A == register[inst.B]) ? 1 : 0;
            }
        }

        class Etri : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = (register[inst.A] == inst.B) ? 1 : 0;
            }
        }

        class Etrr : Operation
        {
            public override void Apply(Instruction inst, Register register)
            {
                register[inst.C] = (register[inst.A] == register[inst.B]) ? 1 : 0;
            }
        }
    }
}
