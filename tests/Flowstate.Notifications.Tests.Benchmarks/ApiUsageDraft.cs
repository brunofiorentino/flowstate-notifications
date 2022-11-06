namespace Flowstate.Notifications.Tests.Benchmarks
{
    internal class ApiUsageDraft
    {
        Result Operation1(string param)
        {
            if (string.IsNullOrEmpty(param))
                return Result.Failure("'param' must be provided.");

            return Result.Success();
        }

        Result Operation2(string param)
        {
            if (string.IsNullOrEmpty(param))
                return Result.Failure(("'param' must be provided.", "some_tag"));

            return Result.Success();
        }

        Result Operation3(string param)
        {
            if (param.StartsWith("x") && param.EndsWith("z"))
                return Result.Failure(
                    "'param' shouldn't ... x ",
                    "'param' should'nt ... z");

            return Result.Success();
        }

        Result Operation4(string param)
        {
            if (param.StartsWith("x") && param.EndsWith("z"))
                return Result.Failure(
                    ("'param' shouldn't ... x ", "tag1"),
                    ("'param' shouldn't ... z", "tag2"));

            return Result.Success();
        }

        Result Operation5(string param)
        {
            if (param.StartsWith("x") && param.EndsWith("z"))
                return Result.Failure(
                    new FailureDetail("'param' shouldn't ... x ", "tag1"), // Without convenience implicit cast
                    new FailureDetail("'param' shouldn't ... z", "tag2"));

            return Result.Success();
        }

        Result Operation6(string param1, string param2, string param3)
        {
            var details = new List<FailureDetail>();

            if (string.IsNullOrEmpty(param1))
                details.Add("'param1' must..."); // Note the convenience implicit cast from string to description only (without tag) FailureDetail


            if (string.IsNullOrEmpty(param2))
            {
                // Note the convenience implicit cast from (string, string) value tuple to complete FailureDetail

                details.Add(("'param2' should this...", "some_tag"));            
                details.Add(("'param2' should that...", "some_tag"));
            }

            if (string.IsNullOrEmpty(param3))
                details.Add(new FailureDetail("'param3' must...", "another_tag"));

            return details.Any() ? Result.Failure(details) : Result.Success();
        }

        Result<int> Operation7(string param)
        {
            if (string.IsNullOrEmpty(param))
                return Result<int>.Failure("'param' must be provided.");

            return Result<int>.Success(123);
        }




        Result<int> OperationA(string param)
        {
            if (string.IsNullOrEmpty(param))
                return Result<int>.Failure("'param' must be provided.");

            return Result<int>.Success(123);
        }
        Result<int> OperationB(string param)
        {
            if (string.IsNullOrEmpty(param))
                return Result<int>.Failure("'param' must be provided.");

            return Result<int>.Success(123);
        }

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
    }
}