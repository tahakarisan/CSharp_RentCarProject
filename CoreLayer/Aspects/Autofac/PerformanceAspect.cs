using Castle.DynamicProxy;
using CoreLayer.CrossCuttingConcerns.Validation;
using CoreLayer.Utilities.Interceptors;
using CoreLayer.Utilities.IoC;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Aspects.Autofac
{
    public class PerformanceAspect : MethodInterception
    {
        private Stopwatch _stopwatch;
        private int _maxTime;
        public PerformanceAspect(int maxtime)
        {
            _maxTime = maxtime;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
    
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            var result = _stopwatch.Elapsed.TotalSeconds;
            if (result >= _maxTime)
            {
                Debug.WriteLine($"Performance: {invocation.Method.DeclaringType}.{invocation.Method.Name}.{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        } 

    }


}

