using System;
namespace ShortUrl.Utility
{
	public class UriValidator
	{
		public static bool ValidateUri(string uriName)
		{
			return Uri.IsWellFormedUriString(uriName, UriKind.Absolute);
		}

		public static Uri CombineUri(Uri baseUri, string encryptedUri)
        {
            return new Uri(baseUri, encryptedUri);
		}
	}
}
