using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class MethodsTests
{
    [TestMethod]
    public void Method1_UdskriverHejVerden()
    {
        var output = TestHelper.CaptureOutput(Methods.Method1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harHejVerden = sidste == "Hej verden!";
        Assert.IsTrue(harHejVerden, TestHelper.GuideMessage(
            "Methods Opg 1",
            "Du skal lave en method der udskriver 'Hej verden!' og kalde den fra Method1.",
            output.Length > 300 ? output[^300..] : output,
            "Lav void MinMetode() { Console.WriteLine(\"Hej verden!\"); } og kald MinMetode();"));
    }

    [TestMethod]
    public void Parameter1_UdskriverHejMedNavn()
    {
        var output = TestHelper.CaptureOutput(Methods.Parameter1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harHejNavn = sidste != null && sidste.StartsWith("Hej ") && sidste.EndsWith("!");
        Assert.IsTrue(harHejNavn, TestHelper.GuideMessage(
            "Methods Opg 4 (parameter)",
            "Du skal lave en method der tager et navn som parameter og udskriver 'Hej [navn]!'.",
            output.Length > 300 ? output[^300..] : output,
            "void Hil(string navn) { Console.WriteLine($\"Hej {navn}!\"); } Hil(\"Anders\");"));
    }

    [TestMethod]
    public void Parameter2_UdskriverSummenAfToTal()
    {
        var output = TestHelper.CaptureOutput(Methods.Parameter2);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteErSum = sidste != null && System.Text.RegularExpressions.Regex.IsMatch(sidste.Trim(), @"^\d+$");
        Assert.IsTrue(sidsteErSum, TestHelper.GuideMessage(
            "Methods Opg 5 (parametre)",
            "Du skal lave en method der tager to tal og udskriver summen.",
            output.Length > 300 ? output[^300..] : output,
            "void Sum(int a, int b) { Console.WriteLine(a + b); } Sum(5, 10);"));
    }

    [TestMethod]
    public void Return1_ReturnererOgUdskriverSum()
    {
        var output = TestHelper.CaptureOutput(Methods.Return1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool sidsteErTal = sidste != null && System.Text.RegularExpressions.Regex.IsMatch(sidste.Trim(), @"^\d+$");
        Assert.IsTrue(sidsteErTal, TestHelper.GuideMessage(
            "Methods Opg 8 (return)",
            "Du skal lave en method der tager to tal og returnerer summen – og udskrive resultatet.",
            output.Length > 300 ? output[^300..] : output,
            "int Sum(int a, int b) { return a + b; } Console.WriteLine(Sum(3, 7));"));
    }
}
