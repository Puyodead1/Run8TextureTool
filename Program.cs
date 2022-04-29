using System.Text;
using MoreLinq;
using JeremyAnsel.Media.WavefrontObj;
using Newtonsoft.Json.Linq;

void convert(string vboPath, string iboPath, string dest)
{
    ObjFile objFile = new ObjFile();

    // read the Vertex Buffer Object data from json
    string vboJsonString = File.ReadAllText(vboPath);
    dynamic vboArray = JArray.Parse(vboJsonString);

    // read the Index Buffer data from json
    //string iboString = File.ReadAllText(iboPath);
    //dynamic iboArray = JArray.Parse(iboString);
    //int[] buffer = iboArray.ToObject<int[]>();
    //int[] buffer = new int[vboArray.Count];
    //Buffer.BlockCopy(iboArray.ToObject<int[]>(), 0, buffer, 0, vboArray.Count);


    foreach (var vertexObject in vboArray)
    {
        float[] Position = vertexObject.Position.ToObject<float[]>();
        float[] Normal = vertexObject.Normal.ToObject<float[]>();
        float[] TexCoord = vertexObject.TexCoord.ToObject<float[]>();

        objFile.Vertices.Add(new ObjVertex(Position[0], Position[1], Position[2]));
        objFile.VertexNormals.Add(new ObjVector3(Normal[0], Normal[1], Normal[2]));
        objFile.TextureVertices.Add(new ObjVector3(TexCoord[0], TexCoord[1]));
    }

    //foreach (var faceVerts in buffer.Batch(3))
    //{
    //    ObjFace face = new ObjFace();
    //    foreach (var faceVert in faceVerts)
    //    {
    //        ObjTriplet v = new ObjTriplet(faceVert, 0, 0);
    //        face.Vertices.Add(v);
    //    }
    //    objFile.Faces.Add(face);
    //}

    Console.WriteLine("Number of Vertices: " + objFile.Vertices.Count);
    Console.WriteLine("Number of VertexNormals: " + objFile.VertexNormals.Count);
    Console.WriteLine("Number of TextureVertices: " + objFile.TextureVertices.Count);
    Console.WriteLine("Number of Faces: " + objFile.Faces.Count);

    objFile.WriteTo(dest);
}

// convert("C:\\Users\\23562\\Documents\\Code\\random\\ts\\vbo.json", "C:\\Users\\23562\\Documents\\Code\\random\\ts\\ibo.json", "C:\\Users\\23562\\Documents\\Code\\random\\ts\\ts.obj");

string rawVbo = File.ReadAllText(@"C:\\Users\\23562\\Documents\\Code\\random\\ts\\vbo.txt");
string[] rawVboLines = rawVbo.Split("\n");

List<float> vertices = new List<float>();

foreach (var line in rawVboLines)
{
    var rawValues = line.Split(" ");
    foreach(string value in rawValues)
    {
        long hex = Convert.ToInt64(value, 16);
        var a = BitConverter.GetBytes(hex);

        float f1 = BitConverter.ToSingle(a, 0);
        float f2 = BitConverter.ToSingle(a, 4);
        if (!float.IsNaN(f1) && !float.IsNaN(f2))
        {
            Console.WriteLine(f1);
            Console.WriteLine(f2);
        }
    }
}

Console.WriteLine(vertices.Count());
