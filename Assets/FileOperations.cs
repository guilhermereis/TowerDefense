using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FileOperations : MonoBehaviour
{
    private static StreamWriter sw;
    private static StreamReader sr;
    private const string FILE_NAME = "MyFile.txt";
    // Use this for initialization
    public static void writeToFile(string text)
    {
        //' Create an instance of StreamWriter to write text to a file.
        //StreamWriter sw = new StreamWriter(FILE_NAME);
        //' Add some text to the file.
        //sw.Write(text);
        //sw.Close();

    }
    public static void openStreamWriter()
    {
        //sw = new StreamWriter(FILE_NAME);
    }
    public static StreamWriter getStreamWriterInstance()
    {
        return sw;
    }
    public static void writeLineToFile(string text)
    {
        //' Create an instance of StreamWriter to write text to a file.
        //StreamWriter sw = getStreamWriterInstance();
        //' Add some text to the file.
       // sw.WriteLine(text);
        

    }
    public static void closeStreamWriter()
    {
        //getStreamWriterInstance().Close();
    }
    public static void writeToFile2()
    {
        // Create an instance of StreamWriter to write text to a file.
        // The using statement also closes the StreamWriter.
        //using (StreamWriter sw = new StreamWriter(FILE_NAME))
        //{
        //    // Add some text to the file.
        //    sw.Write("This is the ");
        //    sw.WriteLine("header for the file.");
        //    sw.WriteLine("-------------------");
        //    // Arbitrary objects can also be written to the file.
        //    sw.Write("The date is: ");
        //    sw.WriteLine(DateTime.Now);
        //}
        //Debug.Log("Wrote textfile");
    }

    public static void createFile()
    {
        //Debug.Log("GONNA WRITE !!!");
        //if (File.Exists(FILE_NAME))
        //{
        //    //Console.WriteLine("{0} already exists.", FILE_NAME);
        //    Debug.Log("returning...");
        //    return;
        //}
        //Debug.Log("IT DIDN'T RETURN !!!");
        //StreamWriter sr = File.CreateText(FILE_NAME);
        //sr.WriteLine("This is my file.");
        //sr.WriteLine("I can write ints {0} or floats {1}, and so on.",
        //    1, 4.2);
        //sr.Close();
        //Debug.Log("WROTE IT !!!");
    }
    public static void readTextFromFile()
    {
        //try
        //{   // Open the text file using a stream reader.
        //    using (StreamReader sr = new StreamReader(FILE_NAME))
        //    {
        //        // Read the stream to a string, and write the string to the console.
        //        string line = sr.ReadToEnd();
        //        Debug.Log(line);
        //    }
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError("The file could not be read:");
        //    Debug.LogError(e.Message);
        //}
    }


    // Update is called once per frame
    void Start()
    {
        
                     

        

    }

    //public string structureName;
    //public Transform transform;
    //public Vector3 position;
    //public Quaternion rotation;
    //public float currentHealth;
    //public int attackPowerLVL;
    //public int fireRateLVL;
    public static void writeState(PropertyScript.StructureState state)
    {
        //writeLineToFile(state.structureName);
        //writeLineToFile(state.position.ToString());
        //writeLineToFile(state.attackPowerLVL.ToString());
        //writeLineToFile(state.fireRateLVL.ToString());

    }
    public static List<PropertyScript.StructureState> loadState()
    {
        List<PropertyScript.StructureState> list = new List<PropertyScript.StructureState>();
        //PropertyScript.StructureState newState = new PropertyScript.StructureState();
        //try
        //{   // Open the text file using a stream reader.
        //    using (sr = new StreamReader(FILE_NAME))
        //    {
        //        // Read the stream to a string, and write the string to the console.
        //        string text = "";
        //        int currentLine = 0;
        //        do
        //        {
        //            if (currentLine == 0)
        //            {
        //                newState = new PropertyScript.StructureState();
        //            }
        //            currentLine++;
                    
        //            if (currentLine == 1)
        //            {
        //                text = sr.ReadLine();
        //                newState.structureName = text;
        //            }
        //            else if (currentLine == 2)
        //            {
        //                text = sr.ReadLine();
        //                newState.position = StringToVector3(text);
        //            }
        //            else if (currentLine == 3)
        //            {
        //                text = sr.ReadLine();
        //                newState.attackPowerLVL = Convert.ToInt32(text);
        //            }
        //            else if (currentLine == 4)
        //            {
        //                text = sr.ReadLine();
        //                newState.fireRateLVL = Convert.ToInt32(text);
        //                list.Add(newState);
        //                //
        //                currentLine = 0;
        //            }


        //        } while (text != null);
                
        //    }
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError("The file could not be read:");
        //    Debug.LogError(e.Message);
        //}
        return list;

    }
    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    public static void closeReader()
    {
      //  sr.Close();
    }
}
