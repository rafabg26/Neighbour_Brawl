using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] objectsPrefabs;
    [SerializeField] float secondsSpawn = 0.5f;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObjects()
    {
        while(true){
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);

            GameObject obj = Instantiate(objectsPrefabs[Random.Range(0, objectsPrefabs.Length)],
            position, Quaternion.identity);

            Collider2D objCollider = obj.GetComponent<Collider2D>();
            Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();

            objCollider.isTrigger = false;
            objRigidbody.bodyType = RigidbodyType2D.Dynamic;

            yield return new WaitForSeconds(secondsSpawn);
        }
    }

    
}
