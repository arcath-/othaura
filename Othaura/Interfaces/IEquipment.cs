/************************************************************
01 December 2020 - Started refactor using Sadconsole
  
************************************************************/

namespace Othaura.Interfaces {

    public interface IEquipment {
        int Attack { get; set; }
        int AttackChance { get; set; }
        int Awareness { get; set; }
        int Defense { get; set; }
        int DefenseChance { get; set; }
        int Gold { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
    }
}
