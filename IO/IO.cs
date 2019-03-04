using System;
using System.IO;
using System.Collections.Generic;
using AR_Lib.Geometry;
using AR_Lib.HalfEdgeMesh;
using Rhino;

namespace AR_Lib.IO
{
    #region To/From Files

    /// CSV format
    public static class CSVReader { }
    public static class CSVWritter { }

    /// OFF format
    public static class OFFReader 
    {
        public static OFFResult ReadMeshFromFile(string FilePath, out OFFMeshData data)
        {
            string[] lines = File.ReadAllLines(FilePath);
            data = new OFFMeshData();
            // Check if first line states OFF format
            if (lines[0] != "OFF") return OFFResult.Incorrect_Format;

            // Get second line and extract number of vertices and faces
            string[] initialData = lines[1].Split(' ');
            int nVertex = 0;
            int nFaces = 0;
            if (!Int32.TryParse(initialData[0], out nVertex)) return OFFResult.Incorrect_Format;
            if (!Int32.TryParse(initialData[1], out nFaces)) return OFFResult.Incorrect_Format;

            // Check if length of lines correct
            if (nVertex + nFaces + 2 != lines.Length) return OFFResult.Incorrect_Format;

            // Iterate through all the lines containing the mesh data
            int start = 2;
            List<Point3d> vertices = new List<Point3d>();
            List<List<int>> faces = new List<List<int>>();

            for (int i = start; i < lines.Length; i++)
            {
                if (i < (nVertex + start))
                { // Extract vertices
                    
                    List<double> coords = new List<double>();
                    string[] pointStrings = lines[i].Split(' ');
                    // Iterate over the string fragments and convert them to numbers
                    foreach(string ptStr in pointStrings)
                    {
                        double ptCoord;
                        if (!Double.TryParse(pointStrings[0], out ptCoord)) return OFFResult.Incorrect_Vertex;
                        coords.Add(ptCoord);
                    }
                    vertices.Add(new Point3d(coords[0], coords[1], coords[2]));
                }
                else if (i < (nVertex + nFaces + start))
                { // Extract faces
                    
                    // In OFF, faces come with a first number determining the number of vertices in that face
                    List<int> vertexIndexes = new List<int>();

                    string[] faceStrings = lines[i].Split(' ');
                    // Get first int that represents vertex count of face
                    int vertexCount;
                    if (!Int32.TryParse(faceStrings[0], out vertexCount)) return OFFResult.Incorrect_Face;

                    for (int f = 1; f < faceStrings.Length; f++)
                    {
                        int vertIndex;
                        if (!Int32.TryParse(faceStrings[f], out vertIndex)) return OFFResult.Incorrect_Face;
                        vertexIndexes.Add(vertIndex);
                    }
                    faces.Add(vertexIndexes);
                }
            }

            // Set data output
            data.vertices = vertices;
            data.faces = faces;

            return OFFResult.OK;
        }
    }

    public static class OFFWritter { }


    public enum OFFResult
    {
        OK,
        Incorrect_Format,
        Incorrect_Vertex,
        Incorrect_Face,
        Non_Matching_Vertices_Size,
        Non_Matching_Faces_Size,
        File_Not_Found
    }
    public struct OFFMeshData
    {
        public List<Point3d> vertices;
        public List<List<int>> faces;
    }

    public struct OBJMeshData
    {
        public List<Point3d> vertices;
        public List<List<int>> faces;
        public List<List<int>> edges;
        public List<List<double>> textureCoords;
        public List<List<int>> faceTextureCoords;
        public List<Vector3d> normals;
    }

    public static class OBJReader 
    { 
        
    }

    public static class OBJWritter 
    {
        
    }
    
    #endregion

    #region To/From Applications

    public static class RhinoIO
    {
        public static RhinoMeshResult ToRhinoMesh(HE_Mesh mesh)
        {
            Rhino.Geometry.Mesh tmpMesh = new Rhino.Geometry.Mesh();

            return RhinoMeshResult.OK;

        }

        public static HE_Mesh FromRhinoMesh(Rhino.Geometry.Mesh rhinoMesh){ throw new NotImplementedException(); }

        public enum RhinoMeshResult
        {
            OK,
            Empty,
            Invalid
        }
    }

    public static class DynamoIO
    {
        public static object ToDynamoMesh(HE_Mesh mesh){ throw new NotImplementedException(); }

        public static HE_Mesh FromRhinoMesh(object dynamoMesh){ throw new NotImplementedException(); }

    }


    #endregion
}