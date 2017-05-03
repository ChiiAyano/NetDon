using System;
using System.Collections.Generic;
using System.Text;

namespace NetDon.Enums
{
    [Flags]
    public enum Scope
    {
        Read = 0x00,
        Write = 0x01,
        Follow = 0x10
    }

    public static class ScopeExtensions
    {
        public static string ToScopeStrings(this Scope scopes, string joinSeparator = " ")
        {
            var result = new List<string>();
            if ((scopes & Scope.Read) == Scope.Read) result.Add("read");
            if ((scopes & Scope.Write) == Scope.Write) result.Add("write");
            if ((scopes & Scope.Follow) == Scope.Follow) result.Add("follow");

            return string.Join(joinSeparator, result);
        }
    }
}
