#pragma warning disable 1591

namespace AR_Lib.IO
{
    /// <summary>
    /// Enum containing the result of the OFF conversion.
    /// </summary>
    public enum OFFResult
    {
        OK,
        IncorrectFormat,
        IncorrectVertex,
        IncorrectFace,
        NonMatchingVerticesSize,
        NonMatchingFacesSize,
        FileNotFound,
    }
}