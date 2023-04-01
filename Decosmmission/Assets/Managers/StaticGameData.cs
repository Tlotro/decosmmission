using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StaticGameData
{
    public static StaticGameData GlobalInstance = new();
    public Dictionary<string, (Type, object)> Values = new Dictionary<string, (Type, object)>(); 
    public object this[string name] { get { return Values.ContainsKey(name) ? Values[name].Item2 : null; } set { Values[name] = (value.GetType(), value); } }
    public Type GetTypeOf(string name) { return Values.ContainsKey(name) ? Values[name].Item1 : null; }
    public void Delete(string name) { Values.Remove(name); }
}
