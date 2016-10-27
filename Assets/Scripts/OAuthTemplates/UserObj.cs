using UnityEngine;

public class UserObj
{

    public string first_name;
    public string last_name;
    public string id;
    public string picture_url;

    public static UserObj CreateFromJson(string jsonStr)
    {
        return JsonUtility.FromJson<UserObj>(jsonStr);
    }

}
