using UnityEngine;

public class AccessToken
{

    public string access_token;

    public static AccessToken CreateFromJson(string jsonStr)
    {
        return JsonUtility.FromJson<AccessToken>(jsonStr);
    }

}
