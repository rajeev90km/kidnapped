using UnityEngine;
using System.Collections;

public class PassCode : MonoBehaviour {

    private float lastTimeOfEnterCode;
    public TextMesh passCodeDisplay;

    public string correctPassCode = "1211";

    private bool waitForCodeCheck = false;

    private bool isCorrectCodeEntered = false;

    public bool IsCodeUnlocked()
    {
        return isCorrectCodeEntered;
    }

    // Use this for initialization
    void Start () {
	
	}

    public void EnterCode(int codeValue)
    {
        if (isCorrectCodeEntered == false)
        {
            if (waitForCodeCheck == false)
            {
                if (Time.time - lastTimeOfEnterCode < 0.5f) // Can only press button every 0.75 seconds
                {
                    return;
                }

                lastTimeOfEnterCode = Time.time;

                if (passCodeDisplay.text == "----")
                {
                    passCodeDisplay.text = codeValue.ToString();
                }
                else
                {
                    passCodeDisplay.text += codeValue;
                }

                if (passCodeDisplay.text.Length == 4)
                {
                    waitForCodeCheck = true;
                    StartCoroutine(checkCode((passCodeDisplay.text)));
                }
            }
        }
    }

    public IEnumerator checkCode(string code)
    {
        Debug.Log(code + "  " + correctPassCode);
        yield return new WaitForSeconds(0.75f);
        if (code.CompareTo(correctPassCode) == 0)
        {
            passCodeDisplay.text = "CORRECT";
            //Door open Sound
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(4f);
            passCodeDisplay.text = ""; ;
            waitForCodeCheck = false;
            isCorrectCodeEntered = true;

        }
        else
        {
            passCodeDisplay.text = "RETRY";
            yield return new WaitForSeconds(1f);
            passCodeDisplay.text = "----";
            waitForCodeCheck = false;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
