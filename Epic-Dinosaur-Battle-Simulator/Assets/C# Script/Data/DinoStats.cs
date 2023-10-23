using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class OneDinoStat
{
    public string name;
    public int attack;
    public int hp;
    public float speed;
    public int price;
    public string description;

    public OneDinoStat(string name, int attack, int hp, float speed, int price)
    {
        this.name = name;
        this.attack = attack;
        this.hp = hp;
        this.speed = speed;
        this.price = price;
    }

    public OneDinoStat(string name, int attack, int hp, float speed, int price, string description) : this(name, attack, hp, speed, price)
    {
        this.description = description;
    }
}

public class DinoStats
{
    private List<OneDinoStat> dinosaurs = new List<OneDinoStat>();

    public List<OneDinoStat> Dinosaurs { get => dinosaurs; set => dinosaurs = value; }

    public DinoStats() 
    {
        Dinosaurs.Add(new OneDinoStat("Allosaurus fragilis",15,40,5f,200));
        Dinosaurs.Add(new OneDinoStat("Diplodocus", 10, 150, 2f, 500));
        Dinosaurs.Add(new OneDinoStat("Stegosaurus", 20, 50, 3f, 450));
        Dinosaurs.Add(new OneDinoStat("Triceratops", 15, 50, 3f, 300));
        Dinosaurs.Add(new OneDinoStat("T-rex", 40, 120, 4f, 650));
        Dinosaurs.Add(new OneDinoStat("Spinozaur", 25, 27, 5, 11500));
        Dinosaurs.Add(new OneDinoStat("Apatosaurus", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Barosaurus", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Stegoceras", 5, 5, 5, 310));
        Dinosaurs.Add(new OneDinoStat("Dracorex", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Yaverlandia", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Tarchia", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Edmontonia", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Ojoceratops", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Kosmoceratops", 5, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Ornitholestes hermanni", 0, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Sinornithosaurus", 0, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Anchiornis", 0, 5, 5f, 500));
        Dinosaurs.Add(new OneDinoStat("Nest of eggs", 0, 5, 5, 500));
        Dinosaurs.Add(new OneDinoStat("Egg", 0, 5, 5, 500));
    }
}
