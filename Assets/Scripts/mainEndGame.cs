using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainEndGame : MonoBehaviour
{
    int time = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(time);
        load();
    }
    private void load(){
        SceneManager.LoadScene("mainMenu");
    }
}
