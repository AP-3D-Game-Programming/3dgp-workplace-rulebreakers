using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    public Button restartButton;
    public MouseMovement mouseMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mouseMovement.locked = !mouseMovement.locked;
            Cursor.lockState = mouseMovement.locked ? CursorLockMode.Locked : CursorLockMode.None;
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestatGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
