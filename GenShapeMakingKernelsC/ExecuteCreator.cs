using System;
namespace CrozzleCodeGen
{
    // Creates a function that can call each and every Cluster3x3 pattern for example
    public class ExecuteCreator
	{
		public ExecuteCreator()
		{
		}
        
		public static string Execute(List<List<string>> patterns, int interlockWidth, int interlockHeight, string name)
		{
			string result = "public class " + name + " {\n"; ;
            

            result += "    static std::vector<ShapeModel> Execute(WordModelSOA const& w, int scoreMin, int widthMax, int heightMax) -> [ShapeModel] {\n\n";

            result += "        auto wordCount = w.wordCount\n\n";


            foreach (var combinations in patterns)
            {
                result += "        auto " + PatternUtilities.ConcatinateList(combinations) + " = ToShape.from(cluster: " + name + "_" + PatternFinder.GetSegmentName(combinations) + ".Execute(w, wordCount), w, scoreMin, widthMax, heightMax)\n";
            }



            result += "\n";

            result += "        print(\"Cluster" + interlockWidth + "x" + interlockHeight + "\")\n";
            foreach (var combinations in patterns)
            {
                string concatinatedList = PatternUtilities.ConcatinateList(combinations);

                result += "        if (" + concatinatedList + ".count > 0) {\n";
                result += "            print(\"" + concatinatedList.ToUpper() + ": \\(" + concatinatedList + ".count)\")\n";
                result += "        }\n";
            }

            result += "\n";


            // We want to return a list of lists

            result += "        auto result = ";
            for(int i=0;i<patterns.Count; i++)
            {
                string patternArray = PatternUtilities.ConcatinateList(patterns[i]);

                if (i > 0)
                    result += " + ";

                result += patternArray;
                 
            }
            result += "\n";
            result += "        return result\n";

            result += "    }\n";
            result += "}";


            return result;
        }
    
	}
}

