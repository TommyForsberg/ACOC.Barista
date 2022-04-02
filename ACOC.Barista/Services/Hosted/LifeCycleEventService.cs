namespace ACOC.Barista.Services.Hosted
{
    public class LifeCycleEventService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<LifeCycleEventService> logger;

        public LifeCycleEventService(IServiceProvider serviceProvider, ILogger<LifeCycleEventService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("{Service} is executing", nameof(LifeCycleEventService));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = serviceProvider.CreateScope();

                    //var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                    //await paymentService.ProcessIncompletedPayments(stoppingToken);
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }
}
