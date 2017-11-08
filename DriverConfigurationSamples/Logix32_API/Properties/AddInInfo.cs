using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("Logix32_API", "1.0")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("Logix32_API")]
[assembly: AddinDescription("")]