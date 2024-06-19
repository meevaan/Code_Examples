using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBushes : MonoBehaviour
{
    public AudioSource thisAS;

    public int listNum;

    public List<AudioClip> bushSounds = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Bush")
        {
            listNum = Random.Range(0, bushSounds.Count);
            thisAS.clip = bushSounds[listNum];
            thisAS.Play();
        }
    }
}
