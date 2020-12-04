using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("XmlImporter", "1.0")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("XmlImporter")]
[assembly: AddinDescription("This Wizard is used to demonstrate a technique for zenon object creation. This wizards modifies an xml and imports the xml file into zenon.")]