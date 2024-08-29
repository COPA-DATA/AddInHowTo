using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("DynamicFilterSample", "1.0",Namespace = "com.copadata.training")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("DynamicFilterSample")]
[assembly: AddinDescription("This wizard is for demonstration purposes only. It shows the usage of dynamic properties in a project wizard extention.")]