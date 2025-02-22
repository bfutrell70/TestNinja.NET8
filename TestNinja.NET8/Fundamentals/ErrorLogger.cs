﻿using System;

namespace TestNinja.NET8.Fundamentals
{
    public class ErrorLogger
    {
        public string? LastError { get; set; }

        public event EventHandler<Guid>? ErrorLogged; 
        
        public void Log(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException(nameof(error));
                
            LastError = error; 
            
            // Write the log to a storage
            // ...

            ErrorLogged?.Invoke(this, Guid.NewGuid());
        }
    }
}