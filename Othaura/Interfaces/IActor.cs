/************************************************************
23 November 2020 - Roguesharp V5 implementation  
  
************************************************************/

using Othaura.Equipment;

namespace Othaura.Interfaces {

    public interface IActor {

        HeadEquipment Head { get; set; }
        BodyEquipment Body { get; set; }
        HandEquipment Hand { get; set; }
        FeetEquipment Feet { get; set; }

        //Number of dice to roll when performing an attack. Also max
        //dmg in one attack.
        int Attack { get; set; }

        //% chance that each dice roll is a success. A success for the 
        //dice means that 1 point of dmg was inflicted. 
        int AttackChance { get; set; }

        //FOV range
        int Awareness { get; set; }

        //Number of dice to roll when defending against an attack. Also 
        //represents the maximum amount of damage that can blocked or 
        //dodged from an incoming attack.
        int Defense { get; set; }

        //Percentage chance that each die rolled is a success. A 
        //success for a die means that 1 point of damage was blocked.
        int DefenseChance { get; set; }

        //How much money the actor has. Most monsters will drop 
        //gold upon death.
        int Gold { get; set; }

        //The current health total of the actor. If this value is 0 
        //or less then the actor is killed.
        int Health { get; set; }

        //How much health the actor has when fully healed.
        int MaxHealth { get; set; }

        //Name of the actor
        string Name { get; set; }

        //How fast the actor is. This determines the rate at which 
        //they perform actions. A lower number is faster. An actor 
        //with a speed of 10 will perform twice as many actions in 
        //the same time as an actor with a speed of 20.
        int Speed { get; set; }
    }
}
