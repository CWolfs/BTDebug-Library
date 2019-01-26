using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTDebug.Utils {
  public static class GameObjectExtensions {
    public static List<GameObject> FindAllContains(this GameObject go, string name) {
      List<GameObject> gameObjects = new List<GameObject>();

      foreach (Transform t in go.transform) {
        if (t.name.Contains(name)) {
          gameObjects.Add(t.gameObject);
        }
      }

      return gameObjects;
    }

    public static List<GameObject> FindAllContainsRecursive(this GameObject go, params string[] names) {
      List<GameObject> gameObjects = new List<GameObject>();

      foreach (Transform t in go.transform) {
        foreach (string checkName in names) {
          if (t.name.Contains(checkName)) {
            gameObjects.Add(t.gameObject);
          }
        }

        gameObjects.AddRange(t.gameObject.FindAllContainsRecursive(names));
      }

      return gameObjects;
    }

    public static List<GameObject> FindAllContainsRecursiveIgnoreCase(this GameObject go, params string[] names) {
      List<GameObject> gameObjects = new List<GameObject>();

      foreach (Transform t in go.transform) {
        foreach (string checkName in names) {
          string check = checkName.ToLower();
          string name = t.name.ToLower();
          if (name.Contains(check)) {
            gameObjects.Add(t.gameObject);
          }
        }

        gameObjects.AddRange(t.gameObject.FindAllContainsRecursiveIgnoreCase(names));
      }

      return gameObjects;
    }

    public static string GetGameObjectPath(this GameObject go) {
      Transform transform = go.transform;
      string path = transform.name;
      while (transform.parent != null) {
          transform = transform.parent;
          path = transform.name + "/" + path;
      }
      return path;
    }
  }
}