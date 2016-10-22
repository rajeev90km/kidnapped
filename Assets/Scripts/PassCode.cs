using UnityEngine;
using System.Collections;

public class PassCode : MonoBehaviour {

    private float lastTimeOfEnterCode;
    public TextMesh passCodeDisplay;

    private bool waitForCodeCheck = false;

    // Use this for initialization
    void Start () {
	
	}

    public void EnterCode(int codeValue)
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
                StartCoroutine(checkCode(int.Parse(passCodeDisplay.text)));
            }
        }
    }

    public IEnumerator checkCode(int code)
    {
        yield return new WaitForSeconds(0.75f);
        passCodeDisplay.text = "RETRY";
        yield return new WaitForSeconds(1f);
        passCodeDisplay.text = "----";
        waitForCodeCheck = false;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
