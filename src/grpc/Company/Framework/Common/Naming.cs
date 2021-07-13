using System.Diagnostics;

namespace Company.Framework.Common
{
    public static class Naming
    {
        //public static string Microservice<I>()//  where I : IService
        //{
        //    Debug.Assert(typeof(I).Namespace.Contains("Manager"), "Invalid microservice interface. Use only the Manager interface to access a microservice.");
        //    string[] namespaceSegments = typeof(I).Namespace.Split('.');
        //    return namespaceSegments[0] + ".Microservice." + namespaceSegments[2];
        //}
        //public static string Component<I>() //  where I : IService
        //{
        //    string[] namespaceSegments = typeof(I).Namespace.Split('.');
        //    return namespaceSegments[2] + namespaceSegments[1];
        //}
        //public static string Listener<I>() // where I : IService
        //{
        //    return typeof(I).Name;
        //}

        //public static string ServiceType<T>() // where T : StatelessService
        //{
        //    return typeof(T).FullName.Replace(".Service", "");
        //}
        //public static string ActorType<T>() //  where T : Actor
        //{
        //    return typeof(T).FullName.Replace(".Service", "");
        //}
    }
}
