using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> bones = null;

    public void UpdateLifes(int amount)
    {
        int total = 0;

        for (int i = bones.Count - 1; i >= 0; i--)
        {
            if (bones[i].activeSelf)
            {
                bones[i].SetActive(false);
                total++;

                if(total >= amount)
                {
                    return;
                }
            }
        }
    }
}
