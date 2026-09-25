using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class JonaUpgradeManager : MonoBehaviour
{
    [SerializeField] private List<JonaUpgrades> _currentJonaUpgrades = new List<JonaUpgrades>();
    [SerializeField] private Dictionary<JonaUpgrades,int> _jonaUpgradesCount = new Dictionary<JonaUpgrades,int>();




    public void AddJonaUpgrades(JonaUpgrades jonaUpgrades, int count = 1)
    {
        if (count < 0)
        {
            RemoveJonaUpgrades(jonaUpgrades, count * -1);
            return;
        }
        if (!_currentJonaUpgrades.Contains(jonaUpgrades))
        {
            _currentJonaUpgrades.Add(jonaUpgrades);
        }

        if (!_jonaUpgradesCount.ContainsKey(jonaUpgrades))
        {
            _jonaUpgradesCount.Add(jonaUpgrades,count);
        }
        else
        {
            _jonaUpgradesCount[jonaUpgrades] += count;
        }
    }
    public void RemoveJonaUpgrades(JonaUpgrades jonaUpgrades, int count = 1)
    {
        if (count < 0)
        {
            AddJonaUpgrades(jonaUpgrades, count * -1);
            return;
        }
        if (!_currentJonaUpgrades.Contains(jonaUpgrades))
        {
            return;
        }
        if (_jonaUpgradesCount.ContainsKey(jonaUpgrades))
        {
            _jonaUpgradesCount[jonaUpgrades] -= count;
            if (_jonaUpgradesCount[jonaUpgrades] <= 0)
            {
                _currentJonaUpgrades.Remove(jonaUpgrades);
                _jonaUpgradesCount.Remove(jonaUpgrades);
            }
        }
    }
}
