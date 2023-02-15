using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    [HideInInspector]
    public static RespawnController instance;

    [SerializeField] float waitToRespawn;

    private Vector3 respawnPoint;
    private GameObject playerREF;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerREF= PlayerHealthController.Instance.gameObject;
        respawnPoint= playerREF.transform.position;
    }


    public void Respawn()
    {
        if (playerREF != null)
        {

            StartCoroutine(RespawnCo());
        }
    }

    IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(waitToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        playerREF.transform.position= respawnPoint;
        playerREF.SetActive(true);

    }

}
