using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using System.Reflection;

namespace BTDebug.Utils {
	public class ReflectionUtils {
    public static Type GetTypeByName(string className) {
      List<Type> returnVal = new List<Type>();

      foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) {
          Type[] assemblyTypes = a.GetTypes();
          for (int j = 0; j < assemblyTypes.Length; j++) {
              if (assemblyTypes[j].Name == className) {
                  returnVal.Add(assemblyTypes[j]);
              }
          }
      }

      foreach (Type t in returnVal) {
      if (t.IsSubclassOf(typeof(MonoBehaviour)) || t.IsSubclassOf(typeof(Component))) return t;
      }

      return null;
    }
  }
}