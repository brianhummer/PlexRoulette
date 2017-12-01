using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlexRoulette.Models;

namespace PlexRoulette.Services
{
    public interface IPlexApi
    {
        Task<PlexAuthentication> SignIn();
        Task<PlexContainer> GetLibrarySections(string authToken);
        Task<PlexContainer> GetLibrary(string authToken, string libraryId);
        Task<T> Request<T>(Request request);
    }
}
