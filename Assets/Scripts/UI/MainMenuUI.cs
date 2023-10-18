using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
   
   [SerializeField] private UnityEngine.UI.Button playButton;
   [SerializeField] private UnityEngine.UI.Button quitButton;

   private void Awake()
   {
      playButton.onClick.AddListener(() =>
      {
         Loader.Load(Loader.Scene.MainScene);
      });
      quitButton.onClick.AddListener(() =>
      {
         Application.Quit();
      });
   }
}
