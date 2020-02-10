#pragma warning disable 1591

namespace AR_Lib.IO
{
    /// <summary>
    /// Enum containing the result of the OFF conversion.
    /// </summary>
    public enum OFFResult
    {
        OK,
        Incorrect_Format,
        Incorrect_Vertex,
        Incorrect_Face,
        Non_Matching_Vertices_Size,
        Non_Matching_Faces_Size,
        File_Not_Found,
    }
}