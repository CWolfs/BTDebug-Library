using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using System.Reflection;

namespace BTDebug.Utils {
	public class ReflectionUtils {
    public static Type GetAssemblyType(string typeName)	{
      var type = Type.GetType(typeName);
      if (type != null) return type;
      foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
        type = a.GetType($"{a.GetName().Name}.{typeName}");
        if (type != null)
          return type;
      }
      return null;
	  }
  } 
}