using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class JonaGameManager : MonoBehaviour
{
    public ulong Jonathan;
    [SerializeField] private TextMeshProUGUI _jonaText;
    [SerializeField] private TextMeshProUGUI _jonaPerSecondText;
    [Header("Gameplay")] 
    [SerializeField] private int _clickStrengh;

    [Header("UI")] 
    [SerializeField] private float jnSUpdate;
    
    private float _timeSinceUpdate;
    private ulong _jonaGain;
    
    public void Update()
    {
        _timeSinceUpdate += Time.deltaTime;
        if (_timeSinceUpdate >= jnSUpdate)
        {
            _timeSinceUpdate = 0;
            _jonaPerSecondText.text = NumFormater(_jonaGain) + " J/s";
            _jonaGain = 0;
        }
    }
    public void PlayerClick()
    {
        JonaGain((ulong)Mathf.Pow(2f,_clickStrengh));
        UpdateJonaText();
    }
    public void JonaGain(ulong gain)
    {
        Jonathan += gain;
        _jonaGain += gain;
        UpdateJonaText();
        for(ulong i = 0; i < gain; i++) JonathanCreator.Instance.CreateJonathan();
    }
    public void UpdateJonaText()
    {
        _jonaText.text = ": " + NumFormater(Jonathan);
    }
    public string NumFormater(ulong num)
    {
        switch (num)
        {
            case > 1_000_000_000_000:
            {
                return num/1_000_000_000_000+ " t";
            }
            case > 1_000_000_000:
            {
                return num/1_000_000_000+ " b";
            }
            case > 1_000_000:
            {
                return num/1_000_000+ " m";
            }
            case > 1_000:
            {
                return num/1_000 + " k";
            }
            default:
            {
                return num.ToString();
            }
        }
    }

    
}