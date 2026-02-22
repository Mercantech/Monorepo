using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class ControlFlowTests
{
    [TestMethod]
    public void If2_UdskriverLigeEllerUlige()
    {
        var output = TestHelper.CaptureOutput(ControlFlow.If2);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harLigeUlige = sidste == "lige" || sidste == "ulige";
        Assert.IsTrue(harLigeUlige, TestHelper.GuideMessage(
            "Control Flow Opg 2 (lige/ulige)",
            "Du skal tjekke om en værdi er lige eller ulige og udskrive enten 'lige' eller 'ulige'.",
            output.Length > 300 ? output[^300..] : output,
            "Brug if (tal % 2 == 0) ... else ... og Console.WriteLine(\"lige\" eller \"ulige\");"));
    }

    [TestMethod]
    public void Switch1_BrugerSwitchTilLigeUlige()
    {
        var output = TestHelper.CaptureOutput(ControlFlow.Switch1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harLigeUlige = sidste == "lige" || sidste == "ulige";
        Assert.IsTrue(harLigeUlige, TestHelper.GuideMessage(
            "Control Flow Opg 3 (switch)",
            "Du skal bruge switch til at tjekke lige/ulige og udskrive resultatet.",
            output.Length > 300 ? output[^300..] : output,
            "switch (tal % 2) { case 0: Console.WriteLine(\"lige\"); break; default: Console.WriteLine(\"ulige\"); break; }"));
    }

    [TestMethod]
    public void Ternary1_BrugerTernaryTilLigeUlige()
    {
        var output = TestHelper.CaptureOutput(ControlFlow.Ternary1);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool harLigeUlige = sidste == "lige" || sidste == "ulige";
        Assert.IsTrue(harLigeUlige, TestHelper.GuideMessage(
            "Control Flow Opg 4 (ternary)",
            "Du skal bruge ternary operator (? :) til at vælge 'lige' eller 'ulige' og udskrive.",
            output.Length > 300 ? output[^300..] : output,
            "string resultat = (tal % 2 == 0) ? \"lige\" : \"ulige\"; Console.WriteLine(resultat);"));
    }
}
