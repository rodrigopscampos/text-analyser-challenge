using System;
using System.IO;
using TextAnalyser.Algorithms;
using TextAnalyser.Domain.Interfaces.Algorithms;
using TextAnalyser.Domain.ValueObjects;
using TextAnalyser.Infra.SqlServerRepository;
using TextAnalyser.Serializers;
using TextAnalyser.Services;

namespace TextAnalyser
{
    class Program
    {
        static ITextAnalyserAlgorithms _algorithm;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Text Analyser!");
            Console.WriteLine();

            string filePath;

            Console.WriteLine();
            Console.WriteLine("Inform the file path:");
            filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            Console.WriteLine("Inform the analyse algorithm");
            Console.WriteLine(" 1: TextSubStringsAlgorithm");
            Console.WriteLine(" 2: CharacterTableMapAlgorithm");

            try
            {
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        _algorithm = new TextSubStringsAlgorithm();
                        break;

                    case 2:
                        _algorithm = new CharacterTableMapAlgorithm();
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Great. Lets analyse file {filePath}");
            Console.WriteLine();

            var sqlServerRepository = new SqlServerAnalyseDbContext();
            var jsonSerializer = new JsonSerializer();

            Console.WriteLine("-----------------------");
            Console.WriteLine($"Repository: {sqlServerRepository}");
            Console.WriteLine($"Serializer: {jsonSerializer}");
            Console.WriteLine($"Algorithm: {_algorithm}");
            Console.WriteLine();

            Console.WriteLine("Running analyser ...");
            var analyser = new TextAnalyserService(
                repository: sqlServerRepository,
                serializer: jsonSerializer,
                algorithm: _algorithm);

            var result = analyser.Analyse(new AnalyseCommand(filePath));

            Console.WriteLine(result);
        }
    }
}
