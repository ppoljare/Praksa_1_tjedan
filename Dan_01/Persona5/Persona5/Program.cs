using System;
using System.Collections.Generic;

namespace Persona5
{
    class Program
    {
        static void Main(string[] args)
        {
            // TESTIRANJE
            AttackSkill Zio = new AttackSkill("Zio", AttackPowers.Light, Elements.Thunder, Targets.One, 4);
            AttackSkill Zionga = new AttackSkill("Zionga", AttackPowers.Medium, Elements.Thunder, Targets.One, 8);
            AttackSkill Mazio = new AttackSkill("Mazio", AttackPowers.Light, Elements.Thunder, Targets.All, 10);
            StatusSkill Tarukaja = new StatusSkill("Tarukaja", StatChanges.Increase, Stats.Attack, Targets.One, 8);
            AttackSkill Mazionga = new AttackSkill("Mazionga", AttackPowers.Medium, Elements.Thunder, Targets.All, 16);
            StatusSkill Matarukaja = new StatusSkill("Matarukaja", StatChanges.Increase, Stats.Attack, Targets.All, 24);
            AttackSkill Ziodyne = new AttackSkill("Ziodyne", AttackPowers.Heavy, Elements.Thunder, Targets.One, 12);

            Console.WriteLine("{0}: {1}", Mazionga.Name, Mazionga.Description);

            TeamMember Ryuji = new TeamMember("Ryuji", 675, 249, new List<Skill> {Zio, Zionga, Mazio, Tarukaja});
            Ryuji.AddSkills(new Skill[] {Mazionga, Matarukaja, Ziodyne});
            Ryuji.UseSkill(Ziodyne);
            Console.WriteLine(Ryuji.CurrentSP);

            AttackSkill Agi = new AttackSkill("Agi", AttackPowers.Light, Elements.Fire, Targets.One, 4);
            AttackSkill Maragi = new AttackSkill("Maragi", AttackPowers.Light, Elements.Fire, Targets.All, 10);
            StatusSkill Tarunda = new StatusSkill("Tarunda", StatChanges.Decrease, Stats.Attack, Targets.One, 8);

            Console.WriteLine("{0}: {1}", Tarunda.Name, Tarunda.Description);

            TeamMember Ann = new TeamMember("Ann", 522, 310, new Skill[] {Agi, Maragi, Tarunda});

            Team Team1 = new Team("Team 1");
            Team1.AddMember(Ryuji);
            Team1.AddMember(Ann);

            Console.WriteLine();
            Console.WriteLine(Team1.Name + ":");
            foreach (TeamMember teamMember in Team1.TeamMembers)
            {
                Console.Write(teamMember.Name + "\t");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }

    enum Targets
    {
        One,
        All
    }

    enum StatChanges
    {
        Increase,
        Decrease
    }

    enum Stats
    {
        Attack,
        Defense,
        Speed
    }

    enum AttackPowers
    {
        Light,
        Medium,
        Heavy,
        Severe
    }

    enum Elements
    {
        Fire,
        Ice,
        Thunder,
        Wind,
        Psychic,
        Nuclear,
        Bless,
        Curse
    }

    enum Resources
    {
        HP,
        SP
    }

    abstract class Skill
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Targets NoOfTargets { get; protected set; }
        public int Cost { get; protected set; }

        protected abstract void SetDescription();
    }

    class AttackSkill : Skill
    {
        public Elements Element { get; private set; }
        public AttackPowers AttackPower { get; private set; }
        
        public AttackSkill(string name, AttackPowers attackPower, Elements element, Targets noOfTargets, int cost)
        {
            Name = name;
            AttackPower = attackPower;
            Element = element;
            NoOfTargets = noOfTargets;
            Cost = cost;
            SetDescription();
        }
        
        protected override void SetDescription()
        {
            Description = "Deal ";
            SetDescriptionPower();
            SetDescriptionElement();
            Description += "damage to ";
            SetDescriptionTarget();
        }

        private void SetDescriptionPower()
        {
            switch (AttackPower)
            {
                case AttackPowers.Light:
                    Description += "light ";
                    break;
                case AttackPowers.Medium:
                    Description += "medium ";
                    break;
                case AttackPowers.Heavy:
                    Description += "heavy ";
                    break;
                case AttackPowers.Severe:
                    Description += "severe ";
                    break;
                default:
                    break;
            }
        }

        private void SetDescriptionElement()
        {
            switch (Element)
            {
                case Elements.Fire:
                    Description += "fire ";
                    break;
                case Elements.Ice:
                    Description += "ice ";
                    break;
                case Elements.Thunder:
                    Description += "thunder ";
                    break;
                case Elements.Wind:
                    Description += "wind ";
                    break;
                case Elements.Psychic:
                    Description += "psychic ";
                    break;
                case Elements.Nuclear:
                    Description += "nuclear ";
                    break;
                case Elements.Bless:
                    Description += "bless ";
                    break;
                case Elements.Curse:
                    Description += "curse ";
                    break;
                default:
                    break;
            }
        }

        private void SetDescriptionTarget()
        {
            switch (NoOfTargets)
            {
                case Targets.One:
                    Description += "one foe.";
                    break;
                case Targets.All:
                    Description += "all foes.";
                    break;
                default:
                    break;
            }
        }
    }

    class StatusSkill: Skill
    {
        public Stats StatAffected { get; private set; }
        public StatChanges StatChange { get; private set; }

        public StatusSkill(string name, StatChanges statChange, Stats statAffected, Targets noOfTargets, int cost)
        {
            Name = name;
            StatChange = statChange;
            StatAffected = statAffected;
            NoOfTargets = noOfTargets;
            Cost = cost;
            SetDescription();
        }

        protected override void SetDescription()
        {
            Description = "";
            SetDescriptionStatChange();
            SetDescriptionStatAffected();
            Description += "of ";
            SetDescriptionTarget();
            Description += "for 3 turns.";
        }

        private void SetDescriptionStatChange()
        {
            switch (StatChange)
            {
                case StatChanges.Increase:
                    Description += "Increase ";
                    break;
                case StatChanges.Decrease:
                    Description += "Decrease ";
                    break;
                default:
                    break;
            }
        }

        private void SetDescriptionStatAffected()
        {
            switch (StatAffected)
            {
                case Stats.Attack:
                    Description += "attack ";
                    break;
                case Stats.Defense:
                    Description += "defense ";
                    break;
                case Stats.Speed:
                    Description += "speed ";
                    break;
                default:
                    break;
            }
        }

        private void SetDescriptionTarget()
        {
            switch (StatChange)
            {
                case StatChanges.Increase:
                    switch (NoOfTargets)
                    {
                        case Targets.One:
                            Description += "one ally ";
                            break;
                        case Targets.All:
                            Description += "all allies ";
                            break;
                        default:
                            break;
                    }
                    break;
                case StatChanges.Decrease:
                    switch (NoOfTargets)
                    {
                        case Targets.One:
                            Description += "one foe ";
                            break;
                        case Targets.All:
                            Description += "all foes ";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    class TeamMember
    {
        public string Name { get; private set; }
        public List<Skill> Skills { get; private set; }
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        public int MaxSP { get; private set; }
        public int CurrentSP { get; private set; }

        public TeamMember(string name, int maxHP, int maxSP)
        {
            Name = name;
            CurrentHP = MaxHP = maxHP;
            CurrentSP = MaxSP = maxSP;
            Skills = new List<Skill>();
        }

        public TeamMember(string name, int maxHP, int maxSP, Skill[] skills)
        {
            Name = name;
            CurrentHP = MaxHP = maxHP;
            CurrentSP = MaxSP = maxSP;
            Skills = new List<Skill>();
            AddSkills(skills);
        }

        public TeamMember(string name, int maxHP, int maxSP, List<Skill> skills)
        {
            Name = name;
            CurrentHP = MaxHP = maxHP;
            CurrentSP = MaxSP = maxSP;
            Skills = skills;
        }

        public void AddSkill(Skill skill)
        {
            if (!HasSkill(skill))
            {
                Skills.Add(skill);
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} already has {1}!", Name, skill.Name);
            }
        }

        public void AddSkills(Skill[] skills)
        {
            foreach (Skill skill in skills)
            {
                AddSkill(skill);
            }
        }

        public void AddSkills(List<Skill> skills)
        {
            foreach (Skill skill in skills)
            {
                AddSkill(skill);
            }
        }

        public void RemoveSkill(Skill skill)
        {
            if (HasSkill(skill))
            {
                Skills.Remove(skill);
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} doesn't have {1}!", Name, skill.Name);
            }
        }

        public void RemoveSkills(Skill[] skills)
        {
            foreach (Skill skill in skills)
            {
                Skills.Remove(skill);
            }
        }

        public void RemoveSkills(List<Skill> skills)
        {
            foreach (Skill skill in skills)
            {
                Skills.Remove(skill);
            }
        }

        public void UseSkill(Skill skill)
        {
            if (!HasSkill(skill))
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} doesn't have {1}!", Name, skill.Name);
            }
            else if (HasEnoughSP(skill))
            {
                CurrentSP -= skill.Cost;
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} doesn't have enough SP to use {1}!", Name, skill.Name);
            }
        }

        private bool HasSkill(Skill skill)
        {
            return Skills.Contains(skill);
        }

        private bool HasEnoughSP(Skill skill)
        {
            return skill.Cost <= CurrentSP;
        }

        private bool HasFullHP()
        {
            return CurrentHP == MaxHP;
        }

        private bool HasFullSP()
        {
            return CurrentSP == MaxSP;
        }

        public void RestoreResource(Resources resource, int amount)
        {
            switch (resource)
            {
                case Resources.HP:
                    if (HasFullHP())
                    {
                        // Nisam se htio petljati sa exception-ima
                        Console.WriteLine("{0}'s HP is already full!", Name);
                    }
                    else
                    {
                        CurrentHP += amount;
                        if (HasFullHP())
                        {
                            CurrentHP = MaxHP;
                        }
                    }
                    break;
                case Resources.SP:
                    if (HasFullSP())
                    {
                        // Nisam se htio petljati sa exception-ima
                        Console.WriteLine("{0}'s SP is already full!", Name);
                    }
                    else
                    {
                        CurrentHP += amount;
                        if (HasFullSP())
                        {
                            CurrentHP = MaxHP;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    class Team
    {
        public string Name { get; private set; }
        public List<TeamMember> TeamMembers { get; private set; }
        
        public Team(string name)
        {
            Name = name;
            TeamMembers = new List<TeamMember>();
        }

        public Team(string name, TeamMember[] teamMembers)
        {
            Name = name;
            TeamMembers = new List<TeamMember>();
            foreach (TeamMember teamMember in teamMembers)
            {
                AddMember(teamMember);
            }
        }

        public Team(string name, List<TeamMember> teamMembers)
        {
            Name = name;
            TeamMembers = teamMembers;
        }

        public void AddMember(TeamMember teamMember)
        {
            if (AddMemberPossible())
            {
                TeamMembers.Add(teamMember);
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} is already full!", Name);
            }
        }

        public void RemoveMember(TeamMember teamMember)
        {
            if (RemoveMemberPossible(teamMember))
            {
                TeamMembers.Remove(teamMember);
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} is not in {1}!", teamMember.Name, Name);
            }
        }

        private bool AddMemberPossible()
        {
            return TeamMembers.Count < 3;
        }

        private bool RemoveMemberPossible(TeamMember teamMember)
        {
            return TeamMembers.Contains(teamMember);
        }

        public void RestoreResource(Resources resource, int amount)
        {
            foreach (TeamMember teamMember in TeamMembers)
            {
                teamMember.RestoreResource(resource, amount);
            }
        }
    }

    interface IItem
    {
        public void Use(Team team);
        public void Use(TeamMember teamMember);
    }

    class Item : IItem
    {
        public string Name { get; private set; }
        public Resources Resource { get; private set; }
        public int Amount { get; private set; }
        public Targets NoOfTargets { get; private set; }

        // Tu još ide Description, SetDescription i sve ostalo kao i kod AttackSkill i StatusSkill klasa,
        // ali mislim da već i ovako ima previše koda.

        public Item(string name, Resources resource, int amount, Targets noOfTargets)
        {
            Name = name;
            Resource = resource;
            Amount = amount;
            NoOfTargets = noOfTargets;
        }

        public void Use(Team team)
        {
            if (IsForTeam())
            {
                team.RestoreResource(Resource, Amount);
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} is a single-target item!", Name);
            }
        }

        public void Use(TeamMember teamMember)
        {
            if (IsForTeamMember())
            {
                teamMember.RestoreResource(Resource, Amount);
            }
            else
            {
                // Nisam se htio petljati sa exception-ima
                Console.WriteLine("{0} is a multi-target item!", Name);
            }
        }

        private bool IsForTeam()
        {
            return NoOfTargets == Targets.All;
        }

        private bool IsForTeamMember()
        {
            return NoOfTargets == Targets.One;
        }
    }
}
