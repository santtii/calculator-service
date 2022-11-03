using CalculatorAPI.Core.Interfaces.Services;

namespace CalculatorAPI.Client
{
    public class CalculatorConsole
    {
        public string? TrackingId { get; set; }

        private readonly IClientService _clientService;

        public CalculatorConsole(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task ExecuteAsync()
        {
            while (await MainMenuAsync()) ;
        }

        private void AddTrackingIdAsync()
        {
            Console.Write("Tracking-Id: ");
            _clientService.AddTrackingId(TrackingId = Console.ReadLine());
        }

        private async Task AddAsync()
        {
            Console.Write("Operands (space separated): ");
            var inputString = Console.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(inputString))
            {
                Console.Write("Operands (space separated): ");
                inputString = Console.ReadLine()?.Trim();
            }
            var data = Array.ConvertAll(inputString?.Split(' '), Convert.ToDecimal);
            var result = await _clientService.AddRequestAsync(data);

            if (result != null)
            {
                Console.WriteLine($"Operation result: {result?.Sum}");
            }
            else
            {
                Console.WriteLine($"An error ocurred, plese try again");
            }
        }

        private async Task SubtractAsync()
        {
            decimal minuend;
            decimal subtrahend;

            Console.Write("Minuend: ");
            while (!decimal.TryParse(Console.ReadLine(), out minuend)) { Console.Write("Minuend: "); }
            Console.Write("Subtrahend: ");
            while (!decimal.TryParse(Console.ReadLine(), out subtrahend)) { Console.Write("Subtrahend: "); }
            var result = await _clientService.SubtractRequestAsync(minuend, subtrahend);

            if (result != null)
            {
                Console.WriteLine($"Operation result: {result?.Difference}");
            }
            else
            {
                Console.WriteLine($"An error ocurred, plese try again");
            }
        }

        private async Task MultiplyAsync()
        {
            Console.Write("Operands (space separated): ");
            var inputString = Console.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(inputString))
            {
                Console.Write("Operands (space separated): ");
                inputString = Console.ReadLine()?.Trim();
            }
            var data = Array.ConvertAll(inputString?.Split(' '), Convert.ToDecimal);
            var result = await _clientService.MultiplyRequestAsync(data);

            if (result != null)
            {
                Console.WriteLine($"Operation result: {result?.Product}");
            }
            else
            {
                Console.WriteLine($"An error ocurred, plese try again");
            }
        }

        private async Task DivideAsync()
        {
            decimal dividend;
            decimal divisor;

            Console.Write("Dividend: ");
            while (!decimal.TryParse(Console.ReadLine(), out dividend)) { Console.Write("Dividend: "); }
            Console.Write("Divisor: ");
            while (!decimal.TryParse(Console.ReadLine(), out divisor)) { Console.Write("Divisor: "); }
            var result = await _clientService.DivisionRequestAsync(dividend, divisor);

            if (result != null)
            {
                Console.WriteLine($"Quotient: {result?.Quotient}");
                Console.WriteLine($"Remainder: {result?.Remainder}");
            }
            else
            {
                Console.WriteLine($"An error ocurred, plese try again");
            }
        }

        private async Task SquareRootAsync()
        {
            decimal number;

            Console.Write("Number: ");
            while (!decimal.TryParse(Console.ReadLine(), out number)) { Console.Write("Number: "); }
            var result = await _clientService.SquareRootRequestAsync(number);

            if (result != null)
            {
                Console.WriteLine($"Operation result: {result?.Square}");
            }
            else
            {
                Console.WriteLine($"An error ocurred, plese try again");
            }
        }

        private async Task JournalQueryAsync()
        {
            Console.Write("Tracking-Id: ");
            var result = await _clientService.JournalQuery(Console.ReadLine());

            if (result != null)
            {
                Console.WriteLine($"Operations:");
                result?.Operations?.ToList().ForEach(element => Console.WriteLine($" - {element}"));
            }
            else
            {
                Console.WriteLine($"An error ocurred, plese try again");
            }
        }

        private static async Task<bool> ContinueAsync()
        {
            Console.Write("\r\n\tPress ESC to return or Enter to continue");
            switch (Console.ReadKey(true).KeyChar)
            {
                case (char)ConsoleKey.Escape:
                    return await Task.FromResult(false);
                case (char)ConsoleKey.Enter:
                default:
                    Console.WriteLine("\r\n");
                    return await Task.FromResult(true);
            }
        }

        private async Task<bool> MainMenuAsync(int refresh = 2000)
        {
            Console.Clear();
            Console.WriteLine($"0) Tracking-Id ({TrackingId})");
            Console.WriteLine("1) Add (+)");
            Console.WriteLine("2) Subtract (-)");
            Console.WriteLine("3) Multiply (x)");
            Console.WriteLine("4) Divide (/)");
            Console.WriteLine("5) Square root (√)");
            Console.WriteLine("6) Journal Query");
            Console.Write("\r\nSelect an option: ");

            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case '0':
                        Console.Clear();
                        Console.WriteLine("Tracking-Id");
                        Console.WriteLine("----------------------------------------------------------------");
                        AddTrackingIdAsync();
                        return true;
                    case '1':
                        Console.Clear();
                        Console.WriteLine($"Addition (Tracking-Id: {TrackingId})");
                        Console.WriteLine("----------------------------------------------------------------");
                        do
                        {
                            await AddAsync();
                        } while (await ContinueAsync());
                        return true;
                    case '2':
                        Console.Clear();
                        Console.WriteLine($"Subtraction (Tracking-Id: {TrackingId})");
                        Console.WriteLine("----------------------------------------------------------------");
                        do
                        {
                            await SubtractAsync();
                        } while (await ContinueAsync());
                        return true;
                    case '3':
                        Console.Clear();
                        Console.WriteLine($"Multiplication (Tracking-Id: {TrackingId})");
                        Console.WriteLine("----------------------------------------------------------------");
                        do
                        {
                            await MultiplyAsync();
                        } while (await ContinueAsync());
                        return true;
                    case '4':
                        Console.Clear();
                        Console.WriteLine($"Division (Tracking-Id: {TrackingId})");
                        Console.WriteLine("----------------------------------------------------------------");
                        do
                        {
                            await DivideAsync();
                        } while (await ContinueAsync());
                        return true;
                    case '5':
                        Console.Clear();
                        Console.WriteLine($"Square Root (Tracking-Id: {TrackingId})");
                        Console.WriteLine("----------------------------------------------------------------");
                        do
                        {
                            await SquareRootAsync();
                        } while (await ContinueAsync());
                        return true;
                    case '6':
                        Console.Clear();
                        Console.WriteLine($"Journal Query (Tracking-Id: {TrackingId})");
                        Console.WriteLine("----------------------------------------------------------------");
                        do
                        {
                            await JournalQueryAsync();
                        } while (await ContinueAsync());
                        return true;
                    case '7':
                    default:
                        return true;
                }
            }
            Thread.Sleep(refresh);
            return true;
        }
    }
}
