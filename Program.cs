namespace Szintfelmero;

record Match {
    public int Round { get; set; }
    public int HomeGoals { get; set; }
    public int GuestGoals { get; set; }
    public int HalftimeHomeGoals { get; set; }
    public int HalftimeGuestGoals { get; set; }
    public string HomeName { get; set; } = "";
    public string GuestName { get; set; } = "";
}

internal static class Program {
    private static int matchCount = 0;
    private static Match[] matches = [];


    private static void Main(string[] args) {
        Console.WriteLine("### Feladat 1 ###");
        var lines = File.ReadAllLines("../../../Feladat/meccs.txt");
        matchCount = Convert.ToInt32(lines.First());
        matches = lines.Skip(1).Select(line => line.Split(" ")).Select(segments => new Match() {
            Round = Convert.ToInt32(segments[0]),
            HomeGoals = Convert.ToInt32(segments[1]),
            GuestGoals = Convert.ToInt32(segments[2]),
            HalftimeHomeGoals = Convert.ToInt32(segments[3]),
            HalftimeGuestGoals = Convert.ToInt32(segments[4]),
            HomeName = segments[5],
            GuestName = segments[6],
        }).ToArray();
        Console.WriteLine($"{matches.Length} meccs beolvasva");

        Console.WriteLine("### Feladat 2 ###");
        Console.Write("Írjon be egy fordulószámot: ");
        var round = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(
            String.Join("\n",
                matches
                    .Where(x => x.Round == round)
                    .Select(x =>
                        $"{x.HomeName}-{x.GuestName}: {x.HomeGoals}-{x.GuestGoals} ({x.HalftimeHomeGoals}-{x.HalftimeGuestGoals})")
            ));
    }
}