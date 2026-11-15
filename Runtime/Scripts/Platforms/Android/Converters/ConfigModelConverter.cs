using UnityEngine;

namespace Appcharge.PaymentLinks.Platforms.Android {
	public static class ConfigModelConverter
	{
		public static AndroidJavaObject ToAndroidJavaObject(string checkoutPublicKey, string environment)
		{
			return new AndroidJavaObject(
				"com.appcharge.paymentlinks.domain.model.ConfigModel",
				checkoutPublicKey,
				environment
			);
		}
	}
}