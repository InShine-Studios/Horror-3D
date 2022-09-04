using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    /**
     * Utils class for managing I/O and filesystem related things
     */
    public class FileSystemHelper
    {
        #region Const
        public static readonly string RootDirName = Path.GetFullPath(Path.Combine(Application.dataPath, "Astralization"));
        private static readonly string AbsoluteProjectPath = Application.dataPath;
        public static readonly string RelativeProjectPath = "Assets" + RootDirName.Substring(Application.dataPath.Length);
        #endregion

        #region Directory
        private static string ConvertToRelativePath(string absolutePath)
        {
            return "Assets" + absolutePath.Substring(Application.dataPath.Length);
        }

        private static string CombinePath(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        private static string CombinePath(string path1, string path2, string path3)
        {
            return Path.Combine(path1, path2, path3).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        public static string CombinePath(string[] path)
        {
            return Path.Combine(path).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        public static void CreateDirectory(string dirPath)
        {
            Directory.CreateDirectory(dirPath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        }

        public static void CreateDirectories(string[] pathSequence)
        {
            Directory.CreateDirectory(CombinePath(RelativeProjectPath, CombinePath(pathSequence)));
        }

        public static bool IsDirectoryExist(string dirPath)
        {
            return Directory.Exists(CombinePath(RelativeProjectPath, dirPath));
        }
        #endregion

        #region Writer
        public static void CreateAsset(Object @object, string pathToDirectory, string filename)
        {
            AssetDatabase.CreateAsset(@object, CombinePath(RelativeProjectPath,pathToDirectory,filename));
        }

        public static void CreateAsset(Object @object, string[] pathToDirectory, string filename)
        {
            AssetDatabase.CreateAsset(@object, CombinePath(RelativeProjectPath, CombinePath(pathToDirectory), filename));
        }
        #endregion
    }
}
