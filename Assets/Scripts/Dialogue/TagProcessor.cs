using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Windows;

public class TagProcessor : MonoBehaviour
{
    public static string ProcessTags(string input) {
        // Define the pattern for matching any tag with a name and value inside <>
        string pattern = @"<(?<tagName>[^=]+)=""(?<tagValue>[^""]+)"">";

        // Use Regex to find matches
        MatchCollection matches = Regex.Matches(input, pattern);

        if (matches.Count > 0) {
            foreach (Match match in matches) {
                // Extract tag name and value using named groups
                string tagName = match.Groups["tagName"].Value;
                string tagValue = match.Groups["tagValue"].Value;

                if (tagName.ToLower() == "audio") {
                    HanleAudio(tagValue);
                }
                else if (tagName.ToLower() == "camera") {
                    HandleCamera(tagValue);
                }
                Debug.Log($"Tag found: {tagName}=\"{tagValue}\"");

                // Remove the tag from the string
                input = input.Replace(match.Value, "");
            }
        }

        return input;
    }

    public static void ProcessInkTag(string input) {
        var splitLine = input.Split(':');
        if (splitLine.Length > 1) {
            var tagName = splitLine[0];
            var tagValue = splitLine[1];

            if (tagName.ToLower() == "camera") {
                HandleCamera(tagValue);
            }
            else if (tagName.ToLower() == "audio") {
                HanleAudio(tagValue);
            }
            else if (tagName.ToLower() == "name") {
                HandleName(tagValue);
            }
            else if (tagName.ToLower() == "timeline") {
                HandleTimeline(tagValue);
            }
            else if (tagName.ToLower() == "timeline-wait") {
                HandleTimeline(tagValue, true);
            }
            else if (tagName.ToLower() == "anim" || tagName.ToLower() == "animation") {
                HandleAnimation(tagValue);
            }
            else if (tagName.ToLower() == "lock") {
                HandleLock(tagValue);
            }
            else if (tagName.ToLower() == "lock-no-auto") {
                HandleLock(tagValue, false);
            }
            else if (tagName.ToLower() == "hide") {
                HandleHide(tagValue);
            }
            else if (tagName.ToLower() == "wait") {
                Debug.Log("WAIT HERE FOR " + tagValue.ToString());
            }
            else if (tagName.ToLower() == "animatedevent") {
                FindObjectOfType<Car6>().SendMessage("animatedEvent", tagValue.ToString());
            }
            else {
                Debug.Log("No tag found for " + tagName + " [" + tagValue + "]");
            }
        }
        else {
            if (input.ToLower() == "hide") {
                HandleHide(input);
            } else {
                Debug.Log("No tag found for " + input);
            }
        }
    }

    public static void HandleCamera(string tagValue) {
        //CamManager.SetVisibleCamera(tagValue);
    }

    public static void HanleAudio(string tagValue) {
        var clip = Resources.Load(tagValue) as AudioClip;
        if (clip != null) {
            //AudioSource.PlayClipAtPoint(clip, Helpers.Camera.transform.position, 1f);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
        }
        else {
            Debug.Log("Cant find clip " + tagValue);
        }
    }

    public static void HandleName(string tagValue) {
        DialogueUI.Instance.DisplayName(tagValue);
    }

    public static void HandleTimeline(string tagValue, bool shouldWait = false) {
        var timelineGameobject = GameObject.Find(tagValue);
        if (timelineGameobject == null) { Debug.Log("No timeline gameobject with name " + tagValue); return; }

        var director = timelineGameobject.GetComponent<PlayableDirector>();
        if (director == null) { Debug.Log("timeline gameobject doesn't have a playable director [" + tagValue + "]"); return; }
        director.initialTime = 0f;
        director.Play();

        if (shouldWait) {
            HandleLock("wait/" + director.duration.ToString());
        }
    }

    public static void HandleAnimation(string tagValue) {
        var splitLine = tagValue.Split('/');
        if (splitLine.Length > 1) {
            var gameobjectName = splitLine[0];
            var animationName = splitLine[1];

            var animationGameobject = GameObject.Find(gameobjectName);
            if (animationGameobject == null) { Debug.Log("No animation gameobject with name " + tagValue); return; }

            var animatingScript = animationGameobject.GetComponent<AnimateObject>();
            animatingScript.PlayAnimaiton(animationName);
        }
        else
            Debug.Log("Improper animation tag " + tagValue);
    }

    public static void HandleLock(string tagValue, bool auto = true) {
        var splitLine = tagValue.Split('/');
        if (splitLine.Length > 1) {
            var lockType = splitLine[0];
            var lockValue = splitLine[1];

            if(lockType == "wait") {
                float lockTime = float.Parse(lockValue);

                if(auto) 
                    FindObjectOfType<InkDialogueManager>().LockAndNext(lockTime);
                else
                    FindObjectOfType<InkDialogueManager>().LockAndWait(lockTime);
            }
        }
        else
            Debug.Log("Improper lock tag " + tagValue);
    }

    public static void HandleHide(string tagValue) {
        FindObjectOfType<DialogueUI>().SetTextVisibility(false);
    }
}
