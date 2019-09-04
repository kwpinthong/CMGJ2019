using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenuController : MonoBehaviour
{
    public string LoadScene;
    private KeyCode enter = KeyCode.KeypadEnter;

    void Update()
    {
        if (Input.GetKeyDown(enter)) {
            StartCoroutine(LoadStage());
        }
    }

private IEnumerator LoadStage() {
        
        //เล่นเสียงตอนเปลี่ยนฉาก โดยกำหนดให้  Volume เท่ากับ 1 (ดังเต็มที่)
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
        audioSource.Play();

        //รอจดกว่าเสียงตอนเปลี่ยนฉากจะจบ แล้วรออีกประมาณ 0.5 วินาที แล้วเปลี่ยนฉาก
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        SceneManager.LoadScene(LoadScene);
    }
}
