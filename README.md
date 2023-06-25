# Flowstate.Notifications

Flowstate.Notifications is a minimal C# Notification library based on structs (reduced heap allocations) and a simple API: a few factory methods for success and failure results, in the latter case relying on implicit casts to expressively specify failure details.

"Notifications" refers to the [homonymous design pattern](https://martinfowler.com/eaaDev/Notification.html), used to avoid exceptions for control flow/mere input and business rule validation as stacktraces can be costly and it can be harders to reason. However, this is not a substitute for structured error handling, as exceptions should continue to be used for cases like missing/mismatching dependencies or plain api misusage. [Here's another related/interesting article](https://shipilev.net/blog/2014/exceptional-performance/) where the author suggests that 1 in 10K would be "exceptional enough".

## Usage

### Simple early return based validation
``` 
Result Operation(string param)
{
    if (string.IsNullOrEmpty(param))
        return Result.Failure("'param' must be provided.");

    return Result.Success();
}
```

### Simple early return based validation with valued results
``` 
Result<int> Operation(string param)
{
    if (string.IsNullOrEmpty(param))
        return Result<int>.Failure("'param' must be provided.");

    return Result<int>.Success(123);
}
```

### Simple early return based validation specifying a tag
``` 
Result Operation(string param)
{
    if (string.IsNullOrEmpty(param))
        return Result.Failure(("'param' must be provided.", "some_tag"));

    return Result.Success();
}
```

### Simple early return based validation specifying many failure details
``` 
Result Operation(string param)
{
    if (param.StartsWith("x") && param.EndsWith("z"))
        return Result.Failure("'param' shouldn't ... x ", "'param' shouldn't ... z");

    return Result.Success();
}
```

### Accumulating failure details created in multiple ways
``` 
Result Operation(string param1, string param2, string param3)
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
