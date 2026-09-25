using System;
using UnityEngine;

public class Ex1 : MonoBehaviour
{
    public class Cat
    {
        private string Name;
        private string Color;
        private int Weight;

        public Cat(string Name, string Color, int Weight)
        {
            this.Name = Name;
            this.Color = Color;
            this.Weight = Weight;
        }
        public void Display()
        {
            Debug.Log(Name + " is " + Color + " and weights " + Weight + " kilos" );
        }
    }

    private void Start()
    {
        var Picatso = new Cat("Picatso", "white", 10);
        Picatso.Display();
    }
}
