using Microsoft.AspNetCore.Mvc;
using MiddlewareNDi.Dependencies;
using MiddlewareNDi;

namespace dependencyInection.Controllers
{
    [ApiController]
    [Route("/api/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DependencyService1 dependencyService1;
        private readonly DependencyService2 dependencyService2;
        private readonly IEnumerable<IOperationSingletonInstance> allSingletonInstances;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            DependencyService1 dependencyService1,
            DependencyService2 dependencyService2,
            IEnumerable<IOperationSingletonInstance> allSingletonInstances
            )
        {
            _logger = logger;
            this.dependencyService1 = dependencyService1;
            this.dependencyService2 = dependencyService2;
            this.allSingletonInstances = allSingletonInstances;
            _logger.LogInformation("Hello from WeatherForecast");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            foreach (var instance in allSingletonInstances)
                Console.WriteLine(instance.OperationId);

            Console.WriteLine("Get Method Called");
            dependencyService1.Write();
            dependencyService2.Write();
            return Enumerable.Empty<WeatherForecast>();
        }
    }
}
