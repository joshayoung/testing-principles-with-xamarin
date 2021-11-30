## Integration Testing in C#/Xamarin Projects

### What We Should Test

### What We Should Not Test

#### Scenario: Automatic Property Change Triggered from View Model
* GIVEN: I have a model class with a property named X.
* AND: I have a view model class property that is also named X.
* AND: I have my view model set to automatically invoke property changes for like-named properties in my view model.
* WHEN: I write my test for this.
* THEN: I assert that the model `PropertyChanged` triggered a `PropertyChanged` in my view model.

```csharp
  public class Die : INotifyPropertyChanged
  {
      public bool Rolled { get; set; }

      // ...
  }

  public class DieViewModel : INotifyPropertyChanged
  {
    private readonly Die die;
    
    public DieViewModel(Die die)
    {
      this.die = die;
      
      // Automatic properties change trigger for like-named properties in view model:
      die.PropertyChanged += (_, args) => this.PropertyChanged?.Invoke(this, args);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool Rolled
    {
      get => this.die.Rolled;
      set => this.die.Rolled = value;
    }
  }

  [Fact]
  public void Model_PropertyChanges_ExpectVMPropertyChange()
  {
    var dieMock = Substitute.ForPartsOf<Die>();
    var dieViewModelMonitored = new DieViewModel(dieMock).Monitor();

    dieMock.Configure().PropertyChanged += 
      Raise.Event<PropertyChangedEventHandler>(this, new PropertyChangedEventArgs(nameof(Die.Rolled)));

    dieViewModelMonitored.Should()
      .Raise("PropertyChanged")
      .WithSender(dieViewModelMonitored.Subject)
      .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(DieViewModel.Rolled));
  }
```

#### Scenario: View Model Property Returns Value from Model Property
* GIVEN: I have a view model with a property.
* AND: My view model pulls in the data from my model property of the same name.
* THEN: I do not test this.

```csharp
  public class Die : INotifyPropertyChanged
  {
      public bool Rolled { get; set; }

      public virtual event PropertyChangedEventHandler PropertyChanged;
  }

  public class DieViewModel : INotifyPropertyChanged
  {
    private readonly Die die;
    
    public DieViewModel(Die die)
    {
        this.die = die;
    }
    
    public bool Rolled
    {
        get => this.die.Rolled;
        set => this.die.Rolled = value;
    }
  }

  // Do not write this test:
  [Fact]
  public void Model_ValueChanges_ExpectVMValueChange()
  {
      var die = new Die();
      var dieViewModel = new DieViewModel(die);

      die.Rolled = true;

      dieViewModel.Rolled.Should().Be(true);
  }
```