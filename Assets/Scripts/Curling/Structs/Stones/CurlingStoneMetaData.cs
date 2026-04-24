// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using System;
using UnityEngine;
using Animancer;

namespace CurlingStones
{

    [Serializable]
    public class CurlingStoneMetaData
    {
        [Header("Basic Data")]
        [SerializeField]
        private string _Title = "Basic Stone";
        public string Title => _Title;

        [SerializeField]
        private string _Description = "Just your basic curling stone.";
        public string Description => _Description;

        [SerializeField]
        private string _Id = "";
        public string Id => _Id;

        [SerializeField]
        private Texture _AvatarImage = null;
        public Texture AvatarImage => _AvatarImage;

        // public string title = "Basic Stone";
        // public string description = "Just your basic curling stone.";
        // public string id = "";
        // public Texture avatarImage;
        // private RawImage faceRenderTexture;

        [Header("Ids")]
        [HideInInspector] public int teamId_i;
        [HideInInspector] public string teamId;
        [HideInInspector] public int stoneIndex;

        /************************************************************************************************************************/
    }
}
