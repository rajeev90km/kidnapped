using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsController : MonoBehaviour {

    public GameObject _sunLight;
    public bool _sunDown = false;
    public bool _programmerPhotoFadeIn = false;
    public bool _programmerPhotoFadeOut = false;
    public bool _artistPhotoFadeIn = false;
    public bool _artistPhotoFadeOut = false;
    public bool _producerPhotoFadeIn = false;
    public bool _producerPhotoFadeOut = false;
    public bool _soundActingFadeIn = false;
    public bool _soundActingFadeOut = false;
    public bool _endingTextFadeIn = false;
    public bool _endingTextFadeOut = false;
    public float MIN_SUN_HEIGHT;

    public Image _joePhoto;
    public Image _rajeevPhoto;
    public Image _yiwenPhoto;
    public Image _jasonPhoto;
    public Image _lucasPhoto;
    public Text _programmerText;
    public Text _artistText;
    public Text _producerText;
    public Text _soundActingText;
    public Text _endingText;
    private float _displayAlpha = 0;

    

	// Use this for initialization
	void Start () {

        UpdateColor(_joePhoto);
        UpdateColor(_rajeevPhoto);
        UpdateColor(_programmerText);
        UpdateColor(_jasonPhoto);
        UpdateColor(_lucasPhoto);
        UpdateColor(_artistText);
        UpdateColor(_yiwenPhoto);
        UpdateColor(_producerText);
        UpdateColor(_soundActingText);
        UpdateColor(_endingText);
        StartCoroutine(fadeInProgrammer());
        StartCoroutine(fadeOutProgrammer());
        StartCoroutine(fadeInArtist());
        StartCoroutine(fadeOutArtist());
        StartCoroutine(fadeInProducer());
        StartCoroutine(fadeOutProducer());
        StartCoroutine(fadeInVoiceActing());
        StartCoroutine(fadeOutVoiceActing());
        StartCoroutine(fadeInEnding());
        StartCoroutine(fadeOutEnding());
    }

    void UpdateColor(Text target)
    {
        target.color = new Color(target.color.r, target.color.g, target.color.b, _displayAlpha);
    }

    void UpdateColor(Image target)
    {
        target.color = new Color(target.color.r, target.color.g, target.color.b, _displayAlpha);
    }

    IEnumerator fadeInProgrammer()
    {
        yield return new WaitForSeconds(0f);
        _programmerPhotoFadeIn = true;
        _sunDown = true;
    }

    IEnumerator fadeOutProgrammer()
    {
        yield return new WaitForSeconds(5f);
        _programmerPhotoFadeOut = true;
    }

    IEnumerator fadeInArtist()
    {
        yield return new WaitForSeconds(8f);
        _artistPhotoFadeIn = true;
    }

    IEnumerator fadeOutArtist()
    {
        yield return new WaitForSeconds(13f);
        _artistPhotoFadeOut = true;
    }


    IEnumerator fadeInProducer()
    {
        yield return new WaitForSeconds(16f);
        _producerPhotoFadeIn = true;
    }

    IEnumerator fadeOutProducer()
    {
        yield return new WaitForSeconds(21f);
        _producerPhotoFadeOut = true;
    }


    IEnumerator fadeInVoiceActing()
    {
        yield return new WaitForSeconds(24f);
        _soundActingFadeIn = true;
    }

    IEnumerator fadeOutVoiceActing()
    {
        yield return new WaitForSeconds(29f);
        _soundActingFadeOut = true;
    }

    IEnumerator fadeInEnding()
    {
        yield return new WaitForSeconds(32f);
        _endingTextFadeIn = true;
    }

    IEnumerator fadeOutEnding()
    {
        yield return new WaitForSeconds(38f);
        _endingTextFadeOut = true;
    }


    // Update is called once per frame
    void Update () {
        if (_sunDown) {
            _sunLight.transform.Translate(Vector3.down * Time.deltaTime * 0.05f);
            Color sunLightColor = _sunLight.GetComponent<Light>().color;
            _sunLight.GetComponent<Light>().color = new Color(sunLightColor.r, sunLightColor.g - Time.deltaTime * 0.03f, sunLightColor.b);
            
            if (_sunLight.transform.position.y < MIN_SUN_HEIGHT)
            {
                _sunDown = false;
            }
        }

        if (_programmerPhotoFadeIn)
        {
            _displayAlpha += 0.01f;
            if (_displayAlpha > 1)
            {
                _displayAlpha = 1;
                _programmerPhotoFadeIn = false;
            }
            UpdateColor(_joePhoto);
            UpdateColor(_rajeevPhoto);
            UpdateColor(_programmerText);
        }

        if (_programmerPhotoFadeOut)
        {
            _displayAlpha -= 0.01f;
            if (_displayAlpha < 0)
            {
                _displayAlpha = 0;
                _programmerPhotoFadeOut = false;
            }
            UpdateColor(_joePhoto);
            UpdateColor(_rajeevPhoto);
            UpdateColor(_programmerText);
        }

        if (_artistPhotoFadeIn)
        {
            _displayAlpha += 0.01f;
            if (_displayAlpha > 1)
            {
                _displayAlpha = 1;
                _artistPhotoFadeIn = false;
            }
            UpdateColor(_jasonPhoto);
            UpdateColor(_lucasPhoto);
            UpdateColor(_artistText);
        }

        if (_artistPhotoFadeOut)
        {
            _displayAlpha -= 0.01f;
            if (_displayAlpha < 0)
            {
                _displayAlpha = 0;
                _artistPhotoFadeOut = false;
            }
            UpdateColor(_jasonPhoto);
            UpdateColor(_lucasPhoto);
            UpdateColor(_artistText);
        }

        if (_producerPhotoFadeIn)
        {
            _displayAlpha += 0.01f;
            if (_displayAlpha > 1)
            {
                _displayAlpha = 1;
                _producerPhotoFadeIn = false;
            }
            UpdateColor(_yiwenPhoto);
            UpdateColor(_producerText);
        }

        if (_producerPhotoFadeOut)
        {
            _displayAlpha -= 0.01f;
            if (_displayAlpha < 0)
            {
                _displayAlpha = 0;
                _producerPhotoFadeOut = false;
            }
            UpdateColor(_yiwenPhoto);
            UpdateColor(_producerText);
        }

        if (_soundActingFadeIn)
        {
            _displayAlpha += 0.01f;
            if (_displayAlpha > 1)
            {
                _displayAlpha = 1;
                _soundActingFadeIn = false;
            }
            UpdateColor(_soundActingText);
        }

        if (_soundActingFadeOut)
        {
            _displayAlpha -= 0.01f;
            if (_displayAlpha < 0)
            {
                _displayAlpha = 0;
                _soundActingFadeOut = false;
            }
            UpdateColor(_soundActingText);
        }

        if (_endingTextFadeIn)
        {
            _displayAlpha += 0.01f;
            if (_displayAlpha > 1)
            {
                _displayAlpha = 1;
                _endingTextFadeIn = false;
            }
            UpdateColor(_endingText);
        }

        if (_endingTextFadeOut)
        {
            _displayAlpha -= 0.01f;
            if (_displayAlpha < 0)
            {
                _displayAlpha = 0;
                _endingTextFadeOut = false;
            }
            UpdateColor(_endingText);
        }

    }

    
}
