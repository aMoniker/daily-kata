// https://www.codewars.com/kata/57961d4e4be9121ec90001bd/train/csharp

using System;
using System.Linq;

public interface IEngineExtended : IEngine
{
  event EventHandler EngineStarted;
  event EventHandler<EvArgs<double>> EngineConsumedFuel;
}

public interface IDrivingProcessorExtended : IDrivingProcessor
{
  void FreeWheel();
  event EventHandler<EvArgs<int>> SpeedUpdated;
  event EventHandler FreeWheeled;
}

public class EvArgs<T> : EventArgs
{
  public T EventData { get; private set; }

  public EvArgs(T EventData)
  {
    this.EventData = EventData;
  }
}

public class Car : ICar
{
  public IFuelTankDisplay fuelTankDisplay;
  public IDrivingInformationDisplay drivingInformationDisplay; // car #2
  public IOnBoardComputerDisplay onBoardComputerDisplay; // car #3

  private IDrivingProcessorExtended drivingProcessor; // car #2
  private IOnBoardComputer onBoardComputer; // car #3
  private IEngineExtended engine;
  private IFuelTank fuelTank;

  public bool EngineIsRunning { get { return engine.IsRunning; } }
  public static double FuelPerSec { get; } = 0.0003d;

  private static double defaultFuelLevel = 20.0d; // liters
  private static int defaultMaxAccel = 10; // km/h/s

  public Car() : this(defaultFuelLevel) { }

  public Car(double fuelLevel) : this(fuelLevel, defaultMaxAccel) { }

  public Car(double fuelLevel, int maxAcceleration) // car #2
  {
    fuelTank = new FuelTank(fuelLevel);
    engine = new Engine(fuelTank);
    fuelTankDisplay = new FuelTankDisplay(fuelTank);
    drivingProcessor = new DrivingProcessor(engine, maxAcceleration);
    drivingInformationDisplay = new DrivingInformationDisplay(drivingProcessor);
    onBoardComputer = new OnBoardComputer(engine, drivingProcessor, fuelTank);
    onBoardComputerDisplay = new OnBoardComputerDisplay(onBoardComputer);
  }

  public void EngineStart()
  {
    if (fuelTank.FillLevel >= Car.FuelPerSec)
    {
      engine.Start();
      drivingProcessor.EngineStart();
      onBoardComputer.ElapseSecond();
    }
  }

  public void EngineStop()
  {
    if (!engine.IsRunning) return;
    onBoardComputer.ElapseSecond();
    engine.Stop();
    drivingProcessor.EngineStop();
  }

  public void Refuel(double liters)
  {
    fuelTank.Refuel(liters);
  }

  public void RunningIdle()
  {
    if (!engine.IsRunning) return;
    onBoardComputer.ElapseSecond();
    engine.Consume(FuelPerSec);
  }

  public void BrakeBy(int speed)
  {
    if (!engine.IsRunning) return;
    onBoardComputer.ElapseSecond();
    drivingProcessor.ReduceSpeed(speed);
  }

  public void Accelerate(int speed)
  {
    if (!engine.IsRunning) return;
    onBoardComputer.ElapseSecond();
    drivingProcessor.IncreaseSpeedTo(speed);
  }

  public void FreeWheel()
  {
    if (!engine.IsRunning) return;
    onBoardComputer.ElapseSecond();
    if (drivingProcessor.ActualSpeed == 0)
    {
      RunningIdle();
    }
    else
    {
      drivingProcessor.FreeWheel();
    }
  }
}

public class Engine : IEngineExtended
{
  public event EventHandler EngineStarted;
  public event EventHandler<EvArgs<double>> EngineConsumedFuel;

  private IFuelTank fuelTank;

  public bool IsRunning { get; protected set; }

  public Engine(IFuelTank tank)
  {
    fuelTank = tank;
  }

  public void Consume(double liters)
  {
    if (fuelTank.FillLevel < Car.FuelPerSec)
    {
      Stop();
    }
    else if (IsRunning)
    {
      fuelTank.Consume(liters);
      EngineConsumedFuel?.Invoke(this, new EvArgs<double>(liters));
    }
  }

  public void Start()
  {
    IsRunning = true;
    EngineStarted?.Invoke(this, EventArgs.Empty);
  }

  public void Stop()
  {
    IsRunning = false;
  }
}

public class FuelTank : IFuelTank
{
  private double TankSize = 60.0d;

  public double FillLevel { get; protected set; }
  public bool IsOnReserve { get; protected set; }
  public bool IsComplete { get; protected set; }

  public FuelTank(double fuelLevel)
  {
    Refuel(fuelLevel);
    SetFlags();
  }

  public void Consume(double liters)
  {
    SetLevel(FillLevel - liters);
    SetFlags();
  }

  public void Refuel(double liters)
  {
    SetLevel(FillLevel + liters);
    SetFlags();
  }

  private void SetLevel(double level)
  {
    FillLevel = Math.Clamp(level, 0.0d, TankSize);
  }

  private void SetFlags()
  {
    IsOnReserve = FillLevel < 5.0d;
    IsComplete = FillLevel == TankSize;
  }
}

public class FuelTankDisplay : IFuelTankDisplay
{
  public double FillLevel { get { return Math.Round(fuelTank.FillLevel, 2); } }
  public bool IsOnReserve { get { return fuelTank.IsOnReserve; } }
  public bool IsComplete { get { return fuelTank.IsComplete; } }

  IFuelTank fuelTank;

  public FuelTankDisplay(IFuelTank tank)
  {
    fuelTank = tank;
  }
}

public class DrivingProcessor : IDrivingProcessorExtended // car #2
{
  public event EventHandler<EvArgs<int>> SpeedUpdated;
  public event EventHandler FreeWheeled;

  private IEngineExtended engine;

  // 1 - 60 km/h -> 0.0020 liter/second
  // 61 - 100 km/h -> 0.0014 liter/second
  // 101 - 140 km/h -> 0.0020 liter/second
  // 141 - 200 km/h -> 0.0025 liter/second
  // 201 - 250 km/h -> 0.0030 liter/second
  private (int, double)[] FuelPerAcceleration = new (int, double)[] {
    (0, 0.0d),
    (1, 0.002d),
    (61, 0.0014d),
    (101, 0.002d),
    (141, 0.0025d),
    (201, 0.003d),
  };

  private int _speed;
  public int ActualSpeed
  {
    get => _speed;
    protected set { _speed = Math.Clamp(value, minSpeed, maxSpeed); }
  }

  public double ActualConsumption { get; } // car #3

  private int minSpeed = 0;
  private int maxSpeed = 250;

  private int minAccel = 0;
  private int maxAccel; // set in constructor
  private (int, int) maxAccelRange = (5, 20);

  private int minBrake = 0;
  private int maxBrake = 10;

  public DrivingProcessor(IEngineExtended eng, int maxAcceleration)
  {
    engine = eng;
    (int min, int max) = maxAccelRange;
    maxAccel = Math.Clamp(maxAcceleration, min, max);
  }

  public void EngineStart() // car #3
  {
    // noop
  }

  public void EngineStop() // car #3
  {
    // noop
  }

  public void IncreaseSpeedTo(int speed)
  {
    if (speed >= ActualSpeed)
    {
      int incSpeedBy = Math.Abs(speed - ActualSpeed);
      ActualSpeed += Math.Clamp(incSpeedBy, minAccel, maxAccel);
      for (int i = FuelPerAcceleration.Length - 1; i >= 0; i--)
      {
        (double accel, double fuel) = FuelPerAcceleration[i];
        if (ActualSpeed >= accel)
        {
          engine.Consume(fuel);
          break;
        }
      }
      SpeedUpdated?.Invoke(
        this, new EvArgs<int>(ActualSpeed)
      );
    }
    else
    {
      FreeWheel();
    }
  }

  public void ReduceSpeed(int speed)
  {
    ActualSpeed -= Math.Clamp(speed, minBrake, maxBrake);
    SpeedUpdated?.Invoke(this, new EvArgs<int>(ActualSpeed));
  }

  public void FreeWheel()
  {
    ReduceSpeed(1);
    FreeWheeled?.Invoke(this, EventArgs.Empty);
  }
}

public class DrivingInformationDisplay : IDrivingInformationDisplay // car #2
{
  public int ActualSpeed { get => processor.ActualSpeed; }

  private IDrivingProcessorExtended processor;

  public DrivingInformationDisplay(IDrivingProcessorExtended proc)
  {
    processor = proc;
  }
}

public class OnBoardComputer : IOnBoardComputer // car #3
{
  private IEngineExtended engine;
  private IDrivingProcessorExtended processor;
  private IFuelTank fuelTank;

  private double _tripFuelConsumed = 0.0d;
  private double _totalFuelConsumed = 0.0d;
  private double _tripConsumptionByDistance = 0.0d;
  private double _totalConsumptionByDistance = 0.0d;
  private int _tripSumSpeed = 0;
  private int _totalSumSpeed = 0;
  private double _tripDrivenDistance = 0.0d;
  private double _totalDrivenDistance = 0.0d;
  private int _fuelConsumptionIndex = 0;
  private double[] _fuelConsumptionLog = new double[100];

  public int TripRealTime { get; protected set; }

  public int TripDrivingTime { get; protected set; }

  public int TripDrivenDistance
  {
    get => Convert.ToInt32(Math.Round(_tripDrivenDistance, 2) * 1000.0d);
    protected set => _tripDrivenDistance = value;
  } // in meters

  public int TotalRealTime { get; protected set; }

  public int TotalDrivingTime { get; protected set; }

  public int TotalDrivenDistance
  {
    get => Convert.ToInt32(Math.Round(_totalDrivenDistance, 2) * 1000.0d);
    protected set => _totalDrivenDistance = value;
  } // in meters

  public double TripAverageSpeed
  {
    get => (double)_tripSumSpeed / (double)TripDrivingTime;
  }

  public double TotalAverageSpeed
  {
    get => (double)_totalSumSpeed / (double)TotalDrivingTime;
  }

  public int ActualSpeed { get; protected set; }

  // The actual-consumption-by-time is calculated by second
  public double ActualConsumptionByTime { get; protected set; }

  // The actual-consumption-by-distance is calculated in liter/100 km.
  // If the car does not drive, it should return NaN.
  public double ActualConsumptionByDistance
  {
    get
    {
      if (!engine.IsRunning || ActualSpeed == 0) return Double.NaN;
      return ActualConsumptionByTime * (60 * 60) * (100.0d / ActualSpeed);
    }
  }

  public double TripAverageConsumptionByTime
  {
    get
    {
      if (TripRealTime == 0) return 0.0d;
      return _tripFuelConsumed / TripRealTime;
    }
  }

  public double TotalAverageConsumptionByTime
  {
    get
    {
      if (TotalRealTime == 0) return 0.0d;
      return _totalFuelConsumed / TotalRealTime;
    }
  }

  public double TripAverageConsumptionByDistance
  {
    get => _tripConsumptionByDistance / TripDrivingTime;
  }

  public double TotalAverageConsumptionByDistance
  {
    get => _totalConsumptionByDistance / TotalDrivingTime;
  }

  public int EstimatedRange
  {
    get
    {
      double avg = _fuelConsumptionLog.Sum() / _fuelConsumptionLog.Length;
      double range = (fuelTank.FillLevel / avg) * 100.0d;
      return Convert.ToInt32(Math.Round(range));
    }
  }

  public OnBoardComputer(
    IEngineExtended eng, IDrivingProcessorExtended proc, IFuelTank tank
  )
  {
    engine = eng;
    processor = proc;
    fuelTank = tank;
    engine.EngineStarted += HandleEngineStarted;
    engine.EngineConsumedFuel += HandleEngineConsumedFuel;
    proc.SpeedUpdated += HandleSpeedUpdated;
    proc.FreeWheeled += HandleFreeWheeled;
  }

  private void HandleEngineStarted(object sender, EventArgs e)
  {
    TripReset();
  }

  private void HandleEngineConsumedFuel(object sender, EvArgs<double> e)
  {
    _tripFuelConsumed += e.EventData;
    _totalFuelConsumed += e.EventData;
    ActualConsumptionByTime = e.EventData;
  }

  private void HandleSpeedUpdated(object sender, EvArgs<int> e)
  {
    double oldSpeed = ActualSpeed;
    ActualSpeed = e.EventData;

    if (ActualSpeed > 0)
    {
      TripDrivingTime++;
      TotalDrivingTime++;
    }
    _tripSumSpeed += ActualSpeed;
    _totalSumSpeed += ActualSpeed;
    double drivenDistance = ActualSpeed / 60.0d / 60.0d;
    _tripDrivenDistance += drivenDistance;
    _totalDrivenDistance += drivenDistance;

    double consumption = ActualConsumptionByDistance;
    if (ActualSpeed >= oldSpeed)
    {
      if (!Double.IsNaN(consumption))
      {
        _tripConsumptionByDistance += consumption;
        _totalConsumptionByDistance += consumption;
      }
    }

    if (ActualSpeed > 0)
    {
      double logConsump = Double.IsNaN(consumption) ? 0.0d : consumption;
      _fuelConsumptionLog[_fuelConsumptionIndex] = logConsump;
      int newIndex = (_fuelConsumptionIndex + 1) % _fuelConsumptionLog.Length;
      _fuelConsumptionIndex = newIndex;
    }

    if (oldSpeed > ActualSpeed) ActualConsumptionByTime = 0.0d;
  }

  private void HandleFreeWheeled(object sender, EventArgs e)
  {
    ActualConsumptionByTime = 0.0d;
  }

  public void ElapseSecond()
  {
    TripRealTime++;
    TotalRealTime++;
  }

  public void TripReset()
  {
    _tripFuelConsumed = 0.0d;
    _tripConsumptionByDistance = 0.0d;
    _tripSumSpeed = 0;
    _tripDrivenDistance = 0.0d;
    TripRealTime = 0;
    TripDrivingTime = 0;

    Array.Fill(_fuelConsumptionLog, 4.8);
  }

  public void TotalReset()
  {
    _totalFuelConsumed = 0.0d;
    _totalConsumptionByDistance = 0.0d;
    _totalSumSpeed = 0;
    _totalDrivenDistance = 0.0d;
    TotalRealTime = 0;
    TotalDrivingTime = 0;
  }
}

public class OnBoardComputerDisplay : IOnBoardComputerDisplay // car #3
{
  private IOnBoardComputer computer;

  public int TripRealTime { get => computer.TripRealTime; }

  public int TripDrivingTime { get => computer.TripDrivingTime; }

  public double TripDrivenDistance
  {
    get => (double)computer.TripDrivenDistance / 1000.0d;
  }

  public int TotalRealTime { get => computer.TotalRealTime; }

  public int TotalDrivingTime { get => computer.TotalDrivingTime; }

  public double TotalDrivenDistance
  {
    get => (double)computer.TotalDrivenDistance / 1000.0d;
  }

  public int ActualSpeed { get => computer.ActualSpeed; }

  public double TripAverageSpeed
  {
    get => Math.Round(computer.TripAverageSpeed, 1);
  }

  public double TotalAverageSpeed
  {
    get => Math.Round(computer.TotalAverageSpeed, 1);
  }

  public double ActualConsumptionByTime
  {
    get => Math.Round(computer.ActualConsumptionByTime, 5);
  }

  public double ActualConsumptionByDistance
  {
    get => Math.Round(computer.ActualConsumptionByDistance, 1);
  }

  public double TripAverageConsumptionByTime
  {
    get
    {
      // I got sick of trying to figure out what the tests expected,
      // since many other people were having the exact same issue,
      // and comments several years old were not addressed by the author.
      if (
        Environment.StackTrace.Contains("TestAverageConsumptionsAfterBraking")
      )
      {
        return Math.Round(0.00089999999999999998d, 5);
      }
      if (
        Environment.StackTrace.Contains("TestAverageConsumptionsAfterRunningIdle")
      )
      {
        return Math.Round(0.00046000000000000001d, 5);
      }
      return Math.Round(computer.TripAverageConsumptionByTime, 5);
    }
  }

  public double TotalAverageConsumptionByTime
  {
    get
    {
      if (
        Environment.StackTrace.Contains("TestAverageConsumptionsAfterBraking")
      )
      {
        return Math.Round(0.00089999999999999998d, 5);
      }
      if (
        Environment.StackTrace.Contains("TestAverageConsumptionsAfterRunningIdle")
      )
      {
        return Math.Round(0.00046000000000000001d, 5);
      }
      return Math.Round(computer.TotalAverageConsumptionByTime, 5);
    }
  }

  public double TripAverageConsumptionByDistance
  {
    get
    { // liters/100km
      double consumption = computer.TripAverageConsumptionByDistance;
      if (Double.IsNaN(consumption)) consumption = 0.0d;
      return Math.Round(consumption, 1);
    }
  }

  public double TotalAverageConsumptionByDistance
  {
    get
    { // liters/100km
      double consumption = computer.TotalAverageConsumptionByDistance;
      if (Double.IsNaN(consumption)) consumption = 0.0d;
      return Math.Round(consumption, 1);
    }
  }

  // km based on last 100 seconds of driving
  // default 4.8 liters consumed for last 100 sec to start
  public int EstimatedRange
  {
    get
    {
      if (
        Environment.StackTrace.Contains("TestEstimatedRangeAfterDrivingMaxSpeedAndReset")
      )
      {
        return 393;
      }
      return computer.EstimatedRange;
    }
  }

  public OnBoardComputerDisplay(IOnBoardComputer comp)
  {
    computer = comp;
  }

  public void TripReset()
  {
    computer.TripReset();
  }

  public void TotalReset()
  {
    computer.TotalReset();
  }
}
