using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ArabaHareket : MonoBehaviour
{

    bool oyunBitti = false;
    bool dTusunaBasildi =false;
    bool aTusunaBasildi=false;
    private Rigidbody rigidbodyComponent;
    AudioSource audioSource;

    public int puan = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        puan = 0;
        rigidbodyComponent.velocity = new Vector3(-30, 0, 0);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("d"))
        {
            dTusunaBasildi = true;
            
        }

        if (Input.GetKey("a"))
        {
            aTusunaBasildi = true;
            
        }



    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Engel")
        {
            Invoke("restart", 3f);
            oyunBitti = true;
            audioSource.Play();
        }

        if (collision.collider.tag == "Coin")
        {
            puan++;
            Destroy(collision.gameObject);
        }
    }


    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        oyunBitti = false;
    }

    private void FixedUpdate()
    {
        if (oyunBitti == false)


            rigidbodyComponent.velocity = new Vector3(-30, 0, GetComponent<Rigidbody>().velocity.z);

        else if (oyunBitti == true)

            rigidbodyComponent.velocity = Vector3.zero;

        if (dTusunaBasildi)
        {
            rigidbodyComponent.AddForce(0, 0, 60, ForceMode.Force);
            dTusunaBasildi=false;
        }

        if (aTusunaBasildi)
        {
            rigidbodyComponent.AddForce(0, 0, -60, ForceMode.Force);
            aTusunaBasildi =false;
        }

        if ((rigidbodyComponent.position.x <= -90) || (rigidbodyComponent.position.z<=-10 || rigidbodyComponent.position.z >= 10))
        {
            oyunBitti = true;
            rigidbodyComponent.velocity = Vector3.zero;
            Invoke("restart", 3f);

        }
    }
}
