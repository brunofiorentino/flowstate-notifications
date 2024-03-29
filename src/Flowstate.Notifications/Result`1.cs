﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Flowstate.Notifications
{
    public struct Result<T>
    {
        internal static readonly IReadOnlyList<FailureDetail> EmptyFailureDetails = Array.Empty<FailureDetail>();

        private bool _succeeded;
        private T _value;
        private IReadOnlyList<FailureDetail> _failureDetails;

        public bool Succeeded => _succeeded;
        public T Value => _value;
        public IReadOnlyList<FailureDetail> FailureDetails => _failureDetails ?? EmptyFailureDetails;

        public static Result<T> Success(T value) => new Result<T> { _succeeded = true, _value = value };

        public static Result<T> Failure(IReadOnlyList<FailureDetail> details = null) =>
            (details?.Any(x => x.Equals(default)) ?? false)
                ? throw new ArgumentException(ResultsErrorMessages.DetailsContainsUninitializedItems, nameof(details))
                : new Result<T> { _succeeded = false, _failureDetails = details ?? EmptyFailureDetails };


        public static Result<T> Failure(params FailureDetail[] details) =>
            Failure((IReadOnlyList<FailureDetail>)details);


        public void Deconstruct(out bool succeeded, out T value, out IReadOnlyList<FailureDetail> failureDetails)
        {
            succeeded = Succeeded;
            value = Value;
            failureDetails = FailureDetails;
        }

        public Result<TTarget> Cast<TTarget>() =>
            _succeeded
                ? throw new Exception(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult)
                : Result<TTarget>.Failure(FailureDetails);


        public Result Cast() =>
            _succeeded
                ? Result.Success()
                : Result.Failure(FailureDetails);


        public static implicit operator bool(Result<T> @this) => @this.Succeeded;
    }
}