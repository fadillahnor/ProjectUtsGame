using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    public bool packagePicked = false;

    public float timer = 60f;
    private bool timerRunning = false;

    void Update()
    {
        // TIMER BERJALAN
        if (timerRunning)
        {
            timer -= Time.deltaTime;

            Debug.Log("Sisa Waktu : " + Mathf.Ceil(timer));

            // JIKA WAKTU HABIS
            if (timer <= 0)
            {
                timerRunning = false;
                Debug.Log("Waktu Habis! Gagal Mengantar Paket");
            }
        }
    }

    // SAAT MENABRAK OBJECT
    private void OnTriggerEnter(Collider other)
    {
        // AMBIL PAKET
        if (other.CompareTag("Package"))
        {
            packagePicked = true;
            timerRunning = true;

            Debug.Log("Paket Diambil!");

            // HAPUS CUBE PAKET
            Destroy(other.gameObject);
        }

        // ANTAR PAKET
        if (other.CompareTag("Delivery"))
        {
            if (packagePicked)
            {
                timerRunning = false;

                Debug.Log("Paket Sudah Diantar!");

                packagePicked = false;
            }
            else
            {
                Debug.Log("Ambil paket dulu!");
            }
        }
    }
}