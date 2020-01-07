using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Image))]

    
    public class PlugginableImage : PluggableUIElement, IPlugginableUI
    {
        [PluggableAssetSelector(AssetTypeKey = "Color")]
        public string colorId;
        [PluggableAssetSelector(AssetTypeKey = "Sprite")]
        public string spriteId;

      
        public Image image;

        public Image Image { get => image != null ? image : GetComponent<Image>(); }

        public override  void Awake()
        {
            // themeData = ThemeData;
            base.Awake();
          
        }
        public override void Update()
        {
            // themeData = ThemeData;
            base.Update();

        }

        public void SetImageColor()
        {
          Image.color=  ThemeData.GetColor(colorId);

        }

        public void SetImageSprite()
        {
            Image.sprite = ThemeData.GetSprite(spriteId);

        }

        protected override void OnThemeDraw()
        {

            base.OnThemeDraw();

            SetImageColor();
            SetImageSprite();
        }



    }
}