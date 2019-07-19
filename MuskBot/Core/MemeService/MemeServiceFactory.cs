using System;
using System.Collections.Generic;
using System.Text;

namespace MuskBot.Core.MemeService
{
    public interface IFactory<T>
    {
        T Create(string flag);
    }

    public class MemeServiceFactory : IFactory<IMemeService>
    {
        private readonly IServiceProvider _serviceProvider;

        public MemeServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IMemeService Create(string flag)
        {
            if(flag.ToLowerInvariant() == "-g")
            {
                return (IMemeService) _serviceProvider.GetService(typeof(GiphyMemeService));
            }
            return (IMemeService) _serviceProvider.GetService(typeof(TenorMemeService));
        }
    }
}
