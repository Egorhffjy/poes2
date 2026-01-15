using System.Timers;
using SentinelIoT.Models;

namespace SentinelIoT.Services;

public class SensorService : IDisposable
{
    private readonly List<Sensor> _sensors = new();
    private readonly System.Timers.Timer _timer;
    
    // Событие для обновления UI
    public event Action? OnChange;

    public SensorService()
    {
        // Создаем фейковые датчики
        var rng = new Random();
        for (int i = 1; i <= 20; i++)
        {
            _sensors.Add(new Sensor 
            { 
                Id = i, 
                Name = $"Sensor-{i:00}", 
                Zone = i <= 10 ? "Server Room" : "Production Line",
                Temperature = rng.Next(20, 30),
                Humidity = rng.Next(40, 60)
            });
        }

        // Таймер тикает каждую секунду
        _timer = new System.Timers.Timer(1500);
        _timer.Elapsed += SimulateUpdates;
        _timer.Start();
    }

   private void SimulateUpdates(object? sender, ElapsedEventArgs e)
    {
        var rng = new Random();
        foreach (var s in _sensors)
        {
            // УВЕЛИЧИЛИ АМПЛИТУДУ: Теперь меняется от -2.5 до +2.5 градуса за тик
            double change = (rng.NextDouble() * 5) - 2.5; 
            s.Temperature = Math.Round(s.Temperature + change, 1);
            
            // Держим в рамках разумного (чтобы график не улетел в бесконечность)
            if (s.Temperature < 15) s.Temperature += 2;
            if (s.Temperature > 45) s.Temperature -= 2;
            
            // Логика статусов
            if (s.Temperature > 40) s.Status = "Critical";
            else if (s.Temperature > 30) s.Status = "Warning";
            else s.Status = "Normal";
        }
        
        OnChange?.Invoke();
    }

    public List<Sensor> GetSensors() => _sensors;
    
    // Получаем среднюю темп для графика
    public double GetAvgTemp() => _sensors.Average(x => x.Temperature);

    public void Dispose() => _timer?.Dispose();
}
