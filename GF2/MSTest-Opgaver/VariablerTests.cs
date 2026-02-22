using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class VariablerTests
{
    [TestMethod]
    public void Int1_UdskriverTalletTi()
    {
        var output = TestHelper.CaptureOutput(Variabler.Int1);
        var sidsteLinje = TestHelper.GetLastStudentLine(output);
        bool harTi = sidsteLinje == "10";
        Assert.IsTrue(harTi, TestHelper.GuideMessage(
            "Variabler Opg 1 (int)",
            "Du skal lave en int-variabel med værdi 10 og udskrive den til konsollen (Console.WriteLine).",
            output.Length > 200 ? output[^200..] : output,
            "Eksempel: int number = 10; Console.WriteLine(number);"));
    }

    [TestMethod]
    public void Double1_UdskriverFemOgKvart()
    {
        var output = TestHelper.CaptureOutput(Variabler.Double1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool har525 = sidste != null && (sidste.Contains("5.25") || sidste.Contains("5,25"));
        Assert.IsTrue(har525, TestHelper.GuideMessage(
            "Variabler Opg 2 (double)",
            "Du skal udskrive decimaltallet 5,25 (5 og 1/4). Brug en double-variabel.",
            output.Length > 200 ? output[^200..] : output,
            "double værdi = 5.25; Console.WriteLine(værdi);"));
    }

    [TestMethod]
    public void Strings1_UdskriverHelloWorld()
    {
        var output = TestHelper.CaptureOutput(Variabler.Strings1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harHelloWorld = sidste != null && sidste.Contains("Hello, World!");
        Assert.IsTrue(harHelloWorld, TestHelper.GuideMessage(
            "Variabler Opg 3 (string)",
            "Du skal udskrive teksten 'Hello, World!' (med udråbstegn).",
            output.Length > 200 ? output[^200..] : output,
            "string tekst = \"Hello, World!\"; Console.WriteLine(tekst);"));
    }

    [TestMethod]
    public void Bool1_UdskriverSandhedsværdi()
    {
        var output = TestHelper.CaptureOutput(Variabler.Bool1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harBool = sidste == "True" || sidste == "False";
        Assert.IsTrue(harBool, TestHelper.GuideMessage(
            "Variabler Opg 4 (bool)",
            "Du skal udskrive en sandhedsværdi (true eller false).",
            output.Length > 200 ? output[^200..] : output,
            "bool sand = true; Console.WriteLine(sand);"));
    }

    [TestMethod]
    public void StringInterpolation_UdskriverHelloWorldSamlet()
    {
        var output = TestHelper.CaptureOutput(Variabler.StringInterpolation);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harHelloWorld = sidste == "Hello, World!";
        Assert.IsTrue(harHelloWorld, TestHelper.GuideMessage(
            "Variabler Opg 5 (string interpolation)",
            "Du skal kombinere to strenge 'Hello, ' og 'World!' med string interpolation til 'Hello, World!'.",
            output.Length > 200 ? output[^200..] : output,
            "string a = \"Hello, \"; string b = \"World!\"; Console.WriteLine($\"{a}{b}\");"));
    }

    [TestMethod]
    public void StringInterpolation2_KombinererHejMedDig()
    {
        var output = TestHelper.CaptureOutput(Variabler.StringInterpolation2);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harHejMedDig = sidste != null && sidste.Contains("Hej med dig!");
        Assert.IsTrue(harHejMedDig, TestHelper.GuideMessage(
            "Variabler Opg 6 (string interpolation)",
            "Du skal kombinere del1=\"Hej\", del2=\"!\", del3=\"dig\", del4=\"med\" til sætningen: Hej med dig!",
            output.Length > 200 ? output[^200..] : output,
            "Brug $\"{del1} {del4} {del3}{del2}\" – rækkefølgen er vigtig."));
    }

    [TestMethod]
    public void Float1_Udskriver314()
    {
        var output = TestHelper.CaptureOutput(Variabler.Float1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool har314 = sidste != null && (sidste.Contains("3.14") || sidste.Contains("3,14"));
        Assert.IsTrue(har314, TestHelper.GuideMessage(
            "Variabler Opg 7 (float)",
            "Du skal udskrive 3,14 som float. Brug f-suffix: 3.14f.",
            output.Length > 200 ? output[^200..] : output,
            "float f = 3.14f; Console.WriteLine(f);"));
    }

    [TestMethod]
    public void Char1_UdskriverStortA()
    {
        var output = TestHelper.CaptureOutput(Variabler.Char1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harStortA = sidste == "A";
        Assert.IsTrue(harStortA, TestHelper.GuideMessage(
            "Variabler Opg 8 (char)",
            "Du skal udskrive det første bogstav i alfabetet som stort: A.",
            output.Length > 200 ? output[^200..] : output,
            "char c = 'A'; Console.WriteLine(c);"));
    }

    [TestMethod]
    public void Decimal1_Udskriver100Komma5()
    {
        var output = TestHelper.CaptureOutput(Variabler.Decimal1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool har1005 = sidste != null && (sidste.Contains("100.5") || sidste.Contains("100,5"));
        Assert.IsTrue(har1005, TestHelper.GuideMessage(
            "Variabler Opg 9 (decimal)",
            "Du skal udskrive 100,5 som decimal. Brug m-suffix: 100.5m.",
            output.Length > 200 ? output[^200..] : output,
            "decimal d = 100.5m; Console.WriteLine(d);"));
    }
}
