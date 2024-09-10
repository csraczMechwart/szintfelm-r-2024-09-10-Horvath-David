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

        Console.WriteLine("### Feladat 3 ###");
        Console.WriteLine(
            String.Join("\n",
                matches
                    .Where(x => (x.HalftimeHomeGoals > x.HalftimeGuestGoals && x.HomeGoals < x.GuestGoals) ||
                                (x.HalftimeGuestGoals > x.HalftimeHomeGoals && x.GuestGoals < x.HomeGoals))
                    .Select(x =>
                        $"{x.Round}. forduló - győztes csapat: {(x.HomeGoals > x.GuestGoals ? x.HomeName : x.GuestName)}")
            ));

        Console.WriteLine("### Feladat 4 ###");
        Console.Write("Írjon be egy csapatnevet: ");
        var chosenTeam = Console.ReadLine();

        Console.WriteLine("### Feladat 5 ###");
        var goalsGave = 0;
        var goalsTaken = 0;
        goalsGave += matches.Where(x => x.HomeName == chosenTeam).Select(x => x.HomeGoals).Sum();
        goalsGave += matches.Where(x => x.GuestName == chosenTeam).Select(x => x.GuestGoals).Sum();
        goalsTaken += matches.Where(x => x.HomeName == chosenTeam).Select(x => x.GuestGoals).Sum();
        goalsTaken += matches.Where(x => x.GuestName == chosenTeam).Select(x => x.HomeGoals).Sum();
        Console.WriteLine($"lőtt: {goalsGave}, kapott: {goalsTaken}");

        Console.WriteLine("### Feladat 6 ###");
        var lostMatch = matches.FirstOrDefault(x => x.HomeName == chosenTeam && x.GuestGoals > x.HomeGoals);
        Console.WriteLine(lostMatch != null
            ? $"{lostMatch.Round} fordulóban kaptak ki otthon először."
            : "A csapat otthon veretlen maradt.");
    }
}