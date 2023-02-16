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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerREF= PlayerHealthController.instance.gameObject;
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
        playerREF.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        playerREF.SetActive(true);
        PlayerHealthController.instance.RestorePlayerState();
        playerREF.transform.position = respawnPoint;
    }


    public void SetRespawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }


}
