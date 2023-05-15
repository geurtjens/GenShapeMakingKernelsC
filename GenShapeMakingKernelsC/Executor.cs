using System;
namespace CrozzleCodeGen
{
    public class Executor
    {
        public Executor()
        {
        }
        

        public static void Execute(int interlockWidth, int interlockHeight)
        {
            var name = "C" + interlockWidth + "x" + interlockHeight;
            var patterns = PatternFinder.Execute(interlockWidth, interlockHeight);

            Console.WriteLine(name);
            foreach (var pattern in patterns)
            {

                var horizontalList = new List<string>();
                var verticalList = new List<string>();
                foreach (var item in pattern)
                {
                    if (item == "MiddleY" || item == "OuterY" || item == "Up" || item == "Down")
                        verticalList.Add(item);
                    else
                        horizontalList.Add(item);
                }
                var itemName = "";
                foreach (var item in horizontalList)
                    itemName += item[0];
                itemName += "_";
                foreach (var item in verticalList)
                    itemName += item[0];

                Console.WriteLine(itemName);

            }
            Console.WriteLine();


            



            // Creates the summary structure like 2x2
            var result = ExecuteCreator.Execute(patterns, interlockWidth, interlockHeight, name);

            //var path = "/Users/michaelgeurtjens/Developer/Batch/Batch/ShapeCalculators/";
        string path = "/Users/geurt/source/repos/BatchC/ShapeCalculator/";

        System.IO.File.WriteAllText(path + name + ".cpp", result);

            var resultHeader = ExecuteCreator.Execute_H(name);
            System.IO.File.WriteAllText(path + name + ".h", resultHeader);
  
            foreach (var combinations in patterns)
            {
                // C3x3_LRL_UDU is an example
                var structureName = name + "_" + PatternFinder.GetSegmentName(combinations);

                var source = ClusterCreator.Execute(combinations, interlockWidth, interlockHeight, structureName);

                var filename = path + structureName + ".cpp";
                System.IO.File.WriteAllText(filename, source);

                // Now lets create the header file
                string header = CreateHeaderFile(structureName);
                System.IO.File.WriteAllText(filename.Replace(".cpp", ".h"), header);

            }
        }
        // create header file
        static string CreateHeaderFile(string name)
        {
            var result = "";
            result += "#pragma once\n";
            result += "#include \"ClusterModel.h\"\n";
            result += "#include \"WordModelSOA.h\"\n";
            result += "#include <vector>\n";
            result += "\n";
            result += "class " + name + " {\n";
            result += "public:\n";
            result += "    static std::vector<ClusterModel> Execute(WordModelSOA const& w, int scoreMin, int widthMax, int heightMax);\n";
            result += "};\n";
            return result;
        }
    }

    

}