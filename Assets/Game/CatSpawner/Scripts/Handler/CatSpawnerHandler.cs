using UnityEngine;

public class CatSpawnerHandler : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private Transform catsHolder = null;

    [Header("Custom Values")]
    [SerializeField] private float offset = 5f;

    private GameObject catPrefab = null;
    private Transform playerTrans = null;
    private bool canSpawn = false;

    public void Initialize(Transform playerTrans)
    {
        this.playerTrans = playerTrans;
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
            Instantiate(catPrefab, catsHolder).transform.position = playerTrans.position;
            canSpawn = false;
        }
    }
}
