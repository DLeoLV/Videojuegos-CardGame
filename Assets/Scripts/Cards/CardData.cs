using UnityEngine;

[System.Serializable]
public class CardData
{
    public string cardName;
    public int attack;
    public int defense;
    public int energy;
    public int energyAttackCost;
    public int energyRegen;
    public int retreatCost;
    public int health;
    // Constructor que inicializa todos los campos
    public CardData(
    string cardName,
    int attack,
    int defense,
    int energy,
    int energyAttackCost,
    int energyRegen,
    int retreatCost,
    int health)
    {
        this.cardName = cardName;
        this.attack = attack;
        this.defense = defense;
        this.energy = energy;
        this.energyAttackCost = energyAttackCost;
        this.energyRegen = energyRegen;
        this.retreatCost = retreatCost;
        this.health = health;
    }
    public CardData(CardData cardData)
    {
        this.cardName = cardData.cardName;
        this.attack = cardData.attack;
        this.defense = cardData.defense;
        this.energy = cardData.energy;
        this.energyAttackCost = cardData.energyAttackCost;
        this.energyRegen = cardData.energyRegen;
        this.retreatCost = cardData.retreatCost;
        this.health = cardData.health;
    }
    public CardData() // Constructor por defecto
    {
        cardName = "Default";
        attack = 0;
        defense = 0;
        energy = 0;
        energyAttackCost = 0;
        energyRegen = 0;
        retreatCost = 0;
        health = 0;
    }
    // Operador suma
    public static CardData operator +(CardData a, CardData b)
    {
        return new CardData(
        a.cardName, // Se mantiene el nombre de 'a'
        a.attack + b.attack,
        a.defense + b.defense,
        a.energy + b.energy,
        a.energyAttackCost + b.energyAttackCost,
        a.energyRegen + b.energyRegen,
        a.retreatCost + b.retreatCost,
        a.health + b.health
        );
    }
    // Operador resta
    public static CardData operator -(CardData a, CardData b)
    {
        return new CardData(
        a.cardName, // Se mantiene el nombre de 'a'
        a.attack - b.attack,
        a.defense - b.defense,
        a.energy - b.energy,
        a.energyAttackCost - b.energyAttackCost,
        a.energyRegen - b.energyRegen,
        a.retreatCost - b.retreatCost,
        a.health - b.health
        );
    }
}