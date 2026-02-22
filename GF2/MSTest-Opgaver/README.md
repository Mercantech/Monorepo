# MSTest-Opgaver

Unit tests til **Opgaver**-projektet. Testene er tænkt som **undervisnings- og læringsværktøj**: de fejler indtil opgaven er løst korrekt, og ved fejl får eleven en kort guide med:

- **Forventet** – hvad opgaven beder om
- **Din output** – hvad der faktisk blev udskrevet (eller tom, hvis der mangler kode)
- **Tip** – en konkret kodeide til løsningen

## Kør tests

Fra roden af repo eller fra denne mappe:

```bash
dotnet test MSTest-Opgaver/MSTest-Opgaver.csproj
```

I Visual Studio / VS Code: kør test-suiten som sædvanligt (Test Explorer, "Run All Tests").

## Opbygning

| Testfil           | Dækker opgaver i |
|-------------------|-------------------|
| `VariablerTests`  | [1. Variabler.cs](../Opgaver/1.%20Variabler.cs) – int, double, string, bool, interpolation, float, char, decimal |
| `InputsTests`     | [2. Inputs.cs](../Opgaver/2.%20Inputs.cs) – ReadLine og Parse (string, int, double, bool) |
| `ControlFlowTests`| [3.ControlFlow.cs](../Opgaver/3.ControlFlow.cs) – If2, Switch1, Ternary1 (lige/ulige) |
| `LoopsTests`      | [4.Loops.cs](../Opgaver/4.Loops.cs) – Loop1, Loop3, Loop8, BankeBøf |
| `ArraysTests`     | [5.Arrays.cs](../Opgaver/5.Arrays.cs) – Array2 (største tal), Dict1 |
| `MethodsTests`    | [6.Methods.cs](../Opgaver/6.Methods.cs) – Method1, Parameter1/2, Return1 |
| `ClassesTests`    | [7.Classes.cs](../Opgaver/7.Classes.cs) – Class1 (Person), Class2 (Bil), Property2 (Rektangel) |

Tests der bruger **Console.ReadLine** (fx Inputs) får automatisk testdata i testen; eleven behøver ikke taste under kørsel.

## Tip til undervisere

- Lad eleverne køre `dotnet test` jævnligt – de ser med det samme, hvilke opgaver der er løst.
- Fejlbeskederne er skrevet så eleven kan rette sig ind efter "Forventet" og "Tip" uden at skulle åbne løsningsfilerne.
- Flere opgaver (fx mini-projekter og åbne opgaver) har bevidst ingen test – de kan stadig bruges til diskussion og manuel tjek.
