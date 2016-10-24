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

    private string access_code;
    private string access_token;
    private string user_id;

    private AuthCode codeObj;
    private AccessToken accessTokenObj;
    private UserObj userObj;

    private TextMesh fbName;
    private MeshRenderer fbPicture;

    public void Awake()
    {
        //this.Login();
        StartCoroutine(parseCode());
        Debug.Log("asdasd");

        //fbName = GameObject.Find("Facebook Name").GetComponent<TextMesh>();
        fbPicture = GameObject.FindGameObjectWithTag("VictimPicture").GetComponent<MeshRenderer>();
    }



    /*
     * GET Auth Code and Save in Database
     */
    public void Login()
    {
        var token = Guid.NewGuid().ToString();
        Debug.Log(token);
        Application.OpenURL(String.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&state={2}", _facebookAppId, _redirectEndpoint, "c8824f0e-54db-8119-b157-410ff5570d75"));
    }



    /*
     * 1. Get latest auth code from database
     */
    public IEnumerator parseCode()
    {
        WWW respJson = new WWW("http://joesbabysitting.com/getAccessToken.php");

        yield return respJson;

        Debug.Log(respJson.text);

        codeObj = AuthCode.CreateFromJson(respJson.text);
        access_code = codeObj.authCode;

        Debug.Log(access_code);

        StartCoroutine(retrieveAccessToken());
    }



    /*
     * Retrieve access token using auth code
     */
    public IEnumerator retrieveAccessToken()
    {

        Debug.Log(String.Format("https://graph.facebook.com/v2.8/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", _facebookAppId, _redirectEndpoint, _appSecret, access_code));
        WWW accessTokenJson = new WWW(String.Format("https://graph.facebook.com/v2.8/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", _facebookAppId, _redirectEndpoint, _appSecret, access_code));
        //yield return null;
        yield return accessTokenJson;

        accessTokenObj = AccessToken.CreateFromJson(accessTokenJson.text);
        access_token = accessTokenObj.access_token;

        Debug.Log(access_token);

        StartCoroutine(retrieveUserDetails());
    }




    /*
     * Retrieve user details using access token and also get the profile picture
     */
    public IEnumerator retrieveUserDetails()
    {
        WWW userDetailsJson = new WWW(String.Format("https://graph.facebook.com/v2.8/me?fields=first_name%2Clast_name%2Cgender%2Cage_range%2Cid&access_token={0}",access_token));

        yield return userDetailsJson;

        Debug.Log(userDetailsJson.text);
        
        userObj = UserObj.CreateFromJson(userDetailsJson.text);
        user_id = userObj.id;

        Debug.Log(user_id);

        StartCoroutine(retrieveUserPicture());
        
    }





    /* 
     * Retrieve user picture
     */
    public IEnumerator retrieveUserPicture()
    {
        WWW profilePicture = new WWW(String.Format("http://graph.facebook.com/{0}/picture?width=256&height=256", user_id));
        yield return profilePicture;

        Debug.Log(profilePicture.text);
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
        fbPicture.material.mainTexture = tex;

        //fbName.text = "Hello\n" + userObj.first_name + "\n" + userObj.last_name.ToString();


    }


}