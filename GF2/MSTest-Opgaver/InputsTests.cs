using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class InputsTests
{
    [TestMethod]
    public void String1_GemmerOgUdskriverIndtastetStreng()
    {
        const string indtastning = "Min test streng";
        var output = TestHelper.CaptureOutputWithInput(Inputs.String1, indtastning);
        var sidste = TestHelper.GetLastStudentLine(output);
        Assert.IsTrue(sidste == indtastning, TestHelper.GuideMessage(
            "Inputs Opg 1 (string)",
            "Du skal læse en streng med Console.ReadLine(), gemme den i en variabel og udskrive den.",
            output.Length > 300 ? output[^300..] : output,
            "string s = Console.ReadLine(); Console.WriteLine(s);"));
    }

    [TestMethod]
    public void Int1_GemmerOgUdskriverIndtastetTal()
    {
        const string indtastning = "42";
        var output = TestHelper.CaptureOutputWithInput(Inputs.Int1, indtastning);
        var sidste = TestHelper.GetLastStudentLine(output);
        Assert.IsTrue(sidste == "42", TestHelper.GuideMessage(
            "Inputs Opg 2 (int)",
            "Du skal læse et tal med Console.ReadLine(), parse det til int (int.Parse) og udskrive det.",
            output.Length > 300 ? output[^300..] : output,
            "int tal = int.Parse(Console.ReadLine()); Console.WriteLine(tal);"));
    }

    [TestMethod]
    public void Double1_GemmerOgUdskriverIndtastetDecimaltal()
    {
        const string indtastning = "3.14";
        var output = TestHelper.CaptureOutputWithInput(Inputs.Double1, indtastning);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool har314 = sidste != null && (sidste.Contains("3.14") || sidste.Contains("3,14"));
        Assert.IsTrue(har314, TestHelper.GuideMessage(
            "Inputs Opg 3 (double)",
            "Du skal læse et decimaltal med Console.ReadLine(), parse til double og udskrive det.",
            output.Length > 300 ? output[^300..] : output,
            "double d = double.Parse(Console.ReadLine()); Console.WriteLine(d);"));
    }

    [TestMethod]
    public void Bool1_GemmerOgUdskriverIndtastetBool()
    {
        var output = TestHelper.CaptureOutputWithInput(Inputs.Bool1, "true");
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harTrue = sidste == "True" || sidste == "true";
        Assert.IsTrue(harTrue, TestHelper.GuideMessage(
            "Inputs Opg 4 (bool)",
            "Du skal læse true/false med Console.ReadLine(), parse til bool (bool.Parse) og udskrive.",
            output.Length > 300 ? output[^300..] : output,
            "bool b = bool.Parse(Console.ReadLine()); Console.WriteLine(b);"));
    }
}
