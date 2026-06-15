using System;
using System.Linq;
using UnityEngine;

namespace FairyGUI.Dynamic
{
    public partial class UIAssetManager
    {
        private void DestroyTexture(Texture texture)
        {
            if (texture == null)
                return;

            if (m_AssetLoader == null)
                throw new Exception("请设置AssetLoader");

            //m_LoadedTextures.Remove(texture);
            //m_AssetLoader.UnloadTexture(texture);
            string key = "";
            foreach (var pair in m_LoadedTextures)
            {
                if (pair.Value.Contains(texture))
                {
                    key = pair.Key;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(key))
            {
                m_LoadedTextures.Remove(key);
                m_AssetLoader.UnloadTexture(key, texture);
            }
        }

        private void DestroyAudioClip(AudioClip audioClip)
        {
            if (audioClip == null)
                return;

            if (m_AssetLoader == null)
                throw new Exception("请设置AssetLoader");
            string key = "";
            foreach(var pair in m_LoadedAudioClips)
            {
                if(pair.Value.Contains(audioClip))
                {
                    key = pair.Key;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(key))
            {
                m_LoadedAudioClips.Remove(key);
                m_AssetLoader.UnloadAudioClip(key,audioClip);
            }
        }
    }
}