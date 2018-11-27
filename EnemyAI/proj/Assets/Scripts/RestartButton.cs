using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartButton : MonoBehaviour {

    public void restart()
    {
        SceneManager.LoadScene("SunnyLand");
    }
}
