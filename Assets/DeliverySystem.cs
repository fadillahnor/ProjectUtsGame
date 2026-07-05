using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeliverySystem : MonoBehaviour
{
    [Header("Package State")]
    public bool packagePicked = false;

    [Header("Timer")]
    public float timer = 60f;
    private bool timerRunning = false;

    [Header("Game State")]
    private bool isPaused = false;

    [Header("UI TextMeshPro")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI packageText;
    public TextMeshProUGUI infoText;

    [Header("Scene")]
    public string mainMenuSceneName = "mainmenu";

    private void Start()
    {
        Time.timeScale = 1f;

        UpdateUI();
        ShowInfo("Ambil paket terlebih dahulu!");
    }

    private void Update()
    {
        if (isPaused)
            return;

        if (timerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                timerRunning = false;
                packagePicked = false;

                ShowInfo("Waktu Habis! Gagal Mengantar Paket");
            }

            UpdateUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPaused)
            return;

        // AMBIL PAKET
        if (other.CompareTag("Package"))
        {
            packagePicked = true;
            timerRunning = true;

            ShowInfo("Paket Diambil! Segera antar paket!");

            Destroy(other.gameObject);

            UpdateUI();
        }

        // ANTAR PAKET
        if (other.CompareTag("Delivery"))
        {
            if (packagePicked)
            {
                timerRunning = false;
                packagePicked = false;

                ShowInfo("Paket Sudah Diantar! Kembali ke Main Menu...");

                UpdateUI();

                Time.timeScale = 1f;
                SceneManager.LoadScene(mainMenuSceneName);
            }
            else
            {
                ShowInfo("Ambil paket dulu!");
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        UpdateUI();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = "Sisa Waktu: " + Mathf.Ceil(timer).ToString();
        }

        if (packageText != null)
        {
            if (packagePicked)
            {
                packageText.text = "Status Paket: Sudah Diambil";
            }
            else
            {
                packageText.text = "Status Paket: Belum Diambil";
            }
        }
    }

    private void ShowInfo(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
        }

        Debug.Log(message);
    }
}