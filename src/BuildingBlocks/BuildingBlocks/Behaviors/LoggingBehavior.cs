﻿using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            
            logger.LogInformation($"Handling {typeof(TRequest).Name} Response ={typeof(TResponse).Name}");

            var timer = new Stopwatch();
            timer.Start();


            var response = await next();


            timer.Stop();

            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning($"Long Running Request: {typeof(TRequest).Name} Time taken: {timeTaken}");
            }
            else
            {
                logger.LogInformation($"Request: {typeof(TRequest).Name} Time taken: {timeTaken}");
            }

            return response;



        }
    }
}
