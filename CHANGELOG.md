# Basic portable abstractions for Pip.Services in .NET Changelog

## <a name="1.0.3-1.0.33"></a> 1.0.3-1.0.33 (2016-12-11)

### Features
* **auth** MemoryCredentialStore
* **config** IConfigReader interface and readers for AppSettings and ConnectionStrings
* **config** IConfigReader, CachedConfigReader
* **config** NameResolver. Improved support for named (non-singleton) components 
* **connect** MemoryDiscovery
* **log** DiagnoticsLogger
* **refer** Made get methods in IReferences generic
* **refer** Added PutAll method to IReferences
* **run** Changed FixedRateTimer interface
* **refer** Added Kind field to Descriptor
* **refer** ReferencesDecorators and ManagedReferences.
* **data** Added IVersioned interface
* **log** EventLogger
* **config** OptionsResolver

### Bug Fixes
* Added description to NuGet package
* Added development documentation
* Made convenience changes in StringValueMap and AnyValueMap
* Fixed NullPointerException in JsonConverter.ToNullableMap
* Fixed NullPointerException in AnyValueMap and StringValueMap
* Made key methods virtual
* Fixed NullPointerException in NameResolver
* Fixed wrong cast in Referencer
* Fixed GetAll in references
* Fixed NullPointerException in MemoryDiscovery
* Set default log levels
* Fixed endless loop in Loggers while logging errors
* Fixed printing wrong level in loggers
* Renamed ReferenceSet to References
* Added auto-creation to GetOptional methods in References
* Added CompositeCounters constructor with references 
* Renamed SetAsMap method to Append
* Added missing descriptor to EventLogger

## <a name="1.0.0"></a> 1.0.0 (2016-11-21)

Initial public release

### Features
* **auth** Credentials for client authentication
* **build** Component factories
* **commands** Command and Eventing patterns
* **config** Configuration framework
* **connect** Connection parameters
* **convert** Portable soft data converters
* **count** Performance counters
* **data** Data value objects
* **errors** Portable application errors
* **log** Logging components
* **random** Random data generators
* **refer** Component referencing framework
* **reflect** Portable reflection helpers
* **run** Execution framework
* **validate** Data validators

### Breaking Changes
No breaking changes since this is the first version

### Bug Fixes
No fixes in this version

