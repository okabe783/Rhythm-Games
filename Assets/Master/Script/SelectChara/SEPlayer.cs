using System.Collections;
using UnityEngine;

/// <summary>
/// ボタン押下でSE再生
/// </summary>
public class SEPlayer : MonoBehaviour
{
    [SerializeField, Header("SEの名前")] private string _seName = default;

    public void OnClick()
    {
        if (string.IsNullOrEmpty(_seName))
            Debug.LogWarning($"objName: {gameObject.name} // SEの名前が空欄です。");
        else
            StartCoroutine(WaitForSe());
        // CriSoundManager.Instance.PlaySE(_seName);
    }

    private IEnumerator WaitForSe()
    {
        yield return new WaitForSeconds(4f);
        CriSoundManager.Instance.PlaySE(_seName);
    }
}