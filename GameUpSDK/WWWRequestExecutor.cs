// Copyright 2015 GameUp.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GameUp
{
  public class WWWRequestExecutor : SingletonMonoBehaviour<WWWRequestExecutor>
  {
    private static string UNITY_VERSION = Application.unityVersion;
    private static string BUILD_VERSION = Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();
    private static string OPERATING_SYSTEM = SystemInfo.operatingSystem;
    private static string USER_AGENT =
      String.Format ("gameup-unity-sdk/{0} (Unity {1}; {2})", BUILD_VERSION, UNITY_VERSION, OPERATING_SYSTEM);

    internal static void Execute (WWWRequest req) {
      WWWRequestExecutor.Instance.InitExecute (req);
    }

    private void InitExecute (WWWRequest req) {
      StartCoroutine (ExecuteRequest(req));
    }

    private IEnumerator ExecuteRequest (WWWRequest req)
    {
      // Server hack for Unity's broken WWW module
      string query = "_status=200";
      if (req.Method.Equals ("PUT") || req.Method.Equals ("POST") || req.Method.Equals ("DELETE")) {
        query = query + "&_method=" + req.Method;
      }

      UriBuilder b = new UriBuilder (req.Uri);
      b.Query = query;

      // Add necessary request headers
      req.AddHeader ("User-Agent", USER_AGENT);
      req.AddHeader ("Accept", "application/json");
      req.AddHeader ("Content-Type", "application/json");

      req.AddHeader ("Authorization", req.AuthHeader);

      WWW www = new WWW (b.Uri.ToString (), req.Body, req.GetHeaders());

      yield return www;

      if (!String.IsNullOrEmpty (www.error)) {
        if (req.OnFailure != null) {
          req.OnFailure (500, www.error);
        }
      } else {
        if (www.text == null || www.text.Length == 0) {
          if (req.OnSuccess != null) {
            req.OnSuccess ("");
          }
        } else {
          Dictionary<string, object> json = SimpleJson.DeserializeObject<Dictionary<string, object>> (www.text);
          // HACK: make sure that the error is checking for GameUp error message combinations
          if (json.ContainsKey ("status") && json.ContainsKey ("message") && json.ContainsKey ("request")) {
            int statusCode = int.Parse (System.Convert.ToString (json ["status"]));
            if (req.OnFailure != null) {
              req.OnFailure (statusCode, System.Convert.ToString (json ["message"]));
            }
          } else {
            if (req.OnSuccess != null) {
              req.OnSuccess (www.text);
            }
          }
        }
      }
    }
  }
}

