## Unit Testing in C#/Xamarin Projects

### What We Should Test

#### Scenario: Return Value
* GIVEN: I have a method that returns a value.
* WHEN: I write my test for this method.
* THEN: I assert on the value returned.

```csharp
  public int GetNumberOfMoves(int lives, int timeInPlay)
  {
      return lives * timeInPlay;
  }

  [Fact]
  public void GetNumberOfMoves_Called_ReturnsCorrectValue()
  {
      var player = new Player();
      var board = new Board(player);
      
      var result = board.GetNumberOfMoves(2, 20);

      result.Should().Be(40);
  }
```

#### Scenario: Method Called
* GIVEN: I have a method that calls a method on a separate object only.
* WHEN: I write my test for this method.
* THEN: I assert that the method was called on the object.
```csharp
  public void PowerBoost()
  {
      this.player.IncreasePlayerLives();
  }

  [Fact]
  public void PowerBoost_Called_CallsCorrectMethod()
  {
      var player = Substitute.For<Player>(1, "first", "last");
      var board = new Board(player);

      board.PowerBoost();

      player.Received().IncreasePlayerLives();
  }
```

#### Scenario: Method Returns Value Not Asserted On
* GIVEN: That I have a method that calls another method that returns a value.
* AND: the called method has no side-effects.
* AND: the called method returns a value.
* WHEN: I write my test for this method.
* THEN: I do not assert that this method was called.
* AND: I only assert only on return value.

```csharp
  public int GetPlayerStrength()
  {
      return 100 * this.player.StrengthLevel();
  }
  [Fact]
  public void GetPlayerStrength_Called_ReturnsTheCorrectValue()
  {
      var player = new Player();
      var board = new Board(player);

      var result = board.GetPlayerStrength();

      result.Should().Be(1100);
  }
```

#### Scenario: Called Method Has Side Effect(s)
* GIVEN: That I have a method that introduces side-effectss
* WHEN: I write my test for this method.
* THEN: I only test the nearest side-effect(s).

```csharp
  public void MakeMultipleMoves(int firstLocation, int finalLocation)
  {
      this.FirstPosition = firstLocation;
      this.FinalPosition = finalLocation;
  }

  [Fact]
  public void MakeMultipleMoves_Called_SetPropertyValues()
  {
      var player = new Player();
      var board = new Board(player);
      var first = 10;
      var final = 12;
      
      board.MakeMultipleMoves(first, final);

      board.FirstPosition.Should().Be(first);
      board.FinalPosition.Should().Be(final);
  }
```

#### Scenario: Custom Get Properties
* GIVEN: I have a property that has custom logic in my getter
* WHEN: I write my test this property.
* THEN: I should write a test for this custom logic.

```csharp
  public string FullName
  {
      get
      {
          if (this.First != "First")
          {
              return $"{this.First} {this.Last}";
          }

          return "Full Name Not Set";
      }
  }

  [Fact]
  public void FullName_FirstNameSupplied_ReturnsCorrectValue()
  {
      var player = new Player(1, "Joe", "Smith");

      player.FullName.Should().Be("Joe SMITH");
  }

  [Fact]
  public void FullName_FirstNameNotSupplied_ReturnsCorrectValue()
  {
      var player = new Player();
      
      player.FullName.Should().Be("Full Name Not Set");
  }
```

#### Scenario: Custom Set Properties
* GIVEN: I have a property that has custom logic in my setter
* WHEN: I write my test for my property
* THEN: I should write a test for this custom logic.

```csharp
  public string Last
  {
      get => last;
      set
      {
          this.last = value.ToUpperInvariant();
      }
  }

  [Fact]
  public void Last_Called_SetsToCorrectValue()
  {
      var player = new Player();
      
      player.Last = "jones";

      player.Last.Should().Be("JONES");
  }
```

#### Scenario: Constructor Tests for properties
* GIVEN: I have a constructor that accepts params which then sets properties on my class.
* WHEN: I write my test for my class.
* THEN: I should test that the class has set the correct properties on my object.

(this prevents degradation from the original design intent of the class)

```csharp
  public class Player
  {
    public Player(int seniority = 1, string first = "First", string last = "Last")
    {
        this.Seniority = seniority;
        this.First = first;
        this.Last = last;
    }
  }
  
  [Fact]
  public void Constructor_CalledWithParams_SetsCorrectValues()
  {
      var player2 = new Player(10, "Joe", "Smith");

      player2.Seniority.Should().Be(10);
      player2.First.Should().Be("Joe");
      player2.Last.Should().Be("SMITH");
  }
```

#### Scenario: Constructor Tests for params with default values
* GIVEN: I have a constructor that accepts params with default values
* WHEN: I write my test for my class.
* THEN: I should test that the default values to be set correctly.

(this prevents degradation from the original design intent of the class)

```csharp
    public class Player
    {
      public Player(int seniority = 1, string first = "First", string last = "Last")
      {
          this.Seniority = seniority;
          this.First = first;
          this.Last = last;
      }
    }

    [Fact]
    public void Constructor_CalledWithNoParams_SetsCorrectDefaultValues()
    {
        var player2 = new Player();

        player2.Seniority.Should().Be(1);
        player2.First.Should().Be("First");
        player2.Last.Should().Be("LAST");
    }
```

#### Scenario: Property Changes Triggered manually.
* GIVEN: I have triggered a manual property change event.
* WHEN: I write my test for my property/method.
* THEN: I should write a test for the property change event.

```csharp
  public void Roll()
  {
      // this method would perform logic followed by triggering a property change
      // ...
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DieRolled"));
  }

  [Fact]
  public void Roll_Called_TriggersCustomPropertyChange()
  {
      var dieMonitored = new Die().Monitor();
  
      dieMonitored.Subject.Roll();
  
      dieMonitored.Should().Raise("PropertyChanged")
          .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == "DieRolled");
  }
```

### What We Should Not Test

#### Scenario: Automatic Get Propertie(s)
* GIVEN: I have a property that uses C#'s automattic setter.
* WHEN: I write my test for my view model property.
* THEN: I should not write a test for this setter.

(this constitutes testing the "framework" and serves little purpose)

```csharp
  public class Player
  {
      public int Seniority { get; set; }

      // ...
  }

  [Fact]
  public void Seniority_Get_ReturnsValue()
  {
      var player = new Player(11);
      
      player.Seniority.Should().Be(11);
  }
```

#### Scenario: Automatic Set Propertie(s)
* GIVEN: I have a property that uses C#'s automattic getter.
* WHEN: I write my test for my view model property.
* THEN: I should not write a test for this getter.

(this constitutes testing the "framework" and serves little purpose.)

```csharp
  public class Player
  {
      public int Seniority { get; set; }

      // ...
  }

  [Fact]
  public void Seniority_Set_SetsCorrectValue()
  {
      var player = new Player();
      
      player.Seniority = 10;

      player.Seniority.Should().Be(10);
  }
```

#### Scenario: Called Method Is Private, With No Public Side-Effects
* GIVEN: That I have a method that calls a private method.
* THEN: I do not test it.

```csharp
  public class Player {
    private int DeriveStrengthLevel()
    {
        return 11;
    }
  }

  // Do Not Test This
```

#### Scenario: Property Changes Triggered via Fody.
* GIVEN: I have Fody setup and configured.
* THEN: I should not write a test for the property change event.

(this constitutes testing the "plugin" and serves little purpose)

```csharp
    public class Die : INotifyPropertyChanged
    {
        // The property change is being handled automatically by Fody
        public bool Rolled { get; set; }

        // ...
    }

    [Fact]
    public void Rolled_Changes_ExpectPropertyChangedEvent()
    {
        var dieMonitored = new Die().Monitor();
    
        dieMonitored.Subject.Rolled = true;
    
        dieMonitored.Should().RaisePropertyChangeFor(d => d.Rolled);
    }
```