using UnityEngine;

public class AuthCode{

    public string authCode;

    public static AuthCode CreateFromJson(string jsonStr)
    {
        return JsonUtility.FromJson<AuthCode>(jsonStr);
    }
	
}
