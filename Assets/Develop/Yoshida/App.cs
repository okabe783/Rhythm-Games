using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public struct User
{
    public int ID;
    public string Name;
}

public struct Test
{
    public int ID;
    public string Hoge;
}

public class App : MonoBehaviour
{
    private string _baseUrl = "http://localhost:8080";

    private void Start()
    {
        StartCoroutine(Get("ping"));
    }

    public void GetReq(string order)
    {
        StartCoroutine(Get(order));
    }

    public void GetReq(int id)
    {
        StartCoroutine(Get(id));
    }

    public void PostReq(string data)
    {
        var spl = data.Split();
        var json = new User
        {
            ID = int.Parse(spl[0]),
            Name = spl[1]
        };

        var reqJson = JsonUtility.ToJson(json);
        StartCoroutine(Post(Encoding.UTF8.GetBytes(reqJson)));
    }
    
    private void Request(UnityWebRequest req)
    {
        switch (req.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log(req.error);
                break;
            default:
                // リクエスト送信が正常に行われた場合、レスポンスが返ってくる
                Debug.Log(req.downloadHandler.text);
                var response = JsonUtility.FromJson<User>(Encoding.UTF8.GetString(req.downloadHandler.data));
                Debug.Log($"{response.Name}");
                break;
        }

        // 状態を確認するログ
        Debug.Log($"StatusCode: {req.responseCode}");
    }

    /// <summary>
    /// DBから情報を入手するメソッド
    /// </summary>
    /// <param name="order">接続するリンクによって返ってくる値が変わる</param>
    /// <remarks>ping: 接続テスト、 getAll: 全情報</remarks>
    private IEnumerator Get(string order)
    {
        using (var req = UnityWebRequest.Get($"{_baseUrl}/{order}"))
        {
            yield return req.SendWebRequest();
            Request(req);
        }
    }

    private IEnumerator Get(int id)
    {
        using (var req = UnityWebRequest.Get($"{_baseUrl}/get/{id}"))
        {
            yield return req.SendWebRequest();
            Request(req);
        }
    }

    private IEnumerator Post(byte[] postData)
    {
        using (var req = new UnityWebRequest("http://localhost:8080/post", UnityWebRequest.kHttpVerbPOST))
        {
            req.uploadHandler = new UploadHandlerRaw(postData);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
            Debug.Log($"StatusCode: {req.responseCode}");
        }
    }
}