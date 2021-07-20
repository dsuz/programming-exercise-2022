﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DontDestroyOnLoad を使って「シーンをまたいで値を渡す」方法
/// </summary>
public class DDOLPattern : MonoBehaviour
{
    /// <summary>メッセージを表示する Text オブジェクトの名前</summary>
    [SerializeField] string m_messageTextName = "MessageText";
    /// <summary>プレイヤーの名前。これをシーンまたぎで渡す</summary>
    [SerializeField] string m_name = "ああああ";

    void Start()
    {
        if (FindObjectsOfType<DDOLPattern>().Length > 1)
        {
            // 重複しないように、既にある時は自分自身を破棄する
            Destroy(this.gameObject);
        }
        else
        {
            // 自分しかいない時は、自分を DontDestroyOnLoad に登録する
            DontDestroyOnLoad(this.gameObject);
            OnLevelWasLoaded(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // 現在のシーン番号を渡しながら関数を呼ぶ
        }
    }

    /// <summary>
    /// 名前を保存する
    /// </summary>
    /// <param name="input"></param>
    public void SetName(InputField input)
    {
        m_name = input.text;
    }

    /// <summary>
    /// シーンがロードされた時に呼ばれる。
    /// この関数を使うと警告が出て、代わりに SceneManager.sceneLoaded を使うよう促される。
    /// しかし、これを使うにはデリゲートを知らなければならないので、今はこちらを使う。
    /// </summary>
    /// <param name="level"></param>
    void OnLevelWasLoaded(int level)
    {
        GameObject go = GameObject.Find(m_messageTextName);
        Text text = go?.GetComponent<Text>();

        if (text)
        {
            text.text = $"おお！<b><color=red>{m_name}</color></b> よ！しんでしまうとは なにごとだ！";
        }
    }
}
