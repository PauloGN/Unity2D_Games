using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{

    [SerializeField] ScriptableKey keyInfo;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Inventory.Instance.HasKey(keyInfo))
            {
                GetComponent<Animator>().SetBool("OpenDoor", true);
                StartCoroutine(DesableColission());
            }
        }
    }

    IEnumerator DesableColission()
    {
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
