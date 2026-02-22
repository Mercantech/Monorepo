using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class ClassesTests
{
    [TestMethod]
    public void Class1_PersonKlasseMedNavnOgAlder()
    {
        var output = TestHelper.CaptureOutput(Classes.Class1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteIndeholderAlder = sidste != null && System.Text.RegularExpressions.Regex.IsMatch(sidste, @"\d+");
        Assert.IsTrue(sidsteIndeholderAlder, TestHelper.GuideMessage(
            "Classes Opg 1 (Person)",
            "Du skal lave en klasse Person med properties for navn og alder, oprette et objekt og udskrive informationen.",
            output.Length > 300 ? output[^300..] : output,
            "class Person { public string Navn { get; set; } public int Alder { get; set; } } var p = new Person { Navn = \"...\", Alder = 25 }; Console.WriteLine(p.Navn + \" \" + p.Alder);"));
    }

    [TestMethod]
    public void Class2_BilKlasseMedMærkeModelÅrgang()
    {
        var output = TestHelper.CaptureOutput(Classes.Class2);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteErBilInfo = sidste != null
            && System.Text.RegularExpressions.Regex.IsMatch(sidste, @"(19|20)\d{2}")
            && System.Text.RegularExpressions.Regex.IsMatch(sidste, @"[a-zA-ZæøåÆØÅ]");
        Assert.IsTrue(sidsteErBilInfo, TestHelper.GuideMessage(
            "Classes Opg 2 (Bil)",
            "Du skal lave en klasse Bil med mærke, model og årgang – opret to biler og udskriv deres info.",
            output.Length > 300 ? output[^300..] : output,
            "class Bil { public string Mærke { get; set; } public string Model { get; set; } public int Årgang { get; set; } }"));
    }

    [TestMethod]
    public void Property2_RektangelMedAreal()
    {
        var output = TestHelper.CaptureOutput(Classes.Property2);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteErAreal = sidste != null && System.Text.RegularExpressions.Regex.IsMatch(sidste.Trim(), @"^\d");
        Assert.IsTrue(sidsteErAreal, TestHelper.GuideMessage(
            "Classes Opg 5 (Rektangel)",
            "Du skal lave en klasse Rektangel med Længde og Bredde og en read-only property Areal der returnerer Længde * Bredde.",
            output.Length > 300 ? output[^300..] : output,
            "public double Areal => Længde * Bredde; eller public double Areal { get { return Længde * Bredde; } }"));
    }
}
