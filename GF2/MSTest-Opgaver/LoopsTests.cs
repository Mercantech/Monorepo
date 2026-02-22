using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgaver;
using MSTest_Opgaver;

namespace MSTest_Opgaver;

[TestClass]
public sealed class LoopsTests
{
    [TestMethod]
    public void Loop1_UdskriverTallene1Til10()
    {
        var output = TestHelper.CaptureOutput(Loops.Loop1);
        bool harLinje1 = TestHelper.HasExactLine(output, "1");
        bool harLinje10 = TestHelper.HasExactLine(output, "10");
        Assert.IsTrue(harLinje1 && harLinje10, TestHelper.GuideMessage(
            "Loops Opg 1",
            "Du skal bruge et loop (fx for) til at udskrive tallene 1, 2, 3, ... 10 (ét tal per linje).",
            output.Length > 300 ? output[^300..] : output,
            "for (int i = 1; i <= 10; i++) Console.WriteLine(i);"));
    }

    [TestMethod]
    public void Loop3_Summen1Til100Er5050()
    {
        var output = TestHelper.CaptureOutput(Loops.Loop3);
        var sidste = TestHelper.GetLastStudentLine(output);
        bool har5050 = sidste != null && sidste.Contains("5050");
        Assert.IsTrue(har5050, TestHelper.GuideMessage(
            "Loops Opg 3",
            "Du skal lægge alle tal fra 1 til 100 sammen. Summen er 5050 – udskriv den.",
            output.Length > 300 ? output[^300..] : output,
            "int sum = 0; for (int i = 1; i <= 100; i++) sum += i; Console.WriteLine(sum);"));
    }

    [TestMethod]
    public void Loop8_UdskriverUligeTal()
    {
        var output = TestHelper.CaptureOutput(Loops.Loop8);
        bool har49SomLinje = TestHelper.HasExactLine(output, "49");
        Assert.IsTrue(har49SomLinje, TestHelper.GuideMessage(
            "Loops Opg 8",
            "Du skal udskrive alle ulige tal mellem 1 og 50 (1, 3, 5, ... 49).",
            output.Length > 300 ? output[^300..] : output,
            "for (int i = 1; i < 50; i += 2) Console.WriteLine(i);"));
    }

    [TestMethod]
    public void BankeBøf_IndeholderBankeBøf()
    {
        var output = TestHelper.CaptureOutput(Loops.BankeBøf);
        bool harBanke = TestHelper.HasExactLine(output, "Banke");
        bool harBøf = TestHelper.HasExactLine(output, "Bøf");
        bool harBankeBøf = TestHelper.HasExactLine(output, "BankeBøf");
        Assert.IsTrue(harBanke && harBøf && harBankeBøf, TestHelper.GuideMessage(
            "Loops – BankeBøf",
            "Du skal udskrive tallene 1–30, men erstatte med 'Banke' (deleligt med 3), 'Bøf' (deleligt med 5), 'BankeBøf' (begge).",
            output.Length > 400 ? output[^400..] : output,
            "for (int i = 1; i <= 30; i++) { if (i % 15 == 0) Console.WriteLine(\"BankeBøf\"); else if (i % 3 == 0) ... else if (i % 5 == 0) ... else Console.WriteLine(i); }"));
    }
}
