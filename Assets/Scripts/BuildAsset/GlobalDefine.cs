using UnityEngine;
using System.Collections;

/// <summary>
/// 资源类型;
/// </summary>
public enum AssetType
{
    Non,
    Prefab,
    Scene,
    Material,
    Scripts,
    Font,
    /// <summary>
    /// 不需要实例化的的Prefab资源,如图集;
    /// </summary>
    AssetPrefab,
    Shader,
    Texture,
    Audio,
    AnimeCtrl,
    AnimeClip
}
