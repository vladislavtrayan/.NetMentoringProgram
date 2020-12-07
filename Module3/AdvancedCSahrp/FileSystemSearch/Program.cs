using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CommandLine;
using Services;

namespace FileSystemSearch
{
    class Program
    {
        public class Options
        {
            [Option('f', "filter", Required = false, HelpText = "Apply search filtering.")]
            public IEnumerable<string> Filters { get; set; }
            
            [Option('d', "directory", Required = true, HelpText = "Add searching directory.")]
            public IEnumerable<string> Directories { get; set; }
            
            [Option('s', "shortSearch", Required = false, HelpText = "Stop searching once filtering item found.")]
            public bool ShortSearch { get; set; }
        }
        
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }
        static void RunOptions(Options opts)
        {
            FileSystemVisitor fileSystemVisitor;
            if (!opts.Filters.Any())
            {
                fileSystemVisitor = new FileSystemVisitor();
            }
            else
            {
                var filterHandlers = new List<Predicate<string>>();
                foreach (var filter in opts.Filters)
                {
                    filterHandlers.Add(x => x.Contains(filter));
                }

                fileSystemVisitor = new FileSystemVisitor(filterHandlers.ToArray());
                fileSystemVisitor.ItemFound += RemoveItemFromResult;
            }
            
            if (opts.ShortSearch)
                fileSystemVisitor.FilteredItemFound += StopSearching;

            fileSystemVisitor.SearchStarted += SearchStarted;
            fileSystemVisitor.SearchFinished += SearchFinished;
            foreach (var dir in opts.Directories)
            {
                foreach (var item in fileSystemVisitor.Search(dir))
                {
                    Console.WriteLine(item);
                }
            }
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var error in errs)
            {
                Console.WriteLine(error);   
            }
        }


        private static void RemoveItemFromResult(object sender, ItemFoundArgs args)
        {
            args.RemoveItemFromResult = true;
        }

        private static void StopSearching(object sender, ItemFoundArgs args)
        {
            args.EndSearch = true;
        }
        
        private static void SearchStarted()
        {
            Console.WriteLine("Search started..");
        }

        private static void SearchFinished()
        {
            Console.WriteLine("Search completed..");
        }
    }
}