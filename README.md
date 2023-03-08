# Sharpe's Gold
An implementation in C# of Dijkstra's algorithm for finding the shortest path to Sharpe's Gold

[algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm),
[documentation comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments),
[dotnet](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet),
[json](https://www.nuget.org/packages/Newtonsoft.Json/)<br>

## Challenges
1. Complete code within `SharpesNeighbours.Calculate` until all automated tests pass
2. Design/execute manual system tests with objective to find defects in available time

## To run the automated tests
`dotnet test`

## To launch the user interface
`dotnet run --project src/SharpesGold.UI`

## .NET solution commands
These commands used during construction:<br>
`dotnet new gitignore`<br>
`dotnet new sln`<br>
`dotnet new wpf --output src/SharpesGold.UI`<br>
`dotnet new classlib --output src/SharpesGold.Logic`<br>
`dotnet new classlib --output src/SharpesGold.Maps`<br>
`dotnet new classlib --output src/SharpesGold.Neighbours`<br>
`dotnet add src/SharpesGold.Maps package Newtonsoft.Json`<br>
`dotnet sln add src/SharpesGold.Logic/SharpesGold.Logic.csproj`<br>
`dotnet sln add src/SharpesGold.Maps/SharpesGold.Maps.csproj`<br>
`dotnet sln add src/SharpesGold.Neighbours/SharpesGold.Neighbours.csproj`<br>
`dotnet add src/SharpesGold.Logic reference src/SharpesGold.Neighbours/SharpesGold.Neighbours.csproj`<br>
`dotnet add src/SharpesGold.UI reference src/SharpesGold.Logic/SharpesGold.Logic.csproj`<br>
`dotnet add src/SharpesGold.UI reference src/SharpesGold.Maps/SharpesGold.Maps.csproj`<br>
`dotnet add src/SharpesGold.UI reference src/SharpesGold.Neighbours/SharpesGold.Neighbours.csproj`<br>
`dotnet new nunit --output test/SharpesGold.Maps.Tests`<br>
`dotnet add test/SharpesGold.Maps.Tests reference src/SharpesGold.Maps/SharpesGold.Maps.csproj`<br>
`dotnet sln add test/SharpesGold.Maps.Tests/SharpesGold.Maps.Tests.csproj`<br>
`dotnet test test/SharpesGold.Maps.Tests`<br>
`dotnet new nunit --output test/SharpesGold.Neighbours.Tests`<br>
`dotnet add test/SharpesGold.Neighbours.Tests reference src/SharpesGold.Neighbours/SharpesGold.Neighbours.csproj`<br>
`dotnet sln add test/SharpesGold.Neighbours.Tests/SharpesGold.Neighbours.Tests.csproj`<br>
`dotnet test test/SharpesGold.Neighbours.Tests`<br>

## Images courtesy of
`https://img.icons8.com/color/256/gold-bars.png`<br>
`https://img.icons8.com/emoji/256/green-circle-emoji.png`<br>
`https://img.icons8.com/fluency/256/brick-wall.png`<br>
`https://img.icons8.com/fluency/256/home-page.png`<br>
`https://img.icons8.com/windows/256/dot-logo.png`<br>
