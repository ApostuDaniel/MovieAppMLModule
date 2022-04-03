namespace MLModel
{
    class Program
    {
        static void Main(string[] args)
        {
            //SplitData();
            //Console.WriteLine("Done splitting");

        }

        public static void SplitData()
        {
            string[] dataset = File.ReadAllLines(@"../../../Data\rating.csv");
  
            int numLines = dataset.Length;
            var body = dataset.Skip(1);

            //sortare dupa data la care au fost facute ratingurile
            var sorted = body.Select(line => new { SortKey = DateTime.Parse(line.Split(',')[3]).Ticks, Line = line })
                             .OrderBy(x => x.SortKey)
                             .Select(x => x.Line);
            //90% for fi date pentru training si 10% date pentru testare
            File.WriteAllLines(@"../../../Data\ratings_train.csv", dataset.Take(1).Concat(sorted.Take((int)(numLines * 0.9))));
            File.WriteAllLines(@"../../../Data\ratings_test.csv", dataset.Take(1).Concat(sorted.TakeLast((int)(numLines * 0.1))));
        }
    }
}