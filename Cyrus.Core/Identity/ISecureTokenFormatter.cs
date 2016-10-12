using System;
using Cyrus.Core.DomainModels.Identity;


namespace Cyrus.Core.Identity
{
    public interface ISecureTokenFormatter : IDisposable
    {

        string Protect(object ticket);
        void Unprotect(string text);

    }
}
