using meshParser;
using System.Globalization;

string fileVerticesPath = "vtkVertices2D.txt";
string fileElementsPath = "vtkElements2D.txt";

List<Point3D> points = new(75000);

using (StreamReader sr = new(fileVerticesPath))
{
    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        var lexs = line.Split(' ');
        double.TryParse(lexs[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
        points.Add(new()
        {
            x = double.Parse(lexs[0], CultureInfo.InvariantCulture),
            y = double.Parse(lexs[1], CultureInfo.InvariantCulture),
            z = result
        });
    }

}

List<Element> elements = new(212000);
using (StreamReader sr = new(fileElementsPath))
{
    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        var lexs = line.Split(' ');
        int size = int.Parse(lexs[0]);
        Element newElement = new();
        for (int i = 0; i < size; i++)
        {
            newElement.Vertices.Add(points[int.Parse(lexs[i + 1])]);
           
        }
        elements.Add(newElement);
    }
}

if (fileVerticesPath.Contains("2D"))
{
    // ПЕРИМЕТР
    Console.WriteLine("Площадь ячеек по xy: ");
    List<double> polygonAreas = elements.Select(x => Element.CalculatePolygonArea(x.Vertices)).ToList();
    Console.WriteLine(string.Join("\n", polygonAreas));

    // ПЛОЩАДИ ОПИСАННЫХ ОКРУЖНОСТЕЙ
    Console.WriteLine("Площади описанных окружностей кажой ячейки: ");
    List<double> circumscribedCircleArea = elements.Select(x => Element.CalculateCircumscribedCircleArea(x.Vertices)).ToList();
    Console.WriteLine(string.Join("\n", circumscribedCircleArea));
}
else if (fileVerticesPath.Contains("3D"))
{
    // ОБЪЕМ
    Console.WriteLine("Объем ячеек по xyz: ");
    List<double> volumeParallelogram = elements.Select(x => Element.CalculateVolume(x.Vertices)).ToList();
    Console.WriteLine(string.Join("\n", volumeParallelogram));
}


//int HexStringToInt(string hexString)
//{
//    if (string.IsNullOrEmpty(hexString))
//    {
//        throw new ArgumentException("Input string cannot be null or empty");
//    }

//    int decimalValue = Convert.ToInt32(hexString, 16);
//    return decimalValue;
//}