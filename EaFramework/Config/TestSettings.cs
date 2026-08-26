namespace EaFramework.Config;

public class TestSettings
{
    public string[]? Args { get;  set; }

    public float Timeout { get;  set; }
    public bool Headless { get;  set; }
    
    public string ApplicationUrl { get;  set; }
    public float SlowMo { get;  set; }
    
    public DriverType DriverType { get;  set; }
    
   
    
}

public enum DriverType
{
    Chrome,
    Firefox,
    Chromium,
    Edge,
    WebKit
}