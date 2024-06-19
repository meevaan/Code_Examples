using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
    public Vector3 thisPos;
    public Vector3 newPos;

    public float waitTime;
    public float clipLength;

    public AudioSource thisAS;

    public int listNum;

    public List<AudioClip> forestSounds = new List<AudioClip>();
    public List<AudioClip> woodSounds = new List<AudioClip>();
    public bool onWood;

    // Start is called before the first frame update
    void Start()
    {
        thisPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        clipLength = thisAS.clip.length;
        waitTime += Time.deltaTime;

        thisPos = this.transform.position;

        if(waitTime >= clipLength)
        {
            newPos = this.transform.position;
            waitTime = 0f;
        }

        if(waitTime <= clipLength && thisAS.isPlaying == false)
        {
            if(newPos.x != thisPos.x || newPos.z != thisPos.z)
            {
                if(onWood == true)
                {
                    listNum = Random.Range(0, woodSounds.Count);
                    thisAS.clip = woodSounds[listNum];
                    thisAS.Play();
                }
                if(onWood == false)
                {
                    listNum = Random.Range(0, forestSounds.Count);
                    thisAS.clip = forestSounds[listNum];
                    thisAS.Play();
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Wood")
        {
            onWood = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Wood")
        {
            onWood = false;
        }
    }
}
