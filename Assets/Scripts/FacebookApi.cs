using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FacebookApi : MonoBehaviour
{
    private static readonly string _facebookAppId = "563703407151199";
    private static readonly string _redirectEndpoint = "http://joesbabysitting.com/thankyou.php";
    private static readonly string _appSecret = "933bcc85980579c103abcf53705faee3";

    //Test Version
    //private static readonly string _facebookAppId = "563723290482544";
    //private static readonly string _redirectEndpoint = "http://joesbabysitting.com/thankyou.php";
    //private static readonly string _appSecret = "22d0a95c39adadc36d0307fb6cd77cd4";

    private string access_code;
    private string access_token;
    private string user_id;

    private AuthCode codeObj;
    private AccessToken accessTokenObj;
    private UserObj userObj;

    private TextMesh fbName;
    private MeshRenderer fbPicture;

    public TextMesh idCardName;
    public MeshRenderer idCardPicture;

    public TextMesh chequeName;

    public void Awake()
    {
        //this.Login();
        StartCoroutine(parseCode());

        //fbName = GameObject.Find("Facebook Name").GetComponent<TextMesh>();
        fbPicture = GameObject.Find("VictimPicture").GetComponent<MeshRenderer>();
    }



    /*
     * GET Auth Code and Save in Database
     */
    public void Login()
    {
        var token = Guid.NewGuid().ToString();
        //Debug.Log(token);
        Application.OpenURL(String.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&state={2}", _facebookAppId, _redirectEndpoint, "c8824f0e-54db-8119-b157-410ff5570d75"));
    }



    /*
     * 1. Get latest auth code from database
     */
    public IEnumerator parseCode()
    {
        WWW respJson = new WWW("http://joesbabysitting.com/getAccessToken.php");

        yield return respJson;

        //.Log(respJson.text);

        codeObj = AuthCode.CreateFromJson(respJson.text);
        access_code = codeObj.authCode;

       // Debug.Log(access_code);

        StartCoroutine(retrieveUserDetails());
    }



    /*
     * Retrieve access token using auth code
     */
    public IEnumerator retrieveAccessToken()
    {

        //Debug.Log(String.Format("https://graph.facebook.com/v2.8/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", _facebookAppId, _redirectEndpoint, _appSecret, access_code));
        WWW accessTokenJson = new WWW(String.Format("https://graph.facebook.com/v2.8/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", _facebookAppId, _redirectEndpoint, _appSecret, access_code));
        //yield return null;
        yield return accessTokenJson;

        accessTokenObj = AccessToken.CreateFromJson(accessTokenJson.text);
        access_token = accessTokenObj.access_token;

        //Debug.Log(access_token);

        StartCoroutine(retrieveUserDetails());
    }




    /*
     * Retrieve user details using access token and also get the profile picture
     */
    public IEnumerator retrieveUserDetails()
    {
        WWW userDetailsJson = new WWW("http://joesbabysitting.com/getLatestUser.php");

        yield return userDetailsJson;

        Debug.Log(userDetailsJson.text);

        userObj = UserObj.CreateFromJson(userDetailsJson.text);
        user_id = userObj.id;

        //Debug.Log(user_id);

        //SET NAME TO ID CARD
        idCardName.text = userObj.first_name + "\n" + userObj.last_name;
        chequeName.text = userObj.first_name.ToUpper() + " " + userObj.last_name.ToUpper();

        StartCoroutine(retrieveUserPicture(userObj.picture_url));

    }





    /* 
     * Retrieve user picture
     */
    public IEnumerator retrieveUserPicture(string picture_url)
    {
        Debug.Log(picture_url);
        WWW profilePicture = new WWW(picture_url);
        yield return profilePicture;

        //Debug.Log(profilePicture.text);
        Texture2D tex = new Texture2D(100, 100, TextureFormat.DXT1, false);

        while (!profilePicture.isDone) { };
        profilePicture.LoadImageIntoTexture(tex);

        renderUserInfo(tex);
    }




    /*
     * Render User information
     */
    public void renderUserInfo(Texture2D tex)
    {
        idCardPicture.material.mainTexture = tex;
        fbPicture.material.mainTexture = tex;
        
        

        //fbName.text = "Hello\n" + userObj.first_name + "\n" + userObj.last_name.ToString();


    }


}