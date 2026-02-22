using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class ArraysTests
{
    [TestMethod]
    public void Array2_UdskriverStørsteTal()
    {
        var output = TestHelper.CaptureOutput(Arrays.Array2);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteErEtTal = sidste != null && System.Text.RegularExpressions.Regex.IsMatch(sidste.Trim(), @"^\d+$");
        Assert.IsTrue(sidsteErEtTal, TestHelper.GuideMessage(
            "Arrays Opg 2",
            "Du skal gemme 5 tal i et array, finde det største og udskrive det.",
            output.Length > 300 ? output[^300..] : output,
            "Brug et array, et loop til at finde max (sammenlign med int max = tal[0]; og opdater i loop), og Console.WriteLine(max);"));
    }

    [TestMethod]
    public void Dict1_UdskriverNavneOgAldre()
    {
        var output = TestHelper.CaptureOutput(Arrays.Dict1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteIndeholderAlder = sidste != null && System.Text.RegularExpressions.Regex.IsMatch(sidste, @"\d+");
        Assert.IsTrue(sidsteIndeholderAlder, TestHelper.GuideMessage(
            "Arrays Opg 9 (Dictionary)",
            "Du skal gemme 3 personer (navn + alder) i en Dictionary og udskrive dem.",
            output.Length > 300 ? output[^300..] : output,
            "Dictionary<string, int> personer = new Dictionary<string, int>(); personer.Add(\"Navn\", 25); foreach (var p in personer) Console.WriteLine(p.Key + \" \" + p.Value);"));
    }
}
