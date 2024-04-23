using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;

    [SerializeField] private GameObject dynaPrefab;

    [SerializeField] private GameObject gatherMusicPrefab;

    [SerializeField] private GameObject bossMusicPrefab;

    [SerializeField] private GameObject victoryMusicPrefab;

    [SerializeField] private GameObject defeatMusicPrefab;

    private GameObject box;

    private GameObject dyna;

    private GameObject gatheringJuke;

    private GameObject bossJuke;

    private GameObject victoryJuke;

    private GameObject defeatJuke;

    private GameObject[] gos;

    private int maxBoxes = 20;

    public int collectedBoxes = 0;

    private bool danger = false;

    public bool dynaDown = false;

    public bool defeated = false;

    void Start()
    {
        gos = GameObject.FindGameObjectsWithTag("Box");
        gatheringJuke = Instantiate(gatherMusicPrefab) as GameObject;
        gatheringJuke.SetActive(true);
        bossJuke = Instantiate(bossMusicPrefab) as GameObject;
        bossJuke.SetActive(false);
        victoryJuke = Instantiate(victoryMusicPrefab) as GameObject;
        victoryJuke.SetActive(false);
        defeatJuke = Instantiate(defeatMusicPrefab) as GameObject;
        defeatJuke.SetActive(false);
    }

    void Update()
    {
        gos = GameObject.FindGameObjectsWithTag("Box");

        if (gos.Length < maxBoxes)
        {
            SpawnBox();
        }

        if (collectedBoxes >= 10 && !danger)
        {
            SpawnDyna();
            bossJuke.SetActive(true);
            gatheringJuke.SetActive(false);
            danger = true;
        }

        if (dynaDown)
        {
            victoryJuke.SetActive(true);
            bossJuke.SetActive(false);
        }

        if (defeated)
        {
            defeatJuke.SetActive(true);
            gatheringJuke.SetActive(false);
            bossJuke.SetActive(false);
            victoryJuke.SetActive(false);
        }
    }

    private void SpawnBox()
    {
        box = Instantiate(boxPrefab) as GameObject;
        box.transform.position = new Vector3((Random.Range(-400, 400)), (Random.Range(120, 200)), (Random.Range(-400, 400)));
    }

    private void SpawnDyna()
    {
        dyna = Instantiate(dynaPrefab) as GameObject;
        dyna.transform.position = new Vector3(-500f, 300f, 0f);
    }
}
