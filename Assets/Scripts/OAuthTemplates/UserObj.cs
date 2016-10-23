using UnityEngine;

public class UserObj
{

    public string first_name;
    public string last_name;
    public string gender;
    public string id;

    public static UserObj CreateFromJson(string jsonStr)
    {
        return JsonUtility.FromJson<UserObj>(jsonStr);
    }

}
