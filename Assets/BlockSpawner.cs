using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public float widthX = 10f;
    public float widthZ = 10f;
    public float minWidthY = 5f;
    public float maxWidthY = 40f;
    public int numBlocks = 5;


    // Start is called before the first frame update
    void Start()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        for (int i = 0; i < numBlocks; i++)
        {
            GameObject tempCube = Instantiate(cube, transform.position, Quaternion.identity);

            //inclusive
            int widthY = (int)Random.Range(minWidthY, maxWidthY);

            Debug.Log(widthY);
            tempCube.transform.localScale = new Vector3(widthX, widthY, widthZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
