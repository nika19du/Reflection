using ReflectionSample;
using System.Reflection;

Console.Title = "Learning Reflection";

NetworkMonitor.BootstrapFromConfiguration();

Console.WriteLine("Monitor network... something went wrong.");

NetworkMonitor.Warn();

Console.ReadLine();

static void CodeFromThirdModule()
{
    var personType = typeof(Person);
    var personConstructors = personType.GetConstructors(
        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

    foreach (var constructor in personConstructors)
    {
        Console.WriteLine(constructor);
    }

    var privatePersonConstructor = personType.GetConstructor(
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new Type[] { typeof(string), typeof(int) },
        null);

    var person1 = personConstructors[0].Invoke(null);
    var person2 = personConstructors[1].Invoke(new object[] { "Kevin" });
    var person3 = personConstructors[2].Invoke(new object[] { "Sara", 21 });

    var person4 = Activator.CreateInstance("ReflectionSample", "ReflectionSample.Person").Unwrap();
    var person5 = Activator.CreateInstance("ReflectionSample",
        "ReflectionSample.Person",
        true,
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new object[] { "Kevin", 33 },
        null,
        null
        );

    var personTypeFromString = Type.GetType("ReflectionSample.Person");
    var person6 = Activator.CreateInstance(personTypeFromString,
        new object[] { "Kevin" });

    var assembly = Assembly.GetExecutingAssembly();
    var person7 = assembly.CreateInstance("ReflectionSample.Person");

    // create a new instance of a configured type
    var actualTypeFromConfiguration = Type.GetType(GetTypeFromConfiguration());
    var iTalkInstance = Activator.CreateInstance(actualTypeFromConfiguration) as ITalk;
    iTalkInstance.Talk("Hello World!");

    dynamic dynamicITalkInstance = Activator.CreateInstance(actualTypeFromConfiguration);
    dynamicITalkInstance.Talk("Hello world!");

    var personForManipulation = Activator.CreateInstance("ReflectionSample",
        "ReflectionSample.Person",
        true,
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new object[] { "Harry", 25 },
        null,
        null)?.Unwrap();

    var nameProperty = personForManipulation?.GetType().GetProperty("Name");
    nameProperty?.SetValue(personForManipulation, "Sos");

    var ageField = personForManipulation?.GetType().GetField("age");
    ageField?.SetValue(personForManipulation, 37);

    var privateField = personForManipulation?.GetType().GetField("_aPrivateField",
        BindingFlags.Instance | BindingFlags.NonPublic);
    privateField?.SetValue(personForManipulation, "updated private field value");

    personForManipulation?.GetType().InvokeMember("Name",
        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
        null, personForManipulation, new[] { "Emma" });

    personForManipulation?.GetType().InvokeMember("_aPrivateField",
        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField,
        null, personForManipulation, new[] { "second update for a private field value" });

    Console.WriteLine(personForManipulation);

    var talkMethod = personForManipulation?.GetType().GetMethod("Talk");
    talkMethod?.Invoke(personForManipulation, new[] { "something to say" });

    personForManipulation?.GetType().InvokeMember("Yell",
        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
        null, personForManipulation, new[] { "something to yell" });

    Console.ReadLine();

    static string GetTypeFromConfiguration()
    {
        return "ReflectionSample.Person";
    }
}
