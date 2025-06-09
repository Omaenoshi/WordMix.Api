namespace WordMix.Domain.Services;

public static class LevelService
{
    public static int CalculateLevel(int experience)
    {
        var level = 1;

        while (true)
        {
            var requiredXp = GetXpForLevel(level + 1);
            if (experience < requiredXp)
                return level;
            level++;
        }

        static int GetXpForLevel(int level)
        {
            const int a = 50;
            const int b = 50;
            return a * level * level + b * level;
        }
    }
}