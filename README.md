# Flowstate.Notifications

Flowstate.Notitifications is a minimal C# Notification library based on C# structs (reduced allocations), a few factory methods and implicit casts to specify failure details.

## Context

"Notification" refers to the homonymous design pattern, used to avoid the unintelligible and runtime costly exceptions throwing for mere input and business rule validation. However, this is not a substitute for structured error handling, as exceptions should continue to be used for guard clauses and the like.

## Usage

### Simple early return based validation
``` 
Result Operation1(string param)
{
    if (string.IsNullOrEmpty(param))
        return Result.Failure("'param' must be provided.");

    return Result.Success();
}
```

### Simple early return based validation, now specifying a tag
``` 
Result Operation2(string param)
{
    if (string.IsNullOrEmpty(param))
        return Result.Failure(("'param' must be provided.", "some_tag"));

    return Result.Success();
}
```

### Simple early return based validation, specifying two failure details
``` 
Result Operation3(string param)
{
    if (param.StartsWith("x") && param.EndsWith("z"))
        return Result.Failure(
            "'param' shouldn't ... x ",
            "'param' should'nt ... z");

    return Result.Success();
}
```

### Simple early return based validation, specifying two failure details with tags
``` 
Result Operation4(string param)
{
    if (param.StartsWith("x") && param.EndsWith("z"))
        return Result.Failure(
            ("'param' shouldn't ... x ", "tag1"),
            ("'param' shouldn't ... z", "tag2"));

    return Result.Success();
}
```

### Simple early return based validation, specifying two failure details with tags
``` 
Result Operation5(string param)
{
    if (param.StartsWith("x") && param.EndsWith("z"))
        return Result.Failure(
            new FailureDetail("'param' shouldn't ... x ", "tag1"), // Without convenience implicit cast
            new FailureDetail("'param' shouldn't ... z", "tag2"));

    return Result.Success();
}
```

### Accumulating failure details created in multiple ways
``` 
Result Operation6(string param1, string param2, string param3)
{
    var details = new List<FailureDetail>();

    if (string.IsNullOrEmpty(param1))
        details.Add("'param1' must..."); // Implicit cast from string to description only FailureDetail


    if (string.IsNullOrEmpty(param2))
    {
        // Implicit cast from (string, string) value tuple to complete FailureDetail

        details.Add(("'param2' should this...", "some_tag"));            
        details.Add(("'param2' should that...", "some_tag"));
    }

    if (string.IsNullOrEmpty(param3))
        details.Add(new FailureDetail("'param3' must...", "another_tag"));

    return details.Any() ? Result.Failure(details) : Result.Success();
}
```


### Valued results
``` 
Result<int> Operation7(string param)
{
    if (string.IsNullOrEmpty(param))
        return Result<int>.Failure("'param' must be provided.");

    return Result<int>.Success(123);
}
```

### Handling multiple notification based operations
``` 
Result<int> OperationA(string param) { /*...*/}
Result<int> OperationB(string param) { /*...*/}

Result<double> OperationC(string param)
{
    var op1Result = OperationA(param);
            
    if (!op1Result) // Note the convenience boolean implicit cast
        return op1Result.Cast<double>(); 

    var op2Result = OperationB(param);
            
    if (!op2Result.Succeeded) // ... without implicit cast
        return op2Result.Cast<double>();

    var op3Calculation = (double)op1Result.Value / op2Result.Value;

    return Result<double>.Success(op3Calculation);
}
```


## References

- [Notification Design Pattern](https://martinfowler.com/eaaDev/Notification.html)
