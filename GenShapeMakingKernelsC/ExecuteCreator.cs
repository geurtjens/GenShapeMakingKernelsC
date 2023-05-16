using System;
namespace CrozzleCodeGen
{
    // Creates a function that can call each and every Cluster3x3 pattern for example
    public class ExecuteCreator
	{
		public ExecuteCreator()
		{
		}
        public static string Execute_H(string name)
        {
            string result = "";
            result += "#pragma once\n";
            result += "#include <vector>\n";
            result += "#include \"ShapeModel.h\"\n";
            result += "#include \"WordModelSOA.h\"\n";
            
            result += "#include \"ToShape.h\"\n";
            
            result += "\n";

            result += "class " + name + "\n{\npublic:\n";
            result += "static std::vector<ShapeModel> Execute(WordModelSOA const& w, int scoreMin, int widthMax, int heightMax);\n";
            result += "};";
            return result;
        }
		public static string Execute(List<List<string>> patterns, int interlockWidth, int interlockHeight, string name)
		{
            string result = "#include \"" + name + "\"\n";


            result += "static std::vector<ShapeModel> Execute(WordModelSOA const& w, int scoreMin, int widthMax, int heightMax)\n";
            result += "{\n";

            result += "    int wordCount = w.wordCount\n\n";


            foreach (var combinations in patterns)
            {
                result += "    auto " + PatternUtilities.ConcatinateList(combinations) + " = ToShape.from(" + name + "_" + PatternFinder.GetSegmentName(combinations) + ".Execute(w, wordCount), w, scoreMin, widthMax, heightMax)\n";
            }



            result += "\n";

            result += "    print(\"Cluster" + interlockWidth + "x" + interlockHeight + "\")\n";
            foreach (var combinations in patterns)
            {
                string concatinatedList = PatternUtilities.ConcatinateList(combinations);

                result += "    if (" + concatinatedList + ".size() > 0)\n";
                result += "    {\n";
                result += "        printf(\"" + concatinatedList.ToUpper() + ": %i\", " + concatinatedList + ".size())\n";
                result += "    }\n";
            }

            result += "\n";


            // We want to return a list of lists

            result += "    auto result = ";
            for(int i=0;i<patterns.Count; i++)
            {
                string patternArray = PatternUtilities.ConcatinateList(patterns[i]);

                if (i > 0)
                    result += " + ";

                result += patternArray;
                 
            }
            result += "\n";
            result += "    return result\n";

            result += "}";


            return result;
        }
    
	}
}

