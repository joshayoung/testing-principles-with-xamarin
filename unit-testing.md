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

#### Scenario: Dependency's Method Called
* GIVEN: I have a method that calls another method on a separate object. 
* WHEN: I write my test for this method.
* THEN: I assert that the method was called on that object.
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

#### Scenario: Dependency's Method Returns a Value While Method Under Test Returns a Derived Value 
* GIVEN: My subject under test calls another method which returns a value.
* AND: this method has no side-effects.
* AND: the method under test returns a value.
* WHEN: I write my test for this method.
* THEN: I do not assert that this method was called.
* AND: I only assert only on return value of the method under test.

```csharp
  public int GetPlayerStrength()
  {
      // Do not assert that `StrengthLevel()` was called on `player`
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
* GIVEN: That I have a method that introduces side-effects.
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
* GIVEN: I have a property that has custom logic in my getter.
* WHEN: I write my test this property.
* THEN: I should write test(s) for this custom logic.

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

      player.FullName.Should().Be("Joe Smith");
  }

  [Fact]
  public void FullName_FirstNameNotSupplied_ReturnsCorrectValue()
  {
      var player = new Player();
      
      player.FullName.Should().Be("Full Name Not Set");
  }
```

#### Scenario: Custom Set Properties
* GIVEN: I have a property that has custom logic in my setter.
* WHEN: I write my tests for this property.
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

#### Scenario: Constructor Tests for Properties
* GIVEN: I have a constructor that accepts params which then sets properties on my class.
* WHEN: I write my test for this constructor.
* THEN: I should test that the class has set the correct properties on my object.

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
      var player = new Player(10, "Joe", "Smith");

      player.Seniority.Should().Be(10);
      player.First.Should().Be("Joe");
      player.Last.Should().Be("Smith");
  }
```

#### Scenario: Constructor Tests for Params with Default Values
* GIVEN: I have a constructor that accepts params with default values.
* WHEN: I write my test for my constructor.
* THEN: I should test that the default values are set correctly.

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
        var player = new Player();

        player.Seniority.Should().Be(1);
        player.First.Should().Be("First");
        player.Last.Should().Be("LAST");
    }
```

#### Scenario: Property Changes Triggered Manually.
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

#### Scenario: Private Method That Introduces a Public Side-Effect
* GIVEN: A public method that calls a private method.
* WHEN: I write my test for my property/method.
* THEN: I assert that the private method introduced the public side-effect.
```csharp
// Huge code-smell, but this is just for example
public class Player
{
  public int Strength { get; set; }

  public void GetStrengthLevelAndAssign()
  {
      this.AssignStrengthLevel();

      // ... more logic
  }

  private void AssignStrengthLevel()
  {
      this.Strength = this.StrengthLevel();
      // ... more logic
  }
}

[Fact]
public void GetStrengthLevelAndAssign_Called_SetsStrength()
{
    var player = new Player();

    player.GetStrengthLevelAndAssign();

    player.Strength.Should().Be(11);
}
```

### What We Should Not Test

#### Scenario: Automatic Get Properties
* GIVEN: I have a property that uses C#'s automattic getter.
* WHEN: I write my test for this property.
* THEN: I should **not** write a test for this getter.

```csharp
  public class Player
  {
      public int GameDuration { get; set; }

      // ...
  }

  [Fact]
  public void GameDuration_Get_ReturnsValue()
  {
    var player = new Player(11);

    player.GameDuration.Should().Be(0);
  }
```

#### Scenario: Automatic Set Properties
* GIVEN: I have a property that uses C#'s automattic setter.
* WHEN: I write my test for my property.
* THEN: I should **not** write a test for this setter.

```csharp
  public class Player
  {
      public int GameDuration { get; set; }

      // ...
  }

  [Fact]
  public void GameDuration_Set_SetsCorrectValue()
  {
      var player = new Player();
      
      player.GameDuration = 10;

      player.GameDuration.Should().Be(10);
  }
```

#### Scenario: Called Method Is Private
* GIVEN: That I have a private method.
* THEN: I do **not** test it.

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
* GIVEN: I have the Fody plugin setup and configured.
* AND: I have a property with an automatic get/set `{ get; set; }`
* THEN: I should not write a test for the property change event.

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
