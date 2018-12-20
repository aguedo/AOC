using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D19
{
    class Solution1 : BaseSolution
    {
        private List<Instruction> _instructions = new List<Instruction>();
        private Register _register = new Register { Values = new int[6] };
        private int _registryPointer;

        public override void FindSolution()
        {
            ReadInstructions();

            while (_register[_registryPointer] < _instructions.Count)
            {
                //Console.WriteLine(_register);
                var nextInstruction = _instructions[_register[_registryPointer]];
                nextInstruction.Apply(_register);
                _register[_registryPointer] += 1;
            }            

            var result = _register[0];
            Console.WriteLine(result);
        }

        private void ReadInstructions()
        {
            var lines = _stream.ReadStringDocument();
            var firstLine = lines[0];
            var ip = int.Parse(firstLine[firstLine.Length - 1].ToString());
            _register.InstructionPointer = ip;
            _registryPointer = ip;

            var pattern = @"(.+)\s(\d+)\s(\d+)\s(\d+)";
            for (int i = 1; i < lines.Count; i++)
            {
                var groups = Regex.Match(lines[i], pattern).Groups;
                var instruction = new Instruction
                {
                    A = int.Parse(groups[2].Value),
                    B = int.Parse(groups[3].Value),
                    C = int.Parse(groups[4].Value),
                    Operation = _operations[groups[1].Value],
                    Opcode = groups[1].Value
                };
                _instructions.Add(instruction);
            }
        }

        private Dictionary<string, Operation> _operations = new Dictionary<string, Operation>
        {
            { "addr", new Addr() },
            { "addi", new Addi() },
            { "mulr", new Mulr() },
            { "muli", new Muli() },
            { "banr", new Banr() },
            { "bani", new Bani() },
            { "borr", new Borr() },
            { "bori", new Bori() },
            { "setr", new Setr() },
            { "seti", new Seti() },
            { "gtir", new Gtir() },
            { "gtri", new Gtri() },
            { "gtrr", new Gtrr() },
            { "eqir", new Etir() },
            { "eqri", new Etri() },
            { "eqrr", new Etrr() }
        };

        class Register
        {
            public int[] Values { get; set; }

            public int InstructionPointer { get; set; }

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

            public void UpdateBeforeInstruction()
            {
                //Values[InstructionPointer]
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

            public override string ToString()
            {
                return string.Join(" ", Values);
            }
        }

        class Instruction
        {
            public string Opcode { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public Operation Operation { get; set; }

            public void Apply(Register register)
            {
                Operation.Apply(this, register);
            }
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
