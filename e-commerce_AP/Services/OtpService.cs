
using System;
using System.Collections.Generic;

namespace e_commerce_AP.Services
{
    public class OtpService
    {
        private readonly Dictionary<string, string> _otpStore = new Dictionary<string, string>();

        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void StoreOtp(string email, string otp)
        {
            _otpStore[email] = otp;
        }

        public bool ValidateOtp(string email, string otp)
        {
            return _otpStore.TryGetValue(email, out var storedOtp) && storedOtp == otp;
        }
    }
}

