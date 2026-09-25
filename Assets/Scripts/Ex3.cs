using UnityEngine;

public class Ex3 : MonoBehaviour
{
    public class Gazelle
    {
        private int _healthPoints = 2;
        
        public void ReceiveDamage(int damage)
        {
            _healthPoints -= damage;
            if (_healthPoints <= 0)
                Debug.Log("Dinner Time !");
        }
    }

    public class Lion
    {
        public int Power;
        
        public void Attack(Gazelle gazelle)
        {
            gazelle.ReceiveDamage(Power);
        }
    }

    private void Start()
    {
        Lion butcher = new Lion();
        butcher.Power = 5;
        
        Gazelle nextMeal = new Gazelle();
        
        butcher.Attack(nextMeal);
    }
    
}
