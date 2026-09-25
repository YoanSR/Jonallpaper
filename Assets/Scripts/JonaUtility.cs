using System.Collections.Generic;
using UnityEngine;

public static class JonaUtility 
{
    public static float EvaluateVector2(Vector2 vector, float time)
    {
        return vector.x + (vector.y - vector.x) * time;
    }
    public static float EvaluateFloat(float x, float y, float time)
    {
        return x + (y- x) * time;
    }

    public static int RandomRound(float num)
    {
        int num2 = Mathf.FloorToInt(num);
        return num2 + (Random.Range(1,100) <= (num - num2) * 100 ?  1 : 0);
    }

    public static string GetListString<TVar>(List<TVar>list)
    {
        string output = string.Empty;
        foreach (var item in list)
        {
            output += item.ToString() + " , ";
        }
        return output;
    }
}
