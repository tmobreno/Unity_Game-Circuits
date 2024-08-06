using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [field: SerializeField] public GameObject components { get; private set; }

    public static UIGameOver Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetVisible()
    {
        components.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
