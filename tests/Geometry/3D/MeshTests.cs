using System.Collections.Generic;
using Paramdigma.Core.Geometry;
using Paramdigma.Core.HalfEdgeMesh;
using Xunit;

namespace Paramdigma.Core.Tests.Geometry._3D
{
    public class MeshTests
    {
        
        public Mesh FlatSquare
        {
            get
            {
                var ptA = new Point3d(0, 0, 0);
                var ptB = new Point3d(1, 0, 0);
                var ptC = new Point3d(1, 1, 0);
                var ptD = new Point3d(0, 1, 0);
                var vertices = new List<Point3d> {ptA, ptB, ptC, ptD};
                var face = new List<int> {0, 1, 2, 3};
                var mesh = new Mesh(vertices, new List<List<int>> {face});
                return mesh;
            }
        }
        
        [Fact]
        public void CanCreate_Mesh()
        {
            var mesh = FlatSquare;
            Assert.NotNull(mesh);
            Assert.NotEmpty(mesh.Vertices);
            Assert.NotEmpty(mesh.Faces);
        }

        [Fact]
        public void CanCompute_Boundary()
        {
            var mesh = FlatSquare;
            Assert.NotEmpty(mesh.Boundaries);
            Assert.Single(mesh.Boundaries);
        }

        [Fact]
        public void CanDetect_IsolatedVertices()
        {
            var mesh = FlatSquare;
            Assert.False(mesh.HasIsolatedVertices());
            
            mesh.Vertices.Add(new MeshVertex(3,0,0));
            Assert.True(mesh.HasIsolatedVertices());

        }
        [Fact]
        public void CanDetect_IsolatedFaces()
        {
            var mesh = FlatSquare;
            Assert.False(mesh.HasIsolatedFaces());
        }

        [Fact]
        public void CanCompute_EulerCharacteristic()
        {
            var mesh = FlatSquare;
            Assert.Equal(1,mesh.EulerCharacteristic);
        }

        [Fact]
        public void CanCheck_QuadMesh()
        {
            var mesh = FlatSquare;
            Assert.True(mesh.IsQuadMesh());
            Assert.False(mesh.IsNgonMesh());
            Assert.False(mesh.IsTriangularMesh());
        }
        
        [Fact]
        public void CanConvert_ToString()
        {
            Assert.IsType<string>(FlatSquare.ToString());
            Assert.IsType<string>(FlatSquare.GetMeshInfo());
        }
    }
}