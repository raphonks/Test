using System.Collections.Generic;

namespace RaphaelPinho.Twitter.Services
{
    public interface IService
    {
        List<Entities.Tweet> Search(dynamic parameters);
    }
}
