using AOC.Common.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Season2018.D24
{
    class Solution1 : BaseSolution
    {
        private List<Group> _immuneSystem = new List<Group>();
        private List<Group> _infections = new List<Group>();
        private Group[] _attackOrder;
        private int _immuneGroupCount;
        private int _infectionGroupCount;
        private HashSet<Group> _selectedImmunes = new HashSet<Group>();
        private HashSet<Group> _selectedInfections = new HashSet<Group>();

        public override void FindSolution()
        {
            ReadInput();

            while (_infectionGroupCount > 0 && _immuneGroupCount > 0)
            {
                Fight();
            }

            var result = 0;
            if (_infectionGroupCount > 0)
                result = _infections.Where(t => t.Active).Sum(t => t.Units);
            else
                result = _immuneSystem.Where(t => t.Active).Sum(t => t.Units);
            Console.WriteLine(result);
        }

        private void Fight()
        {
            SelectTarget();
            Attack();
        }

        private void Attack()
        {
            foreach (var group in _attackOrder)
            {
                if (!group.Active || group.Target == null)
                    continue;

                group.Attack();
                if (!group.Target.Active)
                {
                    if (group.Target.IsInfection)
                        _infectionGroupCount--;
                    else
                        _infectionGroupCount--;
                }
            }
        }

        private void SelectTarget()
        {
            var selectingOrder = _infections.Concat(_immuneSystem)
                .OrderByDescending(t => t.EffectivePower)
                .ThenBy(t => t.Inititiative);
            _selectedImmunes.Clear();
            _selectedInfections.Clear();

            foreach (var group in selectingOrder)
            {
                SelectTaget(group);
            }
        }

        private void SelectTaget(Group group)
        {
            HashSet<Group> selectedTargets;
            List<Group> tagets;
            if (group.IsInfection)
            {
                tagets = _immuneSystem;
                selectedTargets = _selectedImmunes;
            }
            else
            {
                tagets = _infections;
                selectedTargets = _selectedInfections;
            }

            group.Target = null;
            var enemies = tagets.Where(t => t.Active && group.DamageRateTo(t) > 0 && !selectedTargets.Contains(t))
                .OrderByDescending(t => group.DamageRateTo(t))
                .ToArray();
            if (enemies.Length == 0)
                return;
            var target = enemies
                .OrderByDescending(t => t.EffectivePower)
                .ThenByDescending(t => t.Inititiative)
                .First();
            selectedTargets.Add(target);
            group.Target = target;
        }

        private void ReadInput()
        {
            var lines = _stream.ReadStringDocument();
            const string pattern = @"(\d+)\D+(\d+) hit points (\(.+\) )?with an attack that does (\d+) (\w+) damage at initiative (\d+)";
            var army = _immuneSystem;
            var isInfection = false;
            for (int i = 1; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.Length == 0)
                {
                    i += 1;
                    army = _infections;
                    isInfection = true;
                }
                else
                {
                    var regex = Regex.Match(line, pattern).Groups;
                    var group = new Group
                    {
                        Units = int.Parse(regex[1].Value),
                        HitPoints = int.Parse(regex[2].Value),
                        Damage = int.Parse(regex[4].Value),
                        DamageType = regex[5].Value,
                        Inititiative = int.Parse(regex[6].Value),
                        IsInfection = isInfection
                    };
                    SetSpecialPower(group, regex[3].Value);
                    army.Add(group);
                }
            }

            _infectionGroupCount = _infections.Count;
            _immuneGroupCount = _immuneSystem.Count;
            _attackOrder = _immuneSystem.Concat(_infections).OrderByDescending(t => t.Inititiative).ToArray();
        }

        private void SetSpecialPower(Group group, string line)
        {
            if (string.IsNullOrEmpty(line))
                return;

            var powers = line.Substring(1, line.Length - 3).Split(';');
            SetPower(group, powers[0]);
            if (powers.Length > 1)
                SetPower(group, powers[1].Trim());
        }

        private void SetPower(Group group, string line)
        {
            if (line.StartsWith("weak"))
            {
                var weaks = line.Substring(8, line.Length - 8).Split(',');
                foreach (var weak in weaks)
                    group.WeakTo.Add(weak);
            }
            else if (line.StartsWith("immune"))
            {
                var immunes = line.Substring(10, line.Length - 10).Split(',');
                foreach (var immune in immunes)
                    group.ImmuneTo.Add(immune);
            }
        }

        class Group
        {
            public int Units { get; set; }

            public int HitPoints { get; set; }

            public int Damage { get; set; }

            public string DamageType { get; set; }

            public int Inititiative { get; set; }

            public int EffectivePower => Units * Damage;

            public bool Active => Units > 0;

            public bool IsInfection { get; set; }

            public HashSet<string> WeakTo { get; set; } = new HashSet<string>();

            public HashSet<string> ImmuneTo { get; set; } = new HashSet<string>();

            public Group Target { get; set; }

            internal void Attack()
            {
                if (Target == null)
                    return;

                var damageRate = DamageRateTo(Target);
                if (damageRate == 0)
                    return;

                var killedUnits = EffectivePower * damageRate / Target.HitPoints;
                Target.Units -= killedUnits;
            }

            internal int DamageTo(Group target)
            {
                var damageRate = DamageRateTo(target);
                return EffectivePower * damageRate / Target.HitPoints;
            }

            internal int DamageRateTo(Group target)
            {
                if (target.WeakTo.Contains(DamageType))
                    return 2;
                else if (target.ImmuneTo.Contains(DamageType))
                    return 0;
                return 1;
            }
        }
    }
}
