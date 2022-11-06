﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowstate.Notifications.Tests.References
{
    public class Result
    {
        internal static readonly IReadOnlyList<FailureDetail> EmptyFailureDetails = Array.Empty<FailureDetail>();

        private bool _succeeded;
        private IReadOnlyList<FailureDetail> _failureDetails;

        public bool Succeeded => _succeeded;
        public IReadOnlyList<FailureDetail> FailureDetails => _failureDetails ?? EmptyFailureDetails;

        public static Result Success() => new Result { _succeeded = true };

        public static Result Failure(IReadOnlyList<FailureDetail> details = null) =>
            details?.Any(x => x.Equals(default)) ?? false
                ? throw new ArgumentException(ResultsErrorMessages.DetailsContainsUninitializedItems, nameof(details))
                : new Result { _succeeded = false, _failureDetails = details ?? EmptyFailureDetails };


        public static Result Failure(params string[] details) =>
            Failure(details?.Select(x => new FailureDetail(x)).ToArray());


        public void Deconstruct(out bool succeeded, out IReadOnlyList<FailureDetail> failureDetails)
        {
            succeeded = Succeeded;
            failureDetails = FailureDetails;
        }

        public Result<TTarget> CastFailure<TTarget>() =>
            _succeeded
                ? throw new Exception(ResultsErrorMessages.CannotCastSuccessResultAsFailure)
                : Result<TTarget>.Failure(FailureDetails);


        public static implicit operator bool(Result @this) => @this.Succeeded;
    }
}