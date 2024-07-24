internal class Program
{
    static void Main(string[] args)
    {
        string delimiter = ",";
        List<int> fields = new List<int>();
        string file = null;

        for (int i = 1; i < args.Length; i++)
        {
            switch (args[i]){
                case "-f":
                    if (i + 1 < args.Length)
                    {
                        fields.AddRange(args[++i].Split(',').Select(int.Parse));
                    }
                    else
                    {
                        Console.WriteLine("Error: -f option requires a value");
                        return;
                    }
                    break;
                case "-d":
                    if(i + 1 < args.Length)
                    {
                        delimiter = args[i++];
                    }
                    else {
                        delimiter = "\t";
                    }
                    break;
                default:
                    if(file == null)
                    {
                        file = args[i];
                    }
                    else 
                    {
                        Console.WriteLine($"Error: Unexpected argument {args[i]}");
                        return;
                    }
                    break;
            }
        }

        if(file == null) {
            Console.WriteLine("Usage: cut_tool [-f][fields] [-d][deliminator] <file-path>");
            return;
        }

        var lines = File.ReadAllLines(file);

        for (int i = 0; i < lines.Length; i++)
        {
            int tabCount = 0;
            int delLength = delimiter.Length;
            string fieldValue = "";
            for (int j = 0; j < lines[i].Length; j++)
            {
                for(int k = j + delLength; lines[i].Substring(k, delLength) != delimiter; k++) 
                {
                    if(lines[i].Substring(j, delLength) == delimiter){
                        fieldValue = lines[i].Substring(j + delLength, k);
                        Console.WriteLine(fieldValue);
                        tabCount++;
                    }
                } 
            }
        }
    }
}