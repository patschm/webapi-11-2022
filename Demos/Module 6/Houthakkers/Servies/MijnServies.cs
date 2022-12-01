using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Houthakkers.Servies
{
    public class MijnServies
    {
        private int counter = 0;
        private readonly ILogger<MijnServies> _logger;

        public MijnServies(ILogger<MijnServies> logger)
        {
            this._logger = logger;
        }

        public void Increment()
        {
            _logger.LogInformation($"[Voor] De nieuwe waarde voor counter is: {counter}");
            Task.Delay(2000).Wait();
            counter++;
            _logger.LogInformation($"[Na] De nieuwe waarde voor counter is: {counter}");
        }
    }
}