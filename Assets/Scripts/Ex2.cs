using UnityEngine;

public class Ex2 : MonoBehaviour
{
    public class Car
    {
        private int _speed;
        private int _power;
        private int _maxSpeed;
        
        public Car(int speed, int power, int maxSpeed)
        {
            _power = power;
            _maxSpeed = maxSpeed;
        }

        private void CheckMaxSpeed()
        {
            if (_speed > _maxSpeed)
                _speed = _maxSpeed;
        }

        public void Accelerate()
        {
            _speed += _power;
            CheckMaxSpeed();
            Debug.Log("Vitesse actuelle " + _speed);
        }
    }

    private void Start()
    {
        var clio = new Car(0, 50, 130);
        clio.Accelerate();
        clio.Accelerate();
        clio.Accelerate();
    }
}
