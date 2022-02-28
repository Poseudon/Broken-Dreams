using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Optionen;
    private OrbitCamera cam;
    private PlayerController player;
    public GameObject slider;

    private void Start()
    {
        cam = FindObjectOfType<OrbitCamera>();
        cam.enabled = false;
        player = FindObjectOfType<PlayerController>();
        player.enabled = false;
        Time.timeScale = 0;
    }

    public void Play()
    {
        MainMenu.SetActive(false);
        cam.enabled = true;
        player.enabled = true;
        Time.timeScale = 1;
        cam.SetRotSpeed(slider.GetComponent<Slider>().value);
    }

    public void Options()
    {
        MainMenu.SetActive(false);
        Optionen.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void back()
    {
        MainMenu.SetActive(true);
        Optionen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            MainMenu.SetActive(true);
            cam.enabled = false;
            player.enabled = false;
        }
    }
}