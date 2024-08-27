using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CreateBundle
{
    [MenuItem("Assets/Create Bundle")]
    private static void BuildBundles()
    {
        string path = "C:/Users/justi/Desktop/Cookie Land/Bundles";
        string hashFilePath = Path.Combine(path, "hash.txt");
        string path2 = "C:/Users/justi/Desktop/Cookie Land/Bundles/Server";
        string hashFilePath2 = Path.Combine(path2, "hash.txt");

        try
        {
            // Build the asset bundles
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.WebGL);

            // Initialize a list to store hash strings
            var hashList = new System.Collections.Generic.List<string>();

            // Get all .manifest files in the directory
            string[] manifestFiles = Directory.GetFiles(path, "*.manifest");

            foreach (string manifestFile in manifestFiles)
            {
                // Skip the main manifest file (which is named after the folder)
                if (Path.GetFileName(manifestFile) == Path.GetFileName(path) + ".manifest")
                {
                    continue;
                }

                // Read the manifest file line by line
                string[] lines = File.ReadAllLines(manifestFile);

                for (int i = 0; i < lines.Length; i++)
                {
                    // Look for the line that contains "AssetFileHash"
                    if (lines[i].TrimStart().StartsWith("AssetFileHash:"))
                    {
                        // The hash is 2 lines below "AssetFileHash:"
                        if (i + 2 < lines.Length && lines[i + 2].TrimStart().StartsWith("Hash:"))
                        {
                            string hashLine = lines[i + 2].Trim();
                            string hashValue = hashLine.Substring("Hash:".Length).Trim();
                            hashList.Add(hashValue);
                        }
                    }
                }
            }

            // Write all hashes to the text file
            File.WriteAllLines(hashFilePath, hashList);

            // Build the asset bundles
            BuildPipeline.BuildAssetBundles(path2, BuildAssetBundleOptions.None, BuildTarget.StandaloneLinux64);

            // Initialize a list to store hash strings
            var hashList2 = new System.Collections.Generic.List<string>();

            // Get all .manifest files in the directory
            string[] manifestFiles2 = Directory.GetFiles(path2, "*.manifest");

            foreach (string manifestFile in manifestFiles2)
            {
                // Skip the main manifest file (which is named after the folder)
                if (Path.GetFileName(manifestFile) == Path.GetFileName(path2) + ".manifest")
                {
                    continue;
                }

                // Read the manifest file line by line
                string[] lines = File.ReadAllLines(manifestFile);

                for (int i = 0; i < lines.Length; i++)
                {
                    // Look for the line that contains "AssetFileHash"
                    if (lines[i].TrimStart().StartsWith("AssetFileHash:"))
                    {
                        // The hash is 2 lines below "AssetFileHash:"
                        if (i + 2 < lines.Length && lines[i + 2].TrimStart().StartsWith("Hash:"))
                        {
                            string hashLine = lines[i + 2].Trim();
                            string hashValue = hashLine.Substring("Hash:".Length).Trim();
                            hashList2.Add(hashValue);
                        }
                    }
                }
            }

            // Write all hashes to the text file
            File.WriteAllLines(hashFilePath2, hashList2);

            Debug.Log("Bundle Built");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
