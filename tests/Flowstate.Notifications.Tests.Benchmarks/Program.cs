using BenchmarkDotNet.Running;
using Flowstate.Notifications.Tests.Benchmarks;

BenchmarkRunner.Run(typeof(NotificationsBenchmarks).Assembly);