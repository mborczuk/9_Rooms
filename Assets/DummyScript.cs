using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyScript : MonoBehaviour
{
    public void ChangeScene() {
        SceneManager.LoadScene("Room 1");
    }
}
