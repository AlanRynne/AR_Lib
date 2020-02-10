using System.Collections.Generic;
using System.IO;
using AR_Lib.Geometry;

#pragma warning disable 1591

namespace AR_Lib.IO
{
    // OFF format

    /// <summary>OFF Reader class.</summary>
    public static class OFFReader
    {
        public static OFFResult ReadMeshFromFile(string filePath, out OFFMeshData data)
        {
            string[] lines = File.ReadAllLines(filePath);
            data = default;

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
                {
                    // Extract vertices
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
                {
                    // Extract faces
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
            data.Vertices = vertices;
            data.Faces = faces;

            return OFFResult.OK;
        }
    }
}