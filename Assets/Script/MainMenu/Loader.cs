using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shmup
{
  public static partial class Loader
  {
    static SceneReference targetScene;
    public static void Load(SceneReference scene)
    {
      targetScene = scene;
      Timing.RunCoroutine(LoadTargetScene());
    }

    static IEnumerator<float> LoadTargetScene()
    {
      yield return Timing.WaitForOneFrame;
      SceneManager.LoadScene(targetScene.Name);
    }

    public static void Restart()
    {
      SceneManager.LoadScene("Main Menu");
      GameObject.Destroy(GameController.instance.gameObject);
      // GameObject.Destroy(TimingController.);

    }
  }
}