using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkWithCharacter : MonoBehaviour
{
    public TMP_Text charText;
    public float delay;
    public string fullText;
    private string currentText = "";

    public List<string> allTalking = new List<string>();

    public int currentCaseNum;

    public bool thisCharTalking;
    private bool goNextText;
    public GameObject theirTWC;

    // UI Stuff
    public GameObject convoStuff;

    public Image thisImage;

    private SpriteRenderer thisSR;

    // Start is called before the first frame update
    void Start()
    {
        thisSR = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // This is how the text is viewed
        if(allTalking[currentCaseNum] != "" && allTalking[currentCaseNum] != "END CONVO")
        {
            thisCharTalking = true;
            charText.text = currentText;
        }
        else
        {
            thisCharTalking = false;
        }

        if(thisCharTalking == true)
        {
            charText.color = new Color(1f, 1f, 1f, 1f);
            thisImage.color = new Color(1f, 1f, 1f, 1f);
        }
        if(thisCharTalking == false)
        {
            charText.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            thisImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }

        if(Input.GetMouseButtonDown(0) && goNextText == true && currentCaseNum >= 1)
        {
            //Pressing is how to move conversation along
            StartCoroutine(GoToConvo(currentCaseNum-1));
            goNextText = false;
        }

        if(theirTWC != null)
        {
            if(theirTWC.GetComponent<TalkWithCharacter>().goNextText == true)
            {
                goNextText = true;
            }
        }

        if(allTalking[currentCaseNum] == "END CONVO")
        {
            StopCoroutine(GoToConvo(currentCaseNum));
            thisCharTalking = false;
            goNextText = false;
            currentText = "";
            fullText = "";
            convoStuff.SetActive(false);
            currentCaseNum = 0;
        }
    }

    public IEnumerator GoToConvo(int caseNum)
    {
        //change character image to fit first
        thisImage.sprite = thisSR.sprite;
        thisImage.preserveAspect = true;
        thisImage.SetNativeSize();

        //then handle writing the text
        currentCaseNum = caseNum;
        currentText = "";
        fullText = allTalking[caseNum];

        if(allTalking[caseNum] != "")
        {
            if(currentText.Length != fullText.Length-1)
            {
                for(int i = 0; i < fullText.Length; i++)
                {
                    currentText = fullText.Substring(0 , i);

                    yield return new WaitForSeconds(delay);

                    if(currentText.Length == fullText.Length-1)
                    {
                        goNextText = true;
                        yield break;
                    }
                }
            }
        }
    }
}
