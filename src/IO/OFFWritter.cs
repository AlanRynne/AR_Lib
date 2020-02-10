using System.Collections.Generic;
using AR_Lib.HalfEdgeMesh;

#pragma warning disable 1591

namespace AR_Lib.IO
{
    /// <summary>Writter class.</summary>
    public static class OFFWritter
    {
        /// <summary>
        /// Write a Half-Edge mesh to a .OFF file.
        /// </summary>
        /// <param name="mesh">Half-edge mesh to export.</param>
        /// <param name="filePath">Path to save the file to.</param>
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
}