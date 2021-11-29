## Unit Testing in C#/Xamarin Projects

### What We Should Test

#### Scenario: Return Value
* GIVEN: I have a method that returns a value.
* WHEN: I write my test for this method.
* THEN: I assert on the value returned.

```csharp
```

#### Scenario: Method Called
* GIVEN: I have a method that calls a method on a separate object only.
* WHEN: I write my test for this method.
* THEN: I assert that the method was called on the object.
```csharp
```

#### Scenario: Method Returns Value Not Asserted On
* GIVEN: That I have a method that calls another method that returns a value.
* AND: the called method has no side-effects.
* AND: the called method returns a value.
* WHEN: I write my test for this method.
* THEN: I do not assert that this method was called.
* AND: I only assert only on return value.

```csharp
```

#### Scenario: Called Method Is Provide, With One Side-Effect
* GIVEN: That I have a method that calls a private method.
* WHEN: I write my test for this method.
* THEN: I only test the nearest side-effect.

```csharp
```

#### Scenario: Called Method Is Provide, With Two or More Side-Effects
* GIVEN: That I have a method that calls a private method.
* WHEN: I write my test for this method.
* THEN: I only test the nearest side-effects.

```csharp
```

#### Scenario: Automatic Get/Set Properties
* GIVEN: I have a property that uses C#'s automattic get/set values.
* WHEN: I write my test for my view model property.
* THEN: I should not write a test for these setters or getters

(this constitutes testing the "framework" / "plugin" and serves little purpose)

```csharp
```

#### Scenario: Custom Get Properties
* GIVEN: I have a property that has custom logic in my getter
* WHEN: I write my test for my view model property.
* THEN: I should write a test for this custom logic.

```csharp
```

#### Scenario: Custom Set Properties
* GIVEN: I have a property that has custom logic in my setter
* WHEN: I write my test for my property
* THEN: I should write a test for this custom logic.

```csharp
```

#### Scenario: Constructor Tests for properties
* GIVEN: I have a constructor that accepts params which then sets properties on my class.
* WHEN: I write my test for my class.
* THEN: I should test that the class has set the correct properties on my object.

(this prevents degradation from the original design intent of the class)

```csharp
```

#### Scenario: Constructor Tests for params with default values
* GIVEN: I have a constructor that accepts params with default values
* WHEN: I write my test for my class.
* THEN: I should test that the default values to be set correctly.

(this prevents degradation from the original design intent of the class)

```csharp
```

#### Scenario: Property Changes Triggered manually.
* GIVEN: I have triggered a manual property change event view 'Invoke'.
* WHEN: I write my test for my view model property.
* THEN: I should write a test for the property change event.

(this constitutes testing the "framework" / "plugin" and serves little purpose)

```csharp
```

### What We Should Not Test

#### Scenario: Called Method Is Provide, With No Side-Effects
* GIVEN: That I have a method that calls a private method.
* WHEN: I write my test for this method.
* THEN: I do not test it.

```csharp
```

#### Scenario: Property Changes Triggered via Fody.
* GIVEN: I have Fody setup and configured.
* WHEN: I write my test for a property.
* THEN: I should not write a test for the property change event.

(this constitutes testing the "framework" / "plugin" and serves little purpose)

```csharp
```