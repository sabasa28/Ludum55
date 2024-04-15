using System;
using UnityEngine;

public class CatSpawnerHandler : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private Transform catsHolder = null;

    private GameObject catPrefab = null;
    private Transform playerTrans = null;
    private bool canSpawn = false;

    private Action onResetButtons = null;

    public void Initialize(Transform playerTrans, Action onResetButtons)
    {
        this.playerTrans = playerTrans;
        this.onResetButtons = onResetButtons;
    }

    public void GenerateCat(GameObject catPrefab)
    {
        this.catPrefab = catPrefab;
        canSpawn = true;
    }

    public void UpdateInput()
    {
        if(Input.GetMouseButtonDown(0) && canSpawn)
        {
            onResetButtons.Invoke();

            GameObject SpawnedGO = Instantiate(catPrefab, playerTrans.position, Quaternion.identity, catsHolder);
            Cat SpawnedCat = SpawnedGO.GetComponent<Cat>();
            if (SpawnedCat)
            { 
                SpawnedCat.TargetPosition = Utilities.GetMousePositionInWorld(Camera.main);
            }

            canSpawn = false;
        }
    }
}
