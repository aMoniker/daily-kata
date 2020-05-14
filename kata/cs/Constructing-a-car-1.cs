// https://www.codewars.com/kata/578b4f9b7c77f535fc00002f/train/csharp

using System;

public class Car : ICar
{
  public IFuelTankDisplay fuelTankDisplay;
  private IEngine engine;
  private IFuelTank fuelTank;

  public bool EngineIsRunning { get { return engine.IsRunning; } }
  public static double FuelPerSec { get; } = 0.0003d;
  private static double defaultFuelLevel = 20.0d;

  public Car() : this(defaultFuelLevel) { }

  public Car(double fuelLevel)
  {
    fuelTank = new FuelTank(fuelLevel);
    engine = new Engine(fuelTank);
    fuelTankDisplay = new FuelTankDisplay(fuelTank);
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
