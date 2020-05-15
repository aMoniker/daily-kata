// https://www.codewars.com/kata/578df8f3deaed98fcf0001e9/train/csharp

using System;

interface IDrivingProcessorExtended : IDrivingProcessor
{
  void FreeWheel();
}

public class Car : ICar
{
  public IFuelTankDisplay fuelTankDisplay;
  public IDrivingInformationDisplay drivingInformationDisplay; // car #2
  private IDrivingProcessorExtended drivingProcessor; // car #2
  private IEngine engine;
  private IFuelTank fuelTank;

  public bool EngineIsRunning { get { return engine.IsRunning; } }

  public static double FuelPerSec { get; } = 0.0003d;

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

  private static double defaultFuelLevel = 20.0d; // liters
  private static int defaultMaxAccel = 10; // km/h/s


  public Car() : this(defaultFuelLevel) { }

  public Car(double fuelLevel) : this(fuelLevel, defaultMaxAccel) { }

  public Car(double fuelLevel, int maxAcceleration) // car #2
  {
    fuelTank = new FuelTank(fuelLevel);
    engine = new Engine(fuelTank);
    fuelTankDisplay = new FuelTankDisplay(fuelTank);
    drivingProcessor = new DrivingProcessor(maxAcceleration);
    drivingInformationDisplay = new DrivingInformationDisplay(drivingProcessor);
  }

  public void EngineStart()
  {
    engine.Start();
  }

  public void EngineStop()
  {
    engine.Stop();
  }

  public void Refuel(double liters)
  {
    fuelTank.Refuel(liters);
  }

  public void RunningIdle()
  {
    engine.Consume(FuelPerSec);
  }

  public void BrakeBy(int speed)
  {
    drivingProcessor.ReduceSpeed(speed);
  }

  public void Accelerate(int speed)
  {
    if (!engine.IsRunning) return;

    drivingProcessor.IncreaseSpeedTo(speed);

    for (int i = FuelPerAcceleration.Length - 1; i >= 0; i--)
    {
      (double accel, double fuel) = FuelPerAcceleration[i];
      if (drivingProcessor.ActualSpeed >= accel)
      {
        engine.Consume(fuel);
        break;
      }
    }
  }

  public void FreeWheel()
  {
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

public class Engine : IEngine
{
  IFuelTank fuelTank;

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
    }
  }

  public void Start()
  {
    if (fuelTank.FillLevel >= Car.FuelPerSec) IsRunning = true;
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
  private int _speed;
  public int ActualSpeed
  {
    get => _speed;
    protected set { _speed = Math.Clamp(value, minSpeed, maxSpeed); }
  }

  private int minSpeed = 0;
  private int maxSpeed = 250;

  private int minAccel = 0;
  private int maxAccel; // set in constructor
  private (int, int) maxAccelRange = (5, 20);

  private int minBrake = 0;
  private int maxBrake = 10;

  public DrivingProcessor(int maxAcceleration)
  {
    (int min, int max) = maxAccelRange;
    maxAccel = Math.Clamp(maxAcceleration, min, max);
  }

  public void IncreaseSpeedTo(int speed)
  {
    if (speed <= ActualSpeed)
    {
      FreeWheel();
      return;
    }
    int incSpeedBy = Math.Abs(speed - ActualSpeed);
    ActualSpeed += Math.Clamp(incSpeedBy, minAccel, maxAccel);
  }

  public void ReduceSpeed(int speed)
  {
    ActualSpeed -= Math.Clamp(speed, minBrake, maxBrake);
  }

  public void FreeWheel()
  {
    ReduceSpeed(1);
  }
}

public class DrivingInformationDisplay : IDrivingInformationDisplay // car #2
{
  public int ActualSpeed { get => processor.ActualSpeed; }

  private IDrivingProcessor processor;

  public DrivingInformationDisplay(IDrivingProcessor proc)
  {
    processor = proc;
  }
}
