string data = string.Empty;
using (var reader = new StreamReader("input.txt"))
{
    data = reader.ReadToEnd();
}

int _extractNumberFromCharArray(char[] chars, int index, int length, string word, int value)
{
    if (chars.Length >= index + length)
    {
        var currentSection = string.Concat(chars.Skip(index).Take(length));
        if (currentSection == word)
        {
            return value;
        }
    }
    return -1;
}

int _checkForNumber(char[] chars, int index)
{
    var firstChar = chars[index];

    switch (firstChar)
    {
        case 'o':
            {
                return _extractNumberFromCharArray(chars, index, 3, "one", 1);
            }
        case 't':
            {
                var two = _extractNumberFromCharArray(chars, index, 3, "two", 2);
                if (two != -1)
                {
                    return two;
                }
                return _extractNumberFromCharArray(chars, index, 5, "three", 3);
            }
        case 'f':
            {
                var four = _extractNumberFromCharArray(chars, index, 4, "four", 4);
                if (four != -1)
                {
                    return four;
                }
                return _extractNumberFromCharArray(chars, index, 4, "five", 5);
            }
        case 's':
            {
                var six = _extractNumberFromCharArray(chars, index, 3, "six", 6);
                if (six != -1)
                {
                    return six;
                }
                return _extractNumberFromCharArray(chars, index, 5, "seven", 7);
            }
        case 'e':
            {
                return _extractNumberFromCharArray(chars, index, 5, "eight", 8);
            }
        case 'n':
            {
                return _extractNumberFromCharArray(chars, index, 4, "nine", 9);
            }
        default:
            {
                return -1;
            }
    }
}

int _getCalibrationValue(string input)
{
    var result = ("-1", "-1");
    var current = "-1";
    var charArray = input.ToArray();
    var index = 0;
    foreach (var character in charArray)
    {
        if (int.TryParse(character.ToString(), out var number))
        {
            current = number.ToString();
            if (result.Item1 == "-1")
            {
                result.Item1 = current;
            }
        }
        else
        {
            var numberParsed = _checkForNumber(charArray, index);
            if (numberParsed > -1)
            {
                current = numberParsed.ToString();
                if (result.Item1 == "-1")
                {
                    result.Item1 = current;
                }
            }
        }
        index++;
    }
    result.Item2 = current;
    return int.Parse(result.Item1 + result.Item2);
}

var sum = 0;
foreach (var line in data.Split(Environment.NewLine))
{
    var calibration = _getCalibrationValue(line);
    sum += calibration;
}
Console.WriteLine(sum);