using Quiz.Infra.Interfaces;

namespace Quiz.Infra.Register
{
    public static class ClassMapInitializer
    {
        public static void RegisterAllClassMaps()
        {
            IEnumerable<Type> classMapTypes = typeof(ClassMapInitializer).Assembly.GetTypes()
                .Where(t => typeof(IEntityClassMap).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (Type classMapType in classMapTypes)
            {
                IEntityClassMap? classMapInstance = Activator.CreateInstance(classMapType) as IEntityClassMap;
                classMapInstance?.Register();
            }
        }
    }
}
