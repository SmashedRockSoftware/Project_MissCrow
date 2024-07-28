//using UnityEngine;
//using System.IO;

//public class BadComboReader : MonoBehaviour {
//    void Start() {

//        //Load a text file (Assets/Resources/Text/textFile01.txt)
//        var textFile = Resources.Load<TextAsset>("Text/textFile01");

//        // Specify the path to your text file
//        string filePath = "Path/To/Your/File.txt";

//        // Read all lines from the text file
//        string[] lines = File.ReadAllLines(filePath);

//        // Split each line into its own string array
//        foreach (string line in lines) {
//            string[] parts = line.Split(new char[] { ':' }, 2);
//            string key = parts[0].Trim();
//            string description = parts[1].Trim();

//            Debug.Log($"Key: {key}, Description: {description}");
//        }
//    }
//}

using UnityEngine;
using System.IO;

public class BadComboReader : MonoBehaviour {
    void Start() {        //Load a text file (Assets/Resources/Text/textFile01.txt)
        //var textFile = Resources.Load<TextAsset>("badcombos");
        //string[] lines = textFile.text.Split('\n');

        //for (int i = 0; i < lines.Length; i++) {
        //    var splitComboDialog = lines[i].Split(":");
        //    var splitCombo = splitComboDialog[0].Split("+");

        //    Debug.Log("[" + splitCombo[0] + " " + splitCombo[1] + "]" + splitComboDialog[1]);
        //}

    }
}