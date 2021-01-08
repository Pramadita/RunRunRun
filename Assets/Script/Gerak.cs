using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerak : MonoBehaviour
{
    public int kecepatan; //kecepatan gerak
    public int KeLompat;
    public bool balik;
    public int pindah;
    Rigidbody2D lompat;

    //variabel sensor tanah
    public bool tanah;
    public LayerMask targetLayer;
    public Transform deteksitanah;
    public float jangkauan;

    //Animasi
    Animator anim; //sensor variabel Animator
    

    // Start is called before the first frame update
    void Start()
    {
        lompat = GetComponent<Rigidbody2D>(); //Inisialisasi awal untuk rigibody lompat
        anim = GetComponent<Animator>(); //Inisialisasi Komponen Animasi
    }

    // Update is called once per frame
    void Update()
    {
        //logik untuk animasi
        if(tanah == false)
        {
            anim.SetBool("lompat", true);
        }
        else
        {
            anim.SetBool("lompat", false);
        }

        //sensor tanah
        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetLayer);

        //control player
        if (Input.GetKey(KeyCode.D)) //Key D untuk bergerak maju / ke kanan
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            pindah = -1;
            anim.SetBool("lari", true);//Anim Lari
        }
        else if (Input.GetKey(KeyCode.A)) //Key A untuk bergerak ke belakang / ke kiri
        {
            transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            pindah = 1;
            anim.SetBool("lari", true);//Anim Lari
        }
        else if (tanah == true && Input.GetKey(KeyCode.Mouse1)) // Pergerakan sliding
        {
            if(pindah == -1)// Sliding Kanan
            { 
                transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            }
            else if(pindah ==1)//Sliding Kiri
            {
                transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            }
           
            anim.SetBool("Sliding", true); //Menjalankan sliding
        }
        else
        {
            anim.SetBool("lari", false);//jika tidak klik A dan D tidak lari
            anim.SetBool("Sliding", false); //tidak sliding
        }

        /*if(Input.GetKey(KeyCode.Mouse0))//Mouse 0 = klik kiri
        {
            lompat.AddForce(new Vector2(0,KeLompat));
        }*/

        if (tanah==true && Input.GetKey(KeyCode.Mouse0))//Mouse 0 = klik kiri
        {
            lompat.AddForce(new Vector2(0, KeLompat));
        }

        //logik balik badan
        if (pindah>0 && !balik)
        {
            flip();
        }
        else if(pindah<0 && balik)
        {
            flip();
        }
    }

    void flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }
}
