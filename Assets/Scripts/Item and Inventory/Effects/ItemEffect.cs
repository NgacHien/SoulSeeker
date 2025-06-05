using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item Data", menuName = "Data/Item effect")]


public class ItemEffect : ScriptableObject
{
    [TextArea]
    public string effectDescription;

   public virtual void ExecuteEffect(Transform _enemyPosition)
    {
        Debug.Log("Effect executed");
    }
}
