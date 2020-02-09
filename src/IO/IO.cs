using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

using AR_Lib.Geometry;
using AR_Lib.HalfEdgeMesh;

namespace AR_Lib.IO
{
    #region To/From Files

    /// CSV format
    public static class CSVReader { }
    public static class CSVWritter { }

    /// OFF format

    /// <summary>OFF Reader class</summary>
    public static class OFFReader
    {
        public static OFFResult ReadMeshFromFile(string FilePath, out OFFMeshData data)
        {
            string[] lines = File.ReadAllLines(FilePath);
            data = new OFFMeshData();
            // Check if first line states OFF format
            if (lines[0] != "OFF")
                return OFFResult.Incorrect_Format;

            // Get second line and extract number of vertices and faces
            string[] initialData = lines[1].Split(' ');
            if (!int.TryParse(initialData[0], out var nVertex))
                return OFFResult.Incorrect_Format;
            if (!int.TryParse(initialData[1], out var nFaces))
                return OFFResult.Incorrect_Format;

            // Check if length of lines correct
            if (nVertex + nFaces + 2 != lines.Length)
                return OFFResult.Incorrect_Format;

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
                    foreach (string ptStr in pointStrings)
                    {
                        if (!double.TryParse(ptStr, out var ptCoord))
                            return OFFResult.Incorrect_Vertex;
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
                    if (!int.TryParse(faceStrings[0], out var vertexCount))
                        return OFFResult.Incorrect_Face;

                    for (int f = 1; f < faceStrings.Length; f++)
                    {
                        if (!int.TryParse(faceStrings[f], out var vertIndex))
                            return OFFResult.Incorrect_Face;
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


    /// <summary>Writter class</summary>
    public static class OFFWritter
    {
        /// <summary>
        /// Write a Half-Edge mesh to a .OFF file
        /// </summary>
        /// <param name="mesh">Half-edge mesh to export</param>
        /// <param name="filePath">Path to save the file to</param>
        /// <returns></returns>
        public static OFFResult WriteMeshToFile(Mesh mesh, string filePath)
        {
            string[] offLines = new string[mesh.Vertices.Count + mesh.Faces.Count + 2];

            string offHead = "OFF";
            offLines[0] = offHead;
            string offCount = mesh.Vertices.Count + " " + mesh.Faces.Count + " 0";
            offLines[1] = offCount;

            int count = 2;
            foreach (MeshVertex vertex in mesh.Vertices)
            {
                string vText = vertex.X + " " + vertex.Y + " " + vertex.Z;
                offLines[count] = vText;
                count++;

            }
            foreach (MeshFace face in mesh.Faces)
            {
                if (!face.IsBoundaryLoop())
                {
                    List<MeshVertex> vertices = face.AdjacentVertices();
                    string faceString = vertices.Count.ToString();

                    foreach (MeshVertex v in face.AdjacentVertices())
                    {
                        faceString = faceString + " " + v.Index;
                    }
                    offLines[count] = faceString;
                    count++;
                }
            }

            System.IO.File.WriteAllLines(filePath, offLines);
            return OFFResult.OK;
        }
    }
    /// <summary>
    /// Enum containing the result of the OFF conversion
    /// </summary>
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

    /// <summary>
    /// Struct containing the resulting mesh data extracted from an .OFF file
    /// </summary>
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

    // public static class RhinoIO
    // {
    //     public static RhinoMeshResult ToRhinoMesh(HE_Mesh mesh, out Rhino.Geometry.Mesh rhinoMesh)
    //     {
    //         //CHECKS FOR NGON MESHES!
    //         if(mesh.isNgonMesh())
    //         {
    //             rhinoMesh = null;
    //             return RhinoMeshResult.Invalid;
    //         }

    //         rhinoMesh = new Rhino.Geometry.Mesh();

    //         foreach(HE_Vertex vertex in mesh.Vertices)
    //         {
    //             rhinoMesh.Vertices.Add(vertex.X,vertex.Y,vertex.Z);
    //         }

    //         foreach(HE_Face face in mesh.Faces)
    //         {
    //             List<HE_Vertex> faceVertices = face.adjacentVertices();
    //             if(faceVertices.Count == 3) rhinoMesh.Faces.AddFace(new Rhino.Geometry.MeshFace(faceVertices[0].Index,faceVertices[1].Index,faceVertices[2].Index));
    //             if(faceVertices.Count == 4) rhinoMesh.Faces.AddFace(new Rhino.Geometry.MeshFace(faceVertices[0].Index,faceVertices[1].Index,faceVertices[2].Index,faceVertices[3].Index));
    //         }

    //         return RhinoMeshResult.OK;

    //     }

    //     public static RhinoMeshResult FromRhinoMesh(Rhino.Geometry.Mesh rhinoMesh, out HE_Mesh mesh)
    //     {
    //         List<Point3d> vertices = new List<Point3d>();
    //         List<List<int>> faceVertexIndexes = new List<List<int>>();

    //         foreach(Rhino.Geometry.Point3d vertex in rhinoMesh.Vertices)
    //         {
    //             vertices.Add(new Point3d(vertex.X,vertex.Y,vertex.Z));
    //         }
    //         foreach(Rhino.Geometry.MeshNgon face in rhinoMesh.GetNgonAndFacesEnumerable())
    //         {

    //             List<int> list = new List<int>();

    //             foreach(int i in face.BoundaryVertexIndexList()) list.Add(i);

    //             faceVertexIndexes.Add(list);

    //         }

    //         mesh = new HE_Mesh(vertices,faceVertexIndexes);
    //         return RhinoMeshResult.OK;
    //     }

    //     public enum RhinoMeshResult
    //     {
    //         OK,
    //         Empty,
    //         Invalid
    //     }
    // }

    // public static class DynamoIO
    // {
    //     public static object ToDynamoMesh(HE_Mesh mesh) { throw new NotImplementedException(); }

    //     public static HE_Mesh FromRhinoMesh(object dynamoMesh) { throw new NotImplementedException(); }

    // }

    #endregion
}