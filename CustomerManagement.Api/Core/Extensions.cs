using System;
using Microsoft.AspNetCore.Http;

namespace CustomerManagement.Api
{
	public static class Extensions
	{
		public static void AddPagination(this HttpResponse response, int totalItems)
		{
			response.Headers.Add("TotalItemsCount", totalItems.ToString());
			response.Headers.Add("access-control-expose-headers", "TotalItemsCount");
		}
		public static void AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("access-control-expose-headers", "Application-Error");
		}
	}
}
