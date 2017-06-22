public static class Extensions
{
    /// <summary>
    /// Converts a string to Int
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToInt(this string value)
    {
        int result = 0;

        if (!string.IsNullOrEmpty(value))
            int.TryParse(value, out result);

        return result;
    }

    /// <summary>
    /// converts a float from milliseconds to a string in format 00:00
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string ToTimeString(this float time)
    {
        var minutes = time / 60;
        var seconds = time % 60;

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}