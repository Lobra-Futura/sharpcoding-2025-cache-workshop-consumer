namespace MyFurniture;

public class CacheKeys
{
    public const string PRODUCTS = "products";
    public const string ASSETS = "assets";
    public const string PRICES = "prices";
    public const string STOCKS = "stocks";
}

public class RedisKeys
{
    public static readonly string _prefix = Environment.MachineName;
    public static readonly string PRODUCTS = _prefix + "-products";
    public static readonly string ASSETS = _prefix + "-assets";
    public static readonly string PRICES = _prefix + "-prices";
    public static readonly string STOCKS = _prefix + "-stocks";
}

public class HybridKeys
{
    public static readonly string _prefix = Environment.MachineName + "-hy";
    public static readonly string PRODUCTS = _prefix + "-products";
    public static readonly string ASSETS = _prefix + "-assets";
    public static readonly string PRICES = _prefix + "-prices";
    public static readonly string STOCKS = _prefix + "-stocks";
}