using System;
using System.Text.RegularExpressions;

public class DomainNameValidator
{
  private Regex rxNumericOnly = new Regex(@"^\d+$", RegexOptions.Compiled);
  private Regex rxValidSub = new Regex(
    @"^(?!\-)[0-9a-z\-]{1,63}(?<!\-)$",
    RegexOptions.IgnoreCase | RegexOptions.Compiled
  );

  public bool validate(string domain)
  {
    // Domain name must not be longer than 253 characters (RFC specifies 255, but 2 characters are reserved for trailing dot and null character for root level)
    if (domain.Length > 253) return false;

    // Domain name may contain subdomains (levels),
    // hierarchically separated by . (period) character
    string[] subs = domain.Split('.');

    // Domain name must not contain more than 127 levels, including TLD
    // Domain name must contain at least one subdomain (level) apart from TLD
    if (subs.Length > 127 || subs.Length < 2) return false;

    // Top level (TLD) must not be fully numerical
    if (rxNumericOnly.IsMatch(subs[subs.Length - 1])) return false;

    foreach (string sub in subs)
    {
      // Level names must be composed out of lowercase and uppercase ASCII letters, digits and - (minus sign) character
      // Level names must not start or end with - (minus sign) character
      // Level names must not be longer than 63 characters
      if (!rxValidSub.IsMatch(sub)) return false;
    }

    return true;
  }
}
